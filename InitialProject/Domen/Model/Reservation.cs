using System;
using InitialProject.CustomClasses;
using InitialProject.Domen;

namespace InitialProject.Domen.Model
{
    public enum ReservationStatus { Reserved, CheckedIn, Finished}
    public class Reservation : ISerializable
    {
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        public DateRange ReservationDateRange { get; set; }
        public int NumberOfGuests { get; set; }
        public ReservationStatus Status { get; set; }

        public Reservation()
        {
            UserId = -1;
            ReservationDateRange = new DateRange();
            NumberOfGuests = 0;
            Status = ReservationStatus.Reserved;
        }

        public Reservation(int userId, DateTime startDate, int numberOfVisitors)
        {
            UserId = userId;
            ReservationDateRange = new DateRange(startDate, numberOfVisitors);
            NumberOfGuests = numberOfVisitors;
            Status = ReservationStatus.Reserved;
        }

        public Reservation(DateRange dateRange, int guestNumber, int userId)
        {
            UserId = userId;
            ReservationDateRange = dateRange;
            NumberOfGuests = guestNumber;
            Status = ReservationStatus.Reserved;
        }
        public string[] ToCSV()
        {
            string[] csvValues = {
                ReservationId.ToString(),
                ReservationDateRange.ToString(),
                NumberOfGuests.ToString(),
                UserId.ToString(),
                Status.ToString(),
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            ReservationId = Convert.ToInt32(values[0]);
            ReservationDateRange = ReservationDateRange.fromStringToDateRange(values[1]);
            NumberOfGuests = Convert.ToInt32(values[2]);
            UserId = Convert.ToInt32(values[3]);
            Status = (ReservationStatus)Enum.Parse(typeof(ReservationStatus), values[4]);
        }

    }
}
