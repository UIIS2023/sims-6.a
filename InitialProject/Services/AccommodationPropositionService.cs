using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class AccommodationPropositionService
    {
        private readonly AccommodationStatisticsService _accommodationStatisticService;
        private readonly ReservationService _reservationService;
        private readonly AccommodationService _accommodationService;
        private readonly AccommodationReservationService _accommodationReservationService;


        public AccommodationPropositionService()
        {
            _accommodationStatisticService = new AccommodationStatisticsService();
            _reservationService = new ReservationService();
            _accommodationService = new AccommodationService();
            _accommodationReservationService = new AccommodationReservationService();
        }


        public int AccommodationWithMostReservations(int userId)
        {
            int maxNumberOfReservations = 0;
            int accommodationId = 0;

            List<Accommodation> allUserAccommodation = _accommodationService.GetAccommodationsByOwnerId(userId);
            foreach(Accommodation accommodation in allUserAccommodation)
            {
                List<int> reservationIds= _accommodationReservationService.GetReservationsIdsByAccommodationId(accommodation.AccommodationID);
                if(reservationIds.Count() > maxNumberOfReservations)
                {
                    maxNumberOfReservations = reservationIds.Count();
                    accommodationId = accommodation.AccommodationID;
                }
            }
            return accommodationId;
        }

        public int AccommodationWithLeastReservations(int userId)
        {
            int minNumberOfReservations = int.MaxValue;
            int accommodationId = 0;

            List<Accommodation> allUserAccommodation = _accommodationService.GetAccommodationsByOwnerId(userId);
            foreach (Accommodation accommodation in allUserAccommodation)
            {
                List<int> reservationIds = _accommodationReservationService.GetReservationsIdsByAccommodationId(accommodation.AccommodationID);
                if (reservationIds.Count() < minNumberOfReservations)
                {
                    minNumberOfReservations = reservationIds.Count();
                    accommodationId = accommodation.AccommodationID;
                }
            }
            return accommodationId;
        }

        public int HotAccommodationStatistics(int userId)
        {
            int accommodationid = 0;
            List<Accommodation> userAccommodation = _accommodationService.GetAccommodationsByOwnerId(userId);
            int max = 0;
            foreach (Accommodation accommodation in userAccommodation)
            {
                List<StatisticsByYearDTO> statistics = _accommodationStatisticService.YearStatisticsForAccommodation(accommodation.AccommodationID);
                int number =TodayYearHotStatistic(statistics);
                if (number> max)
                {
                    max = number;
                    accommodationid = accommodation.AccommodationID;
                }
            }
            return accommodationid;
            
        }

        public int TodayYearHotStatistic(List<StatisticsByYearDTO> statistics)
        {
            int maxDaysOccupied = 0;
            foreach(StatisticsByYearDTO sby in statistics)
            {
                if(sby.Year == DateTime.Now.Year)
                {
                   if(sby.DaysOccupied > maxDaysOccupied)
                   {
                        maxDaysOccupied = sby.DaysOccupied;
                   }
                }
            }
            return maxDaysOccupied;
        }


        public int ColdAccommodationStatistics(int userId)
        {
            int accommodationid = 0;
            List<Accommodation> userAccommodation = _accommodationService.GetAccommodationsByOwnerId(userId);
            int min = int.MaxValue;
            foreach (Accommodation accommodation in userAccommodation)
            {
                List<StatisticsByYearDTO> statistics = _accommodationStatisticService.YearStatisticsForAccommodation(accommodation.AccommodationID);
                int number = TodayYearColdStatistic(statistics);
                if (number < min)
                {
                    min = number;
                    accommodationid = accommodation.AccommodationID;
                }
            }
            return accommodationid;

        }

        public int TodayYearColdStatistic(List<StatisticsByYearDTO> statistics)
        {
            int minDaysOccupied = int.MaxValue;
            foreach (StatisticsByYearDTO sby in statistics)
            {
                if (sby.Year == DateTime.Now.Year)
                {
                    if (sby.DaysOccupied > minDaysOccupied)
                    {
                        minDaysOccupied = sby.DaysOccupied;
                    }
                }
            }
            return minDaysOccupied;
        }


    }

}
