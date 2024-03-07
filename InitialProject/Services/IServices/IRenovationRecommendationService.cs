using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IRenovationRecommendationService
    {
        List<RenovationRecommendation> GetAll();
        RenovationRecommendation Save(RenovationRecommendation recommendation);
        List<RenovationRecommendation> GetAllRecommendationByOwnerId(int ownerId);
        List<RenovationRecommendation> GetAllRecommendationsByAccommodationId(int accommodationId);
        void Delete(RenovationRecommendation recommendation);
    }
}
