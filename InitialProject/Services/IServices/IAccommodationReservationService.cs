using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IAccommodationReservationService
    {
        Dictionary<int, string> GetReservationsByUserId(int userId);
        bool IsCancellingPossible(DateTime currentDate, int reservationId);
        List<DateRange> GetAvailableDays(int accommodationId, int reservationDays, DateTime startDate, DateTime endDate);
        bool WasOnLocation(int userId, Location location);
        List<int> GetReservationsIdsByAccommodationId(int accommodationId);

        List<int> GetAccommodationIdByReservationIds(int reservationId);
    }
}
