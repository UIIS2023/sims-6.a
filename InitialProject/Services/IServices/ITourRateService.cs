using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Services.IServices
{
    public interface ITourRateService
    {
        List<TourRate> GetAllRates();
        void MakeTourRate(TourRate tourRate);

        double GetAverageRateForGuideInPastYearWithSpecificLanguage(int guideId, Language language);
        void Update(TourRate tourRate);
    }
}