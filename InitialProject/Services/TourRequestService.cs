namespace InitialProject.Services
{
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.CustomClasses;
    using InitialProject.Domen.Model;
    using InitialProject.Repository;
    using InitialProject.Services.IServices;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.TeamFoundation.WorkItemTracking.Process.WebApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

    public class TourRequestService : ITourRequestService
    {
        private readonly ITourRequestRepository _repository;
        private readonly ITourNotificationService _tourNotificationService;

        public TourRequestService()
        {
            _repository = Injector.CreateInstance<ITourRequestRepository>();
            _tourNotificationService = Injector.CreateInstance<ITourNotificationService>();
            CheckRequests();
        }

        public void CheckRequests()
        {
            foreach (TourRequest tourRequest in  _repository.GetAll()) 
            { 
                if (tourRequest.StartingDate < DateTime.Now.AddDays(2) && tourRequest.RequestStatus == ComplexTourRequest.Status.OnHold)
                {
                    tourRequest.RequestStatus = ComplexTourRequest.Status.Rejected;
                    _repository.Update(tourRequest);
                }
            }
        }

        public List<TourRequest> GetAllRequests()
        {
            return _repository.GetAll().ToList();
        }


        public List<TourRequest> GetAllTourRequests(int userId)
        {
            return _repository.GetAll().Where(tourRequest => tourRequest.UserId == userId).ToList();
        }

        public List<string> GetAllYearsOfTourReqeusts()
        {
            List<TourRequest> tours = _repository.GetAll();
            List<string> years = new();
            foreach (TourRequest tour in tours)
            {
                if (!years.Contains(tour.StartingDate.Year.ToString()))
                    years.Add(tour.StartingDate.Year.ToString());
            }
            return years;
        }

        public void MakeTourRequest(TourRequest tourRequest)
        {
            _repository.Save(tourRequest);
        }

        public void Update(TourRequest tourRequest)
        {
            _repository.Update(tourRequest);
        }


        public List<TourRequest> FilterRequests(FilterRequests dataToFilter)
        {
            List<TourRequest> allTourRequests = new List<TourRequest>(_repository.GetAll());
            List<TourRequest> FilteredData = new();
            FilteredData = allTourRequests.Where(c =>(dataToFilter.City == null || c.Location.City == dataToFilter.City) &&
                                      (dataToFilter.Country == null || c.Location.Country == dataToFilter.Country) &&
                                      (dataToFilter.NumberOfGuests == null || c.GuestNumber <= dataToFilter.NumberOfGuests) &&
                                      (dataToFilter.Language == null || c.Language.Name == dataToFilter.Language) &&
                                      (dataToFilter.StartingDate == null || c.StartingDate >= dataToFilter.StartingDate) &&
                                      (dataToFilter.EndingDate == null || c.EndingDate <= dataToFilter.EndingDate)).ToList();
            return FilteredData;

        }

        public TourRequest GetTourRequestById(int id)
        {
            return _repository.GetById(id);
        }

        public void MakeNotificationsForGuests(Tour tour, int userId)
        {
            if (tour.CreatedType == CreationType.CreationTourType.CreatedByRequest)
            {
                _tourNotificationService.MakeNotification( userId, tour.TourId, TourNotification.NotificationType.TourRequest);
            }
            else if (tour.CreatedType == CreationType.CreationTourType.CreatedByStatistics)
            {
                MakeNotificationsForStatisticTour(tour);
            }
        }


        private void MakeNotificationsForStatisticTour(Tour tour)
        {
            var lista = _repository.GetAll().Where(t => t.Language.Name == tour.Language.Name).ToList().GroupBy(t => t.UserId);
            foreach (var item in lista)
            {
                if (item.Where(t => t.RequestStatus == ComplexTourRequest.Status.Rejected).Count() == item.Count())
                {
                    _tourNotificationService.MakeNotification(item.ElementAt(0).UserId, tour.TourId, TourNotification.NotificationType.StatisticTour);
                }
            }

            var list = _repository.GetAll().Where(t => t.Location.Country.ToString() == tour.Location.Country.ToString() && t.Location.City.ToString() == tour.Location.City.ToString()).ToList().GroupBy(t => t.UserId);
            foreach (var item in list)
            {
                if (item.Where(t => t.RequestStatus == ComplexTourRequest.Status.Rejected).Count() == item.Count())
                {
                    _tourNotificationService.MakeNotification(item.ElementAt(0).UserId, tour.TourId, TourNotification.NotificationType.StatisticTour);
                }
            }
        }
    }
}
