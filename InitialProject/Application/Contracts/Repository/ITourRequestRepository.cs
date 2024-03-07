namespace InitialProject.Aplication.Contracts.Repository
{
    using InitialProject.Domen.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITourRequestRepository
    {
        List<TourRequest> GetAll();
        int NextId();
        TourRequest Save(TourRequest tourRequest);
        TourRequest Update(TourRequest tourRequest);
        TourRequest GetById(int id);
        void Delete(TourRequest tourRequest);
    }
}
