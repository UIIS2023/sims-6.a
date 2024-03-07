using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Domen.Model
{
    public class UserReservationCounter : ISerializable
    {
        public int UserId { get; set; }
        public int ReservationCount { get; set; }
        public DateTime InitialDate { get; set; }
        public UserReservationCounter(int userId, int reservationCount, DateTime initialDate)
        {
            UserId = userId;
            ReservationCount = reservationCount;
            InitialDate = initialDate;
        }
        public UserReservationCounter()
        {
            UserId = -1;
            ReservationCount = 0;
            InitialDate= DateTime.Now;
        }
        public string[] ToCSV()
        {
            string[] csvValues = {
                UserId.ToString(),
                ReservationCount.ToString(),
                InitialDate.ToString(),
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            UserId = Convert.ToInt32(values[0]);
            ReservationCount = Convert.ToInt32(values[1]);
            InitialDate = Convert.ToDateTime(values[2]);
        }
    }
}
