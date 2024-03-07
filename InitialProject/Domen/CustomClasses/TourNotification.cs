namespace InitialProject.Domen.CustomClasses
{
    using InitialProject.CustomClasses;
    using InitialProject.Domen.Model;
    using Microsoft.VisualStudio.Services.ClientNotification;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using static InitialProject.Domen.CustomClasses.CreationType;

    public class TourNotification : ISerializable
    {
        public enum NotificationType { TourRequest, StatisticTour}
        public int UserId { get; set; }
        public int TourId { get; set; }
        public NotificationType Type { get; set; }

        public TourNotification()
        {
        }

        public TourNotification(int userId, int tourId, NotificationType type)
        {
            UserId = userId;
            TourId = tourId;
            Type = type;
        }

        public string[] ToCSV()
        {
            string[] cssValues =
            {
                UserId.ToString(),
                TourId.ToString(),
                Type.ToString(),
              
            };
            return cssValues;
        }
        public void FromCSV(string[] values)
        {
            UserId = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            Type = (NotificationType)Enum.Parse(typeof(NotificationType), values[2]);

        }
    }
}
