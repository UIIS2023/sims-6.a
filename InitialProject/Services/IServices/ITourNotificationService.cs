namespace InitialProject.Services.IServices
{
    using InitialProject.Presentation.WPF.View.Guest2;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITourNotificationService
    {
        void MakeNotification(int userId, int tourId, Domen.CustomClasses.TourNotification.NotificationType type);
        void NotifyGuest(int userId);
    }
}
