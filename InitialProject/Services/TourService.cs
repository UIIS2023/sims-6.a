namespace InitialProject.Services
{
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Services.IServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TourService : ITourService
    {
        private readonly ITourRepository _tourRepository;
        private readonly ITourAttendanceService _tourAttendanceService;
        private readonly ITourPointService _tourPointService;

        public TourService()
        {
            _tourRepository = Injector.CreateInstance<ITourRepository>();
            _tourAttendanceService = Injector.CreateInstance<ITourAttendanceService>();
            _tourPointService = Injector.CreateInstance<ITourPointService>();
        }

        public List<Tour> GetAll()
        {
            return _tourRepository.GetAll();
        }

        public List<Tour> GetAllNotStartedTours()
        {
            return _tourRepository.GetAll().Where(t => t.TourStarted == false).ToList();
        }

        public Tour GetTourById(int id)
        {
            return _tourRepository.GetAll().Where(t => t.TourId == id).FirstOrDefault();
        }

        public List<Tour> GetSimilarAsTourHasFullCapacity(string country, string city)
        {
            return _tourRepository.GetAll().Where(a => (a.Location.Country == country) && (a.Location.City == city) && (a.MaxGuestNumber != 0)).ToList();
        }

        public void ReduceMaxGuestNumber(int tourId, int guestNumber)
        {
            Tour tour = _tourRepository.GetAll().Find(t => t.TourId == tourId);
            tour.MaxGuestNumber -= guestNumber;
            _tourRepository.Update(tour);
        }

        public List<Tour> GetAllFinished(int userId)
        {
            List<TourAttendance> _tourAttendance = _tourAttendanceService.GetAllPresented(userId);
            List<Tour> tours = new List<Tour>();
            foreach (TourAttendance t in _tourAttendance)
            {
                if (!tours.Contains(_tourRepository.GetAll().Find(tour => tour.TourId == t.TourId)) && HasFinished(t.TourId))
                    tours.Add(_tourRepository.GetAll().Find(tour => tour.TourId == t.TourId));
            }
            return tours;
        }

        public bool HasFinished(int tourId)
        {
            foreach (TourPoint tourPoint in _tourPointService.GetAllTourPoints().Where(t => t.TourId == tourId))
            {
                if (tourPoint.CurrentStatus == TourPoint.Status.Active || tourPoint.CurrentStatus == TourPoint.Status.NotActive)
                {
                    return false;
                }

            }
            return true;
        }

        public List<Tour> GetAllNotFinishedTour()
        {
            List<Tour> tours = new List<Tour>();
            tours = _tourRepository.GetAll().Where(t => t.TourStarted == false).ToList();
            foreach (Tour tour in _tourRepository.GetAll().Where(t => t.TourStarted == true).ToList())
                if (!_tourPointService.TourStartedAndFinished(tour.TourId))
                {
                    tours.Add(tour);
                }
            return tours;
        }

        public List<Tour> GetAllFinishedTours()
        {
            return _tourRepository.GetAll().Where(t => t.TourStarted == true).ToList();
        }

        public List<Tour> GetAllFiltered(string city, string country, string durationFrom, string durationTo, string language, string guestNumber)
        {
            List<Tour> toursFiltered = new List<Tour>();
            foreach (Tour tour in GetAllNotStartedTours())
            {
                if (CityFilter(tour, city) && CountryFilter(tour, country) && DurationFilter(tour, durationFrom, durationTo) && LanguageFilter(tour, language) && GuestNumberFilter(tour, guestNumber))
                {
                    toursFiltered.Add(tour);
                }
            }
            return toursFiltered;
        }

        bool CountryFilter(Tour tour, string country)
        {
            return (string.IsNullOrEmpty(country) || tour.Location.Country == country);
        }

        bool CityFilter(Tour tour, string city)
        {
            return (string.IsNullOrEmpty(city) || tour.Location.City == city);
        }

        bool GuestNumberFilter(Tour tour, string guestNumber)
        {
            return (string.IsNullOrEmpty(guestNumber) || tour.MaxGuestNumber >= Convert.ToInt32(guestNumber));
        }

        bool DurationFilter(Tour tour, string durationFrom, string durationTo)
        {
            return (string.IsNullOrEmpty(durationFrom) || tour.Duration >= Convert.ToInt32(durationFrom)) &&
                    (string.IsNullOrEmpty(durationTo) || tour.Duration <= Convert.ToInt32(durationTo));
        }

        bool LanguageFilter(Tour tour, string language)
        {
            return (string.IsNullOrEmpty(language) || tour.Language.ToString() == language);
        }

        public Tour Save(Tour tour)
        {
            return _tourRepository.Save(tour);
        }

        public int FindNextId()
        {
            return _tourRepository.NextId();
        }

        public List<DateTime> GetAvailableDates(DateTime startingDate, DateTime endDate)
        {

            List<DateTime> allDates = new();

            for (DateTime date = startingDate; date <= endDate; date = date.AddDays(1))
            {
                allDates.Add(date);
            }

            List<DateTime> occupiedDates = new();
            foreach (var tour in _tourRepository.GetAll())
            {
                occupiedDates.Add(tour.StartingDateTime.Date);  // Only use the date part
            }


            allDates.RemoveAll(date => occupiedDates.Contains(date.Date));

            return allDates;
        }

        private List<Tour> GetAllFinishedToursInYear()
        {
            List<Tour> allFinishedTours = GetAllFinishedTours();
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);

            foreach (var tour in allFinishedTours.ToList())
            {
                if (tour.StartingDateTime < oneYearAgo)
                {
                    allFinishedTours.Remove(tour);
                }
            }

            return allFinishedTours;

        }

        public List<Tour> GetAllFinishedInOneYearByGuide(int guideId)
        {
            List<Tour> tours = new List<Tour>();

            foreach (var tour in GetAllFinishedToursInYear())
                if(tour.GuideId == guideId)
                    tours.Add(tour);

            return tours;
        }

        public int GetGuideIdByTourId(int tourId)
        {
            try
            {
                return _tourRepository.GetAll().FirstOrDefault(t => t.TourId == tourId).GuideId;
            }catch(Exception ex)
            {
                return -1;
            }
        }

    }
}
