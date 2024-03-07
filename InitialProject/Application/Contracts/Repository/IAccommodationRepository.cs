using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Aplication.Contracts.Repository
{
    public interface IAccommodationRepository
    {
        List<Accommodation> GetAll();
        Accommodation Save(Accommodation accommodation);
        List<Location> GetAllLocationsFromAccommodations();
        int NextId();
        int GetLastAccommodationId();
        void Delete(Accommodation accommodation);
        Accommodation Update(Accommodation accommodation);
    }
}
