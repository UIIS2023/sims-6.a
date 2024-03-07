using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Services.IServices
{
    public interface ITourStatisticsService
    {
        List<string> GetAllYears();

        List<FilteredTourRequestStatistics> FilterData(FilterStatisticDTO selectedData);

        Location GetMostPopularLocation();

        Language GetMostPopularLanguage();
        Statistic GetSpecificStatistic(string tour);
        Tour GetMostVisitedTour(string year);
    }
}