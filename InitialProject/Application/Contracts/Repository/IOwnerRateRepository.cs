using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Aplication.Contracts.Repository
{
    public interface IOwnerRateRepository
    {
        List<OwnerRate> GetAll();
        OwnerRate Save(OwnerRate ownerRate);
        List<OwnerRate> GetAllRatesByOwner(int ownerId);
        int NextId();
        void Delete(OwnerRate ownerRate);
    }
}
