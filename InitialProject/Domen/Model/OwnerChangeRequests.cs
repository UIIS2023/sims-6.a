using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace InitialProject.Domen.Model
{
    public class OwnerChangeRequests 
    {
        private int _requestId;
        private int _reservationId;
        private string _accommodationName;
        private DateTime _newStartDate;
        private DateTime _newEndDate;
        private int _ownerId;
        private bool _isDateAvailable;

        public OwnerChangeRequests()
        {

        }

        public OwnerChangeRequests(int requestId, int reservationId, string accommodationName, DateTime newStartDate, DateTime newEndDate, int ownerId, bool isDateAvailable)
        {
            _requestId = requestId;
            _reservationId = reservationId;
            _accommodationName = accommodationName;
            _newStartDate = newStartDate;
            _newEndDate = newEndDate;
            _ownerId = ownerId;
            _isDateAvailable = isDateAvailable;
        }

        public int RequestId { get => _requestId; set => _requestId = value; }
        public int ReservationId { get => _reservationId; set => _reservationId = value; }
        public string AccommodationName { get => _accommodationName; set => _accommodationName = value; }

        public DateTime NewStartDate { get => _newStartDate; set => _newStartDate = value; }

        public DateTime NewEndDate { get => _newEndDate; set => _newEndDate = value; }

        public int OwnerId { get => _ownerId; set => _ownerId = value; }

        public bool IsDateAvailable { get => _isDateAvailable; set => _isDateAvailable = value;  }

       /* public void FromCSV(string[] values)
        {
            _requestId = Convert.ToInt32(values[0]);
            _reservationId = Convert.ToInt32(values[1]);
            _accommodationName = Convert.ToString(values[2]);
            _newStartDate = Convert.ToDateTime(values[3]);
            _newEndDate = Convert.ToDateTime(values[4]);
            _ownerId = Convert.ToInt32(values[6]);
            _isDateAvailable = Convert.ToBoolean(values[7]);
        } 

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                _requestId.ToString(),
                _reservationId.ToString(),
                _accommodationName,
                _newStartDate.ToString(),
                _newEndDate.ToString(),
                _userId.ToString(),
                _ownerId.ToString(),
                _isDateAvailable.ToString()
            };
            return csvValues;
        }
       */
    }
}
