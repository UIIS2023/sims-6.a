using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using System.Linq;

namespace InitialProject.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRespository;

        public NotificationService()
        {
            _notificationRespository = Injector.CreateInstance<INotificationRepository>();
        }

        public Notification SaveNotification(Notification notificiation)
        {
            return _notificationRespository.Save(notificiation);
        }

        public bool IsReservationCanceled(int reservationId)
        {
            Notification notification = _notificationRespository.GetAll().Find(r => r.ReservationId == reservationId);
            if(notification != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
