using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.Contracts.Repository
{
    public interface IRenovationRecommendationRepository
    {
        List<RenovationRecommendation> GetAll();
        RenovationRecommendation Save(RenovationRecommendation recommendation);
        List<RenovationRecommendation> GetAllRecommendationByOwnerId(int ownerId);
        List<RenovationRecommendation> GetAllRecommendationsByAccommodationId(int accommodationId);
        int NextId();
        void Delete(RenovationRecommendation recommendation);
    }
}
