using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class CheckingInService : ICheckingInService
    {
        private readonly IReservationService _reservationService;
        private readonly IUserService _userService;
        public CheckingInService()
        {
            _reservationService = Injector.CreateInstance<IReservationService>();
            _userService = Injector.CreateInstance<IUserService>();
        }
        public void HandleCheckingIn()
        {
            List<Reservation> reservations = _reservationService.GetUpcomingReservationsByUser(_userService.GetUserId());
            foreach (Reservation reservation in reservations)
            {
                if (HasCheckedIn(reservation))
                {
                    reservation.Status = ReservationStatus.CheckedIn;
                    _userService.UsePoints(_userService.GetUserId());
                    _reservationService.Update(reservation);
                }
            }
        }
        private bool HasCheckedIn(Reservation reservation)
        {
            return reservation.ReservationDateRange.StartDate <= DateTime.Now && reservation.ReservationDateRange.EndDate > DateTime.Now;
        }
    }
}
