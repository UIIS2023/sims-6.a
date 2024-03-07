using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IOwnerRateService
    {
        List<OwnerRate> GetAll();
        void SaveRate(OwnerRate ownerRate);
        List<OwnerRate> RatingsFromRatedGuest(int ownerId);
        bool IsSuperOwner(int ownerId);
        List<OwnerRate> GetRatesByUserId(int userId);
    }
}
