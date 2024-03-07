using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;

namespace InitialProject.Services
{
    public class AccommodationStatisticsService
    {
        private readonly ReservationService _reservationService;
        private readonly AccommodationReservationService _accommodationReservationService;
        private readonly NotificationService _notificationService;
        private readonly RenovationRecommendationService _renovationRecommendationService;
        private readonly ChangeReservationRequestService _changeReservationRequestService;

        public AccommodationStatisticsService()
        {
            _reservationService = new ReservationService();
            _accommodationReservationService = new AccommodationReservationService();
            _notificationService = new NotificationService();
            _renovationRecommendationService = new RenovationRecommendationService();
            _changeReservationRequestService = new ChangeReservationRequestService();
        }

        public List<Reservation> ReservationsByAccommodationIdAndYear(int accommodationId, int year)
        {
            List<Reservation> reservationsById = ReservationsByAccommodationID(accommodationId);
            List<Reservation> finalReservationList = new List<Reservation>();

            foreach (Reservation reservation in reservationsById)
            {
                if (reservation.ReservationDateRange.StartDate.Year == year || reservation.ReservationDateRange.EndDate.Year == year)
                {
                    finalReservationList.Add(reservation);
                }
            }

            return finalReservationList;
        }

        public List<Reservation> ReservationsByAccommodationID(int accommodationID)
        {
            List<int> ReservationIds = _accommodationReservationService.GetReservationsIdsByAccommodationId(accommodationID);
            List<Reservation> Reservations = new List<Reservation>();

            foreach (int reservationId in ReservationIds)
            {
                Reservation reservation = _reservationService.GetReservationById(reservationId);
                Reservations.Add(reservation);
            }

            return Reservations;
        }


        public List<StatisticsByYearDTO> YearStatisticsForAccommodation(int accommodationID)
        {
            List<StatisticsByYearDTO> statisticsByYear = new List<StatisticsByYearDTO>();

            List<Reservation> reservations = new List<Reservation>();
            reservations = ReservationsByAccommodationID(accommodationID);

            foreach (Reservation reservation in reservations)
            {
                StatisticsByYearDTO newStatistics = Years(reservation.ReservationDateRange.StartDate.Year, statisticsByYear);
                newStatistics.NumberOfReservations += 1;

                if (_notificationService.IsReservationCanceled(reservation.ReservationId))
                {
                    newStatistics.NumberOfCanceledReservations += 1;
                }
            }
            YearlyRenovationRecommendationNumber(statisticsByYear, accommodationID);
            YearlyChangeReservationRequestNumber(statisticsByYear, accommodationID);
            YearDaysOccupied(statisticsByYear, reservations);

            return statisticsByYear;
        }

        public StatisticsByYearDTO Years(int year, List<StatisticsByYearDTO> statisticsByYearDTOs)
        {
            foreach (StatisticsByYearDTO sby in statisticsByYearDTOs)
            {
                if (sby.Year == year)
                {
                    return sby;
                }
            }

            StatisticsByYearDTO stats = new StatisticsByYearDTO();
            stats.Year = year;
            statisticsByYearDTOs.Add(stats);
            return stats;
        }

        public void YearlyRenovationRecommendationNumber(List<StatisticsByYearDTO> statisticsByYearDTOs, int accommodationId)
        {
            List<RenovationRecommendation> renvationRecommendations = _renovationRecommendationService.GetAllRecommendationsByAccommodationId(accommodationId);

            foreach (RenovationRecommendation rr in renvationRecommendations)
            {
                StatisticsByYearDTO statistic = Years(rr.RecommendationDate.Year, statisticsByYearDTOs);
                statistic.ReccommendationForRenovations += 1;
            }
        }

        public void YearlyChangeReservationRequestNumber(List<StatisticsByYearDTO> statisticsByYearDTOs, int accommodationId)
        {
            List<ChangeReservationRequest> changeReservationRequests = _changeReservationRequestService.GetRequestsByAccommodationId(accommodationId);
            foreach (ChangeReservationRequest crr in changeReservationRequests)
            {
                StatisticsByYearDTO statistic = Years(crr.NewStartDate.Year, statisticsByYearDTOs);
                if (crr.RequestStatus.ToString() == "Approved")
                {
                    statistic.NumberOfRescheduledReservations += 1;
                }
            }
        }

        public void YearDaysOccupied(List<StatisticsByYearDTO> statistics, List<Reservation> reservations)
        {

            foreach (Reservation reservation in reservations)
            {
                StatisticsByYearDTO statisticsByYearDTO = Years(reservation.ReservationDateRange.StartDate.Year, statistics);

                int daysOccupied = 0;
                int daysOccupied1= 0;
                if (reservation.Status == ReservationStatus.Finished)
                {
                    if(reservation.ReservationDateRange.EndDate.Year == reservation.ReservationDateRange.StartDate.Year)
                    {
                    TimeSpan duration = reservation.ReservationDateRange.EndDate.Subtract(reservation.ReservationDateRange.StartDate);
                    daysOccupied = duration.Days;
                    statisticsByYearDTO.DaysOccupied += daysOccupied;
                    }
                    else
                    {
                        int year = statisticsByYearDTO.Year;
                        DateTime lastDayOfYear = new DateTime(year, 12, 31);
                        TimeSpan duration = lastDayOfYear.Subtract(reservation.ReservationDateRange.StartDate);
                        daysOccupied = duration.Days;
                        statisticsByYearDTO.DaysOccupied += daysOccupied;

                        StatisticsByYearDTO statisticsByYearDTONextYear = Years(year + 1, statistics);
                        int year1 = statisticsByYearDTONextYear.Year;
                        DateTime firstDayOfYear = new DateTime(year1, 1, 1);
                        TimeSpan duration1 = reservation.ReservationDateRange.EndDate.Subtract(firstDayOfYear);
                        daysOccupied1 = duration1.Days;
                        statisticsByYearDTONextYear.DaysOccupied += daysOccupied1;
                    }
                }
            }
        }

        public StatisticsByYearDTO MostOccupiedYear(List<StatisticsByYearDTO> statistics)
        {
            StatisticsByYearDTO statisticWithHighestOccupation = null;
            int maxRemainingDays = int.MaxValue;

            foreach(StatisticsByYearDTO statistic in statistics)
            {
                int days = 0;
                days = 365 - statistic.DaysOccupied;
                if(days > maxRemainingDays)
                {
                    maxRemainingDays = days;
                    statisticWithHighestOccupation = statistic;
                }
            }

            return statisticWithHighestOccupation;
        }

        public List<StatisticsByMonthDTO> MonthStatisticsForYear(int accommodationId, int year)
        {
            List<StatisticsByMonthDTO> monthlyStatistics = new List<StatisticsByMonthDTO>();
            List<Reservation> reservations = new List<Reservation>();

            reservations = ReservationsByAccommodationIdAndYear(accommodationId, year);

            foreach (Reservation reservation in reservations)
            {
                if(reservation.ReservationDateRange.StartDate.Year == year)
                {
                    StatisticsByMonthDTO newStatistic = Month(monthlyStatistics, reservation.ReservationDateRange.StartDate.Month,year);
                    newStatistic.NumberOfReservations+= 1;

                if (_notificationService.IsReservationCanceled(reservation.ReservationId))
                {
                    newStatistic.NumberOfCanceledReservations += 1;
                }

                }
            }

            MonthlyRenovationRecommendationNumber(monthlyStatistics, accommodationId,year);
            MonthlyChangeReservationRequestNumber(monthlyStatistics, accommodationId, year);
            MonthDaysOccupied(monthlyStatistics, reservations,year);

            
            return FilterListByYear(monthlyStatistics, year);
            
        }

        public List<StatisticsByMonthDTO> FilterListByYear(List<StatisticsByMonthDTO>statisticsByMonthDTOs, int year)
        {
            List<StatisticsByMonthDTO> statistics = new List<StatisticsByMonthDTO>();
            foreach(StatisticsByMonthDTO statistic in statisticsByMonthDTOs)
            {
                if(statistic.Year == year)
                {
                    statistics.Add(statistic);
                }
            }

            return statistics;
        }

        public StatisticsByMonthDTO Month(List<StatisticsByMonthDTO> statisticsByMonth, int month,int year)
        {
            foreach(StatisticsByMonthDTO sbm in statisticsByMonth)
            {
                if(sbm.Month == month && sbm.Year == year)
                {
                    return sbm;
                }
            }

            StatisticsByMonthDTO statistic = new StatisticsByMonthDTO();
            statistic.Month = month;
            statistic.Year= year;
            statisticsByMonth.Add(statistic);
            return statistic;
        }


        public void MonthlyRenovationRecommendationNumber(List<StatisticsByMonthDTO> statisticsByMonthDTOs, int accommodationId,int year)
        {
            List<RenovationRecommendation> renvationRecommendations = _renovationRecommendationService.GetAllRecommendationsByAccommodationId(accommodationId);

            foreach (RenovationRecommendation rr in renvationRecommendations)
            {
                if(rr.RecommendationDate.Year == year)
                {
                    StatisticsByMonthDTO statistic = Month(statisticsByMonthDTOs, rr.RecommendationDate.Month,year);
                    statistic.ReccommendationForRenovations += 1;
                }
            }
        }


        public void MonthlyChangeReservationRequestNumber(List<StatisticsByMonthDTO> statisticsByMonthDTOs, int accommodationId, int year)
        {
            List<ChangeReservationRequest> changeReservationRequests = _changeReservationRequestService.GetRequestsByAccommodationId(accommodationId);
            foreach (ChangeReservationRequest crr in changeReservationRequests)
            {
                if (crr.NewStartDate.Year == year)
                {
                    StatisticsByMonthDTO statistic = Month(statisticsByMonthDTOs, crr.NewStartDate.Month, year);
                    if (crr.RequestStatus.ToString() == "Approved")
                    {
                        statistic.NumberOfRescheduledReservations += 1;
                    }
                }
            }
        }

        public StatisticsByMonthDTO MostOccupiedMonth(List<StatisticsByMonthDTO> statistics)
        {
            StatisticsByMonthDTO statisticWithHighestOccupation = null;
            int maxRemainingDays = int.MaxValue;

            foreach (StatisticsByMonthDTO statistic in statistics)
            {
                int days = 0;
                days = 365 - statistic.DaysOccupied;
                if (days > maxRemainingDays)
                {
                    maxRemainingDays = days;
                    statisticWithHighestOccupation = statistic;
                }
            }

            return statisticWithHighestOccupation;
        }

        public void MonthDaysOccupied(List<StatisticsByMonthDTO> statistics, List<Reservation> reservations,int year)
        {
            foreach (Reservation reservation in reservations)
            {
                StatisticsByMonthDTO statisticsByMonthDTO = Month(statistics, reservation.ReservationDateRange.StartDate.Month,year);

                int daysOccupied = 0;
                int daysOccupied1 = 0;
                if (reservation.Status == ReservationStatus.Finished)
                {
                    if (reservation.ReservationDateRange.EndDate.Year == reservation.ReservationDateRange.StartDate.Year && reservation.ReservationDateRange.EndDate.Month == reservation.ReservationDateRange.StartDate.Month)
                    {
                        TimeSpan duration = reservation.ReservationDateRange.EndDate.Subtract(reservation.ReservationDateRange.StartDate);
                        daysOccupied = duration.Days;
                        statisticsByMonthDTO.DaysOccupied += daysOccupied;
                    }

                    else if (reservation.ReservationDateRange.EndDate.Year != reservation.ReservationDateRange.StartDate.Year || reservation.ReservationDateRange.EndDate.Month != reservation.ReservationDateRange.StartDate.Month)
                    {
                        if(reservation.ReservationDateRange.StartDate.Month == 12 && reservation.ReservationDateRange.EndDate.Month !=12)
                        {
                            if(reservation.ReservationDateRange.StartDate.Year == year)
                            {
                                DateTime endDate = new DateTime(statisticsByMonthDTO.Year + 1, 1, 1);
                                TimeSpan time = endDate.Subtract(reservation.ReservationDateRange.StartDate);
                                daysOccupied = time.Days;
                                statisticsByMonthDTO.DaysOccupied += daysOccupied;
                            }
                            else
                            {
                                StatisticsByMonthDTO statisticsByMonthDTONextYear = Month(statistics, 1, year);
                                DateTime firstDayOfJanuary = new DateTime(statisticsByMonthDTONextYear.Year, 1, 1);
                                TimeSpan time1 = reservation.ReservationDateRange.EndDate.Subtract(firstDayOfJanuary);
                                daysOccupied1 = time1.Days;
                                statisticsByMonthDTONextYear.DaysOccupied+= daysOccupied1;

                            }

                        }
                        else
                        {
                            int month = statisticsByMonthDTO.Month;
                            DateTime endDateMonth = new DateTime(statisticsByMonthDTO.Year, month + 1, 1);
                            TimeSpan duration = endDateMonth.Subtract(reservation.ReservationDateRange.StartDate);
                            daysOccupied = duration.Days;
                            statisticsByMonthDTO.DaysOccupied += daysOccupied;

                            StatisticsByMonthDTO statisticsByMonthDTONextMonth = Month(statistics, month + 1, year);
                            int month1 = statisticsByMonthDTONextMonth.Month;
                            DateTime firstDayOfMonth = new DateTime(statisticsByMonthDTONextMonth.Year, month1, 1);
                            TimeSpan duration1 = reservation.ReservationDateRange.EndDate.Subtract(firstDayOfMonth);
                            daysOccupied1 = duration1.Days;
                            statisticsByMonthDTONextMonth.DaysOccupied += daysOccupied1;
                        }
                       
                    }

                }
            }
        }

    }
}
