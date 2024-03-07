using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.CustomClasses
{
    public class AccommodationReservationDTO
    {
        public int AccommodationId { get; set; }
        public string AccommodationName { get; set; }
        public Location Location { get; set; }
        public AccommodationType TypeOfAccommodation { get; set; }
        public DateRange CheckInOutDates { get; set; } 
        public AccommodationReservationDTO(int accommodationId, string accommodationName, Location location, AccommodationType accommodationType, DateRange dateRange)
        {
            AccommodationId = accommodationId;
            AccommodationName = accommodationName;
            this.Location = location;
            TypeOfAccommodation = accommodationType;
            CheckInOutDates = dateRange;
        }
    }

}
