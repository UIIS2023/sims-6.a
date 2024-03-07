namespace InitialProject.Application.Contracts.Repository
{
    using InitialProject.Domen.CustomClasses;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITourNotificationRepository
    {
        List<TourNotification> GetAll();
        List<TourNotification> GetAllTourNotificationsForUser(int userId);
        TourNotification Save(TourNotification notification);
        void Delete(TourNotification notification);
    }
}
