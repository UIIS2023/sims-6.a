using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Aplication.Contracts.Repository
{
    public interface ITourRepository
    {
        List<Tour> GetAll();
        int NextId();
        Tour Save(Tour tour);
        Tour Update(Tour tour);
        Tour GetById(int id);
        void Delete(Tour tour);
    }
}