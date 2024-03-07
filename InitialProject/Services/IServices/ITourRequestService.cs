namespace InitialProject.Services.IServices
{
    using InitialProject.Domen.CustomClasses;
    using InitialProject.Domen.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITourRequestService
    {
        List<TourRequest> GetAllTourRequests(int userId);

        List<TourRequest> GetAllRequests();

        TourRequest GetTourRequestById(int id);

        void MakeTourRequest(TourRequest tourRequest);

        void Update(TourRequest tourRequest);

        void CheckRequests();

        List<string> GetAllYearsOfTourReqeusts();
        List<TourRequest> FilterRequests(FilterRequests dataToFilter);
        void MakeNotificationsForGuests(Tour tour, int userId);


    }
}
