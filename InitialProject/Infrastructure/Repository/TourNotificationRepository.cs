namespace InitialProject.Infrastructure.Repository
{
    using InitialProject.Application.Contracts.Repository;
    using InitialProject.Domen.CustomClasses;
    using InitialProject.Domen.Model;
    using InitialProject.Serializer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TourNotificationRepository : ITourNotificationRepository
    {

        private const string FilePath = "../../../Infrastructure/Resources/Data/tournotifications.txt";

        private readonly Serializer<TourNotification> _serializer;

        private List<TourNotification> _tourNotifications;


        public TourNotificationRepository()
        {
            _serializer = new Serializer<TourNotification>();
            _tourNotifications = _serializer.FromCSV(FilePath);

        }

        public List<TourNotification> GetAll()
        {
            return _tourNotifications;
        }

        public TourNotification Save(TourNotification tourNotification)
        {
            _tourNotifications = _serializer.FromCSV(FilePath);
            _tourNotifications.Add(tourNotification);
            _serializer.ToCSV(FilePath, _tourNotifications);
            return tourNotification;
        }

        public void Delete(TourNotification tourNotification)
        {
            _tourNotifications = _serializer.FromCSV(FilePath);
            TourNotification founded = _tourNotifications.Find(t => t.UserId == tourNotification.UserId && t.TourId == tourNotification.TourId);
            _tourNotifications.Remove(founded);
            _serializer.ToCSV(FilePath, _tourNotifications);
        }





        public List<TourNotification> GetAllTourNotificationsForUser(int userId)
        {
            return _tourNotifications.Where(t => t.UserId ==  userId).ToList();
        }

      
    }
}
