using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IReservationCompletionService
    {
        public void HandleReservationCompletion(int userId, int reservationId);
        public bool CheckIfLeftReservation(Reservation reservation);
    }
}
