using InitialProject.Domen;
using System;

namespace InitialProject.CustomClasses
{
    public class AccommodationReservation: ISerializable
    {
        public int AccommodationId { get; set; }
        public int ReservationId { get; set; }
        public AccommodationReservation(int accommodationId, int reservationId)
        {
            AccommodationId = accommodationId;
            ReservationId = reservationId;
        }
        public AccommodationReservation()
        { }

        public string[] ToCSV()
        {
            string[] csvValues = {
                AccommodationId.ToString(),
                ReservationId.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            AccommodationId = Convert.ToInt32(values[0]);
            ReservationId = Convert.ToInt32(values[1]);
        }
    }
}
