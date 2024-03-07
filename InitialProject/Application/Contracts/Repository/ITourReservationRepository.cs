namespace InitialProject.Aplication.Contracts.Repository
{
    using InitialProject.Domen.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITourReservationRepository
    {
        List<TourReservation> GetAll();
        TourReservation Save(TourReservation tourReservation);
        int NextId();

        void Delete(TourReservation reservation);

    }
}
