using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class ReservationDTOService : IReservationDTOService
    {
        private readonly int _userId;
        private readonly IReservationService _reservationService;
        private readonly IAccommodationService _accommodationService;
        private readonly IUserService _userService;
        public ReservationDTOService()
        {
            _reservationService = Injector.CreateInstance<IReservationService>();
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            _userService = Injector.CreateInstance<IUserService>();
            _userId = _userService.GetUserId();
        }
        public List<ReservationDTO> GetPastReservationsByUser()
        {
            List<Reservation> reservations = _reservationService.GetFinishedReservationsByUser(_userId);
            List<ReservationDTO> result = new List<ReservationDTO>();
            foreach (Reservation reservation in reservations)
            {
                Accommodation accommodation = _accommodationService.GetAccommodationByReservationId(reservation.ReservationId);
                ReservationDTO reservationDTO = new ReservationDTO(accommodation.Name, accommodation.Location, reservation.ReservationDateRange);
                result.Add(reservationDTO);
            }
            return SortResult(result);
        }

        public List<ReservationDTO> GetUpcomingReservationsByUser()
        {
            List<Reservation> reservations = _reservationService.GetUpcomingReservationsByUser(_userId);
            List<ReservationDTO> result = new List<ReservationDTO>();
            foreach (Reservation reservation in reservations)
            {
                Accommodation accommodation = _accommodationService.GetAccommodationByReservationId(reservation.ReservationId);
                ReservationDTO reservationDTO = new ReservationDTO(accommodation.Name, accommodation.Location, reservation.ReservationDateRange);
                result.Add(reservationDTO);
            }
            return SortResult(result);
        }
        private static List<ReservationDTO> SortResult(List<ReservationDTO> result)
        {
            return result.OrderBy(r => r.DateTimeCheckIn).ToList();
        }
    }
}
