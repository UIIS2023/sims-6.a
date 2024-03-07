using InitialProject.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Aplication.Contracts.Repository
{
    public interface IOwnerToRateRepository
    {
        List<OwnerToRate> GetAll();
        OwnerToRate Save(OwnerToRate ownerToRate);
        void Delete(OwnerToRate ownerToRate);
    }
}
