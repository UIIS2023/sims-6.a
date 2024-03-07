using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IUserReservationCounterService
    {
        void UpdateReservationCounter(int userId);
        void InitializeReservationCounter(int userId);
        UserReservationCounter GetReservationCounterByUserId(int userId);

        void CountTourReservations(int userId, int number);
    }
}
