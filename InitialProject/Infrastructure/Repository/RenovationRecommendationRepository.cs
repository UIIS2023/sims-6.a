using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Infrastructure.Repository
{
    public class RenovationRecommendationRepository : IRenovationRecommendationRepository
    {
        private const string FilePath = "../../../Infrastructure/Resources/Data/renovationRecommendations.txt";
        private readonly Serializer<RenovationRecommendation> _serializer;
        private List<RenovationRecommendation> _renovationRecommendations;

        public RenovationRecommendationRepository()
        {
            _serializer = new Serializer<RenovationRecommendation>();
            _renovationRecommendations = new List<RenovationRecommendation>();
        }

        public List<RenovationRecommendation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public RenovationRecommendation Save(RenovationRecommendation recommendation)
        {
            recommendation.RenovationId = NextId();
            _renovationRecommendations.Add(recommendation);
            _serializer.ToCSV(FilePath, _renovationRecommendations);
            return recommendation;
        }

        public List<RenovationRecommendation> GetAllRecommendationByOwnerId(int ownerId)
        {
            _renovationRecommendations = GetAll();
            return _renovationRecommendations.Where(r => r.OwnerId == ownerId).ToList();
        }

        public List<RenovationRecommendation> GetAllRecommendationsByAccommodationId(int accommodationId)
        {
            _renovationRecommendations = GetAll();
            return _renovationRecommendations.Where(r => r.AccommodationId == accommodationId).ToList();
        }

        public int NextId()
        {
            _renovationRecommendations = GetAll();
            if (_renovationRecommendations.Count < 1)
            {
                return 1;
            }
            return _renovationRecommendations.Max(rr => rr.RenovationId) + 1;
        }

        public void Delete(RenovationRecommendation recommendation)
        {
            _renovationRecommendations = GetAll();
            RenovationRecommendation foundedRecommendation = _renovationRecommendations.Find(rr => rr.RenovationId == recommendation.RenovationId);
            _renovationRecommendations.Remove(foundedRecommendation);
            _serializer.ToCSV(FilePath, _renovationRecommendations);
        }


    }
}
