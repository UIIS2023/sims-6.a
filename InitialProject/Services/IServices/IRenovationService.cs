using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IRenovationService
    {
        ObservableCollection<DateRange> GetAvailableDates(Accommodation selectedAccommodation, DateTime startDate, DateTime endDate, int Days);
        Renovation CreateNewRenovation(string AccommodationName, int AccommodationId, DateRange dateRange, string Description);
        void SaveRenovation(Renovation renovation);
        List<Renovation> GetScheduledRenovationsByOwnerId(int ownerId);
        List<Renovation> GetFinishedRenovationsByOwnerId(int ownerId);
        void IsRenovationFinished();
        void DeleteRenovation(Renovation renovation);
        bool isCancelationPeriodExpired(Renovation renovation);
        public void RecentlyRenovated();
    }
}
