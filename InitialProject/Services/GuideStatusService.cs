using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Infrastructure.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static InitialProject.Domen.Model.GuideStatus;

namespace InitialProject.Services
{
    public class GuideStatusService : IGuideStatusService
    {
        private readonly IGuideStatusRepository guideStatusRepository;
        private readonly ITourService tourService;
        private readonly ITourRateService tourRateService;
        public GuideStatusService()
        {
            guideStatusRepository = Injector.CreateInstance<IGuideStatusRepository>();
            tourService = Injector.CreateInstance<ITourService>();
            tourRateService = Injector.CreateInstance<ITourRateService>();
        }

        public GuideStatus GetStatusByUserId(int id)
        {
            return guideStatusRepository.getAll().Where(c=>c.EmployeeId== id).FirstOrDefault();
        }

        public void UpdateToUnemployed(int id)
        {
            GuideStatus guideStatus = guideStatusRepository.getAll().Where(c => c.EmployeeId == id).FirstOrDefault();
            guideStatus.EmploymentStatus = GuideStatus.Status.Unemployed;
            guideStatusRepository.Update(guideStatus);
        }

        private void UpdateToSuper(int id)
        {
            GuideStatus guideStatus = guideStatusRepository.getAll().Where(c => c.EmployeeId == id).FirstOrDefault();
            guideStatus.EmploymentStatus = GuideStatus.Status.SuperGuide;
            guideStatus.DateOfPromotion = DateTime.Now;
            guideStatusRepository.Update(guideStatus);
        }

        private string CheckGroupLanguage(List<Tour> allFinishedToursByGuide)
        {
            var toursGroupedByLanguage = allFinishedToursByGuide
                .GroupBy(tour => tour.Language.ToString());


            var mostFrequentLanguageGroup = toursGroupedByLanguage
                .OrderByDescending(group => group.Count())
                .FirstOrDefault();

            if (mostFrequentLanguageGroup != null && mostFrequentLanguageGroup.Count() >= 20)
            {
                return mostFrequentLanguageGroup.Key;
            }

            return null;
        }


        private Status GetStatus(int GuideId) 
        {
            return guideStatusRepository.getAll().Where(c => c.EmployeeId == GuideId).FirstOrDefault().EmploymentStatus;
        }

        private void CheckIsPromotionStillActive(int GuideId)
        {
            GuideStatus guideStatus = guideStatusRepository.getAll().Where(c => c.EmployeeId == GuideId).FirstOrDefault();
            if (guideStatus.DateOfPromotion <= DateTime.Now.AddYears(-1))
            {
                guideStatus.EmploymentStatus = Status.Employeed;
                guideStatusRepository.Update(guideStatus);
            }

        }


        public void CheckIfGuideIsSuper(int guideId)
        {
            List<Tour> allFinishedToursByGuide = tourService.GetAllFinishedInOneYearByGuide(guideId);

            Language? language = new Language(CheckGroupLanguage(allFinishedToursByGuide));
            if (language == null)
                return;

            if(GetStatus(guideId) == Status.SuperGuide)
            {
                CheckIsPromotionStillActive(guideId);
                return;
            }

            if (tourRateService.GetAverageRateForGuideInPastYearWithSpecificLanguage(guideId, language) > 9)
                UpdateToSuper(guideId);
        }

    }
}
