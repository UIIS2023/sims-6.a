namespace InitialProject.Services
{
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Aplication.Factory;
    using InitialProject.Application.Contracts.Repository;
    using InitialProject.Domen.CustomClasses;
    using InitialProject.Presentation.WPF.View.Guest2;
    using InitialProject.Services.IServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TourNotificationService : ITourNotificationService
    {
        private readonly ITourNotificationRepository _repository;

        public TourNotificationService()
        {
            _repository = Injector.CreateInstance<ITourNotificationRepository>();
        }

        public void NotifyGuest(int userId)
        {
            var lista = _repository.GetAllTourNotificationsForUser(userId);
            foreach (var notification in lista)
            {
                if (notification.Type == TourNotification.NotificationType.StatisticTour) { 
                    TourNotificationView tourNotification = new TourNotificationView(notification);
                    _repository.Delete(notification);
                    tourNotification.ShowDialog();
                } 
                else
                {
                    TourRequestNotificationView tourNotification = new TourRequestNotificationView(notification);
                    _repository.Delete(notification);
                    tourNotification.ShowDialog();
                }
                
            }
        }

        public void MakeNotification(int userId, int tourId, TourNotification.NotificationType type)
        {
            Domen.CustomClasses.TourNotification notification = new TourNotification(userId, tourId, type);
            _repository.Save(notification);
        }
    }
}
