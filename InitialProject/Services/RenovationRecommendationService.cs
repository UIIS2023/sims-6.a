using InitialProject.Aplication.Factory;
using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Infrastructure.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class RenovationRecommendationService : IRenovationRecommendationService
    {
        private readonly IRenovationRecommendationRepository _repository;
        public RenovationRecommendationService()
        {
            _repository = Injector.CreateInstance<IRenovationRecommendationRepository>();
        }
        public void Delete(RenovationRecommendation recommendation)
        {
            _repository.Delete(recommendation);
        }

        public List<RenovationRecommendation> GetAll()
        {
            return _repository.GetAll();
        }

        public List<RenovationRecommendation> GetAllRecommendationByOwnerId(int ownerId)
        {
            return _repository.GetAllRecommendationByOwnerId(ownerId);
        }

        public List<RenovationRecommendation> GetAllRecommendationsByAccommodationId(int accommodationId)
        {
            return _repository.GetAllRecommendationsByAccommodationId(accommodationId);
        }

        public RenovationRecommendation Save(RenovationRecommendation recommendation)
        {
            return _repository.Save(recommendation); 
        }
    }
}
