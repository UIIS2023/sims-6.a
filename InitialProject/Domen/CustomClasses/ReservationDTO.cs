using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.CustomClasses
{
    public class ReservationDTO
    {
        public string AccommodationName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string CheckInDate { get; set; }
        public DateTime DateTimeCheckIn { get; set; }
        public string CheckOutDate { get; set; }
        public ReservationDTO(string accommodationName, Location location, DateRange dateRange)
        {
            AccommodationName = accommodationName;
            Country = location.Country;
            City = location.City;
            CheckInDate = string.Format("{0:dd.MM.yyyy.}", dateRange.StartDate);
            CheckOutDate = string.Format("{0:dd.MM.yyyy.}", dateRange.EndDate);
            DateTimeCheckIn = dateRange.StartDate;
        }
    }
}
