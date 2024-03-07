using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Aplication.Contracts.Repository
{
    public interface IReservationRepository
    {
        void Delete(Reservation reservation);
        List<Reservation> GetAll();
        Reservation GetByReservationId(int reservationId);
        int NextId();
        Reservation Save(Reservation reservation);
        Reservation Update(Reservation reservation);
    }
}
