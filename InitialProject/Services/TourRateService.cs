namespace InitialProject.Services
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Domen.Model;
    using InitialProject.Repository;
    using InitialProject.Services.IServices;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Eco.FrameworkImpl.Ocl;
    using System.Windows;

    public class TourRateService : ITourRateService
    {
        private readonly ITourRateRepository _repository;
        private readonly ITourAttendanceService _tourAttendanceService;
        private readonly ITourService _tourService;

        public TourRateService()
        {
            _repository = Injector.CreateInstance<ITourRateRepository>();
            _tourAttendanceService = Injector.CreateInstance<ITourAttendanceService>();
            _tourService = Injector.CreateInstance<ITourService>();
        }

        public List<TourRate> GetAllRates()
        {
            return _repository.GetAll();
        }

        public void MakeTourRate(TourRate tourRate)
        {
            _repository.Save(tourRate);
            _tourAttendanceService.AddedComment(tourRate.GuestId, tourRate.TourId);
        }

        public void Update(TourRate tourRate)
        {
            _repository.Update(tourRate);
        }

        public double GetAverageRateForGuideInPastYearWithSpecificLanguage(int guideId, Language language)
        {
            List<TourRate> tourRates = GetRatesByGuideId(guideId);
            List<TourRate> tourRatesByLanguage = new();

            foreach (var rate in tourRates)
            {
                if (_tourService.GetTourById(rate.TourId).Language.ToString() == language.ToString())
                {
                    tourRatesByLanguage.Add(rate);
                }
            }


            if (tourRatesByLanguage == null)
            {
                return 0;
            }

            double averageRate = tourRatesByLanguage.Average(rate => (double)(rate.GuideKnowledge + rate.GuideLanguage + rate.TourInterest) / 3);

            return averageRate;
        }


        private List<TourRate> GetRatesByGuideId(int guideId)
        {
            List<TourRate> tourRates = new();
            foreach(var grade in GetGradesInLastYear())
            {
                try
                {
                    if (_tourService.GetGuideIdByTourId(grade.TourId) == guideId)
                    {
                        tourRates.Add(grade);
                    }
                }
                catch(Exception ex)
                {
                    
                }
            }

            return tourRates;
        }

        private List<TourRate> GetGradesInLastYear()
        {
            List<TourRate> allRates = GetAllRates();
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);

            foreach (var tour in allRates.ToList())
            {
                if (tour.Date < oneYearAgo)
                {
                    allRates.Remove(tour);
                }
            }
            return allRates;
        }

    }
}
