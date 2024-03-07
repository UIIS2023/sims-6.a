using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.Contracts.Repository
{
    public interface IUserReservationCounterRepository
    {
        List<UserReservationCounter> GetAll();
        UserReservationCounter Save(UserReservationCounter user);
        void Delete(UserReservationCounter user);
        UserReservationCounter Update(UserReservationCounter user);
    }
}
