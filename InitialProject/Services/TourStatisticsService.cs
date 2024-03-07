using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class TourStatisticsService : ITourStatisticsService
    {
        private readonly ITourRepository _tourRepository;
        private readonly IUserService userService;
        private readonly ITourReservationService tourReservationService;
        private readonly ITourReservationRepository tourReservationRepository;
        private readonly ITourRequestService tourRequestService;
        public TourStatisticsService()
        {
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
            userService = Injector.CreateInstance<IUserService>();
            tourReservationService = Injector.CreateInstance<ITourReservationService>();
            tourRequestService = Injector.CreateInstance<ITourRequestService>();
        }

        public List<string> GetAllYears()
        {
            List<Tour> tours = _tourRepository.GetAll();
            List<string> years = new();
            foreach (Tour tour in tours)
            {
                if (tour.TourStarted == true)
                {
                    string year = tour.StartingDateTime.Year.ToString();
                    if (!years.Contains(year))
                    {
                        years.Add(year);
                    }
                }
            }
            //check if same year already exist in years 
            return years;
        }

        public Tour GetMostVisitedTour(string year)
        {
            Dictionary<int, int> tourVisits = new Dictionary<int, int>();
            List<TourReservation> reservations = tourReservationRepository.GetAll().Where(c => c.TourId > 0).ToList();

            foreach (TourReservation reservation in reservations)
            {
                Tour tour = _tourRepository.GetById(reservation.TourId);
                if (tour.StartingDateTime.Year.ToString() == year)
                {
                    int reservationId = reservation.TourId;
                    int numberOfGuests = reservation.NumberOfGuests;

                    if (tourVisits.ContainsKey(reservationId))
                    {
                        tourVisits[reservationId] += numberOfGuests;
                    }
                    else
                    {
                        tourVisits.Add(reservationId, numberOfGuests);
                    }
                }
            }

            int tourId = tourVisits.FirstOrDefault(x => x.Value == tourVisits.Values.Max()).Key;
            return _tourRepository.GetById(tourId);

        }

        // body of foreach loop in GetSpecificStatistic method

        private void UpdateAgeStatistics(TourReservation reservation, ref AgeStatistics ageStatistics)
        {
            int age = userService.GetById(reservation.UserId).Age;

            if (age < 18)
            {
                ageStatistics.NumberOfPeopleWithAgeUnder18 += reservation.NumberOfGuests;
            }
            else if (age >= 18 && age <= 50)
            {
                ageStatistics.NumberOfPeopleWithAgeBetween18And50 += reservation.NumberOfGuests;
            }
            else
            {
                ageStatistics.NumberOfPeopleWithAgeOver50 += reservation.NumberOfGuests;
            }
        }

        public Statistic GetSpecificStatistic(string tour)
        {
            List<TourReservation> reservations = tourReservationService.GetAllReservations();
            int tourId = int.Parse(tour.Split(' ')[0]);
            int numberOfPeople = 0;
            int numberOfPeopleWithVoucher = 0;
            AgeStatistics ageStatistics = new AgeStatistics();

            foreach (TourReservation reservation in reservations)
            {
                if (reservation.TourId == tourId)
                {
                    numberOfPeople += reservation.NumberOfGuests;
                    if (reservation.VoucherId != 0)
                    {
                        numberOfPeopleWithVoucher += reservation.NumberOfGuests;
                    }

                    UpdateAgeStatistics(reservation, ref ageStatistics);
                }
            }

            return calculateStatistic(numberOfPeople, numberOfPeopleWithVoucher, ageStatistics);
        }


        //made AgeStatistic class to make code more readable and to avoid passing 3 parameters to calculateStatistic method
        private static Statistic calculateStatistic(int numberOfPeople, int numberOfPeopleWithVoucher, AgeStatistics ageStatistics)
        {
            int numberOfPeopleWithAgeUnder18 = ageStatistics.NumberOfPeopleWithAgeUnder18;
            int numberOfPeopleWithAgeBetween18And50 = ageStatistics.NumberOfPeopleWithAgeBetween18And50;
            int numberOfPeopleWithAgeOver50 = ageStatistics.NumberOfPeopleWithAgeOver50;

            double percentageWithVoucher = 0;
            double percentageWithoutVouchers = 0;
            if (numberOfPeople > 0)
            {
                double percentage = (double)100 / numberOfPeople;
                percentageWithVoucher = percentage * numberOfPeopleWithVoucher;
                percentageWithoutVouchers = 100 - percentageWithVoucher;
            }

            Statistic statistic = new(numberOfPeopleWithAgeUnder18, numberOfPeopleWithAgeBetween18And50, numberOfPeopleWithAgeOver50, percentageWithVoucher, percentageWithoutVouchers);
            return statistic;
        }

        public List<FilteredTourRequestStatistics> FilterData(FilterStatisticDTO selectedData)
        {
            var filteredRequests = FilterRequests(selectedData);
            List<FilteredTourRequestStatistics> statistic = new();

            if (string.IsNullOrEmpty(selectedData.Year))
            {
                var yearlyStatistics = GetYearlyStatistics(filteredRequests, selectedData);

                foreach (var data in yearlyStatistics)
                {
                    var filtered = new FilteredTourRequestStatistics();
                    filtered.Year = data.Year;

                    var values = new List<int> { data.NumberOfRequestsCity, data.NumberOfRequestsCountry, data.NumberOfRequestsLanguage };
                    values.RemoveAll(item => item == 0);
                    filtered.NumberOfTourRequests = values.Any() ? values.Min() : 0;

                    filtered.Month = data.Month;
                    statistic.Add(filtered);
                }
            }
            else if (int.TryParse(selectedData.Year, out int selectedYear))
            {
                var monthlyStatistics = GetMonthlyStatistics(filteredRequests, selectedData, selectedYear);

                foreach (var data in monthlyStatistics)
                {
                    var filtered = new FilteredTourRequestStatistics();
                    filtered.Year = data.Year;
                    filtered.Month = data.Month;

                    var values = new List<int> { data.NumberOfRequestsCity, data.NumberOfRequestsCountry, data.NumberOfRequestsLanguage };
                    values.RemoveAll(item => item == 0);
                    filtered.NumberOfTourRequests = values.Any() ? values.Min() : 0;

                    statistic.Add(filtered);
                }
            }

            return statistic;
        }


        private List<TourRequest> FilterRequests(FilterStatisticDTO selectedData)
        {
            var allRequests = tourRequestService.GetAllRequests();

            return allRequests.Where(r => (string.IsNullOrEmpty(selectedData.Country) || r.Location.Country == selectedData.Country)
                                            && (string.IsNullOrEmpty(selectedData.City) || r.Location.City == selectedData.City)
                                            && (string.IsNullOrEmpty(selectedData.Language) || r.Language.Name == selectedData.Language)).ToList();
        }

        private List<TourRequestPerYear> GetYearlyStatistics(List<TourRequest> requests, FilterStatisticDTO selectedData)
        {
            var yearlyStatistics = new List<TourRequestPerYear>();

            var groupedByYear = requests.GroupBy(r => r.StartingDate.Year);
            foreach (var yearGroup in groupedByYear)
            {
                yearlyStatistics.Add(new TourRequestPerYear
                {
                    Year = yearGroup.Key,
                    NumberOfRequestsLanguage = yearGroup.Count(r => r.Language.Name == selectedData.Language),
                    NumberOfRequestsCity = yearGroup.Count(r => r.Location.City == selectedData.City),
                    NumberOfRequestsCountry = yearGroup.Count(r => r.Location.Country == selectedData.Country)
                });
            }

            return yearlyStatistics;
        }


        private List<TourRequestPerYear> GetMonthlyStatistics(List<TourRequest> requests, FilterStatisticDTO selectedData, int selectedYear)
        {
            var monthlyStatistics = new List<TourRequestPerYear>();

            var selectedYearGroup = requests.Where(r => r.StartingDate.Year == selectedYear);

            var groupedByMonth = selectedYearGroup.GroupBy(r => r.StartingDate.Month);
            foreach (var monthGroup in groupedByMonth)
            {
                monthlyStatistics.Add(new TourRequestPerYear
                {
                    Year = selectedYear,
                    Month = monthGroup.Key,
                    NumberOfRequestsLanguage = monthGroup.Count(r => r.Language.Name == selectedData.Language),
                    NumberOfRequestsCity = monthGroup.Count(r => r.Location.City == selectedData.City),
                    NumberOfRequestsCountry = monthGroup.Count(r => r.Location.Country == selectedData.Country)
                });
            }

            return monthlyStatistics;
        }

        public Location GetMostPopularLocation()
        {
            List<TourRequest> AllTourRequests = tourRequestService.GetAllRequests();

            return AllTourRequests.GroupBy(r => r.Location)
                                  .OrderByDescending(g => g.Count())
                                  .FirstOrDefault()?.Key;
        }

        public Language GetMostPopularLanguage()
        {
            List<TourRequest> AllTourRequests = tourRequestService.GetAllRequests();

            return AllTourRequests.GroupBy(r => r.Language)
                                  .OrderByDescending(g => g.Count())
                                  .FirstOrDefault()?.Key;
        }


    }
}

