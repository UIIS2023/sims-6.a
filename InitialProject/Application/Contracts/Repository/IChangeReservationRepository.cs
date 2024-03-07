using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Aplication.Contracts.Repository
{
    public interface IChangeReservationRepository
    {
        List<ChangeReservationRequest> GetAll();
        ChangeReservationRequest Save(ChangeReservationRequest request);
        void Delete(ChangeReservationRequest request);
        ChangeReservationRequest Update(ChangeReservationRequest request);
    }
}
