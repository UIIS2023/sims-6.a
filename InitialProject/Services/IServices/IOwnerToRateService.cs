using InitialProject.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IOwnerToRateService
    {
        List<OwnerToRate> GetOwnersToRate();
        Dictionary<int, string> GetAccommodationNamesByUser(int userId);
        void DeleteOwnerToRate(int accommodationId);
        void DeleteIfFiveDaysPassed();
        void Save(OwnerToRate owner);
    }
}
