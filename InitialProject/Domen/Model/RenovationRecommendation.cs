using InitialProject.Domen.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.Model
{
    public class RenovationRecommendation : ISerializable
    {
        public int RenovationId { get; set; }
        public int OwnerId { get; set; }
        public int AccommodationId { get; set; }
        public int UserId { get; set; }
        public string RenovationLevel { get; set; }
        public string Comment { get; set; }
        public DateTime RecommendationDate { get; set; }

        public RenovationRecommendation()
        {
            OwnerId = -1;
            AccommodationId = -1;
            UserId = -1;
            RenovationLevel = "";
            Comment = "";
        }

        public RenovationRecommendation(int ownerId, int accommodationId, int userId, string renovationLevel, string comment)
        {
            OwnerId = ownerId;
            AccommodationId = accommodationId;
            UserId = userId;
            RenovationLevel = renovationLevel;
            Comment = comment;
            RecommendationDate = DateTime.Now;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                RenovationId.ToString(),
                OwnerId.ToString(),
                AccommodationId.ToString(),
                UserId.ToString(),
                RenovationLevel,
                Comment,
                RecommendationDate.ToString(),
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            RenovationId = Convert.ToInt32(values[0]);
            OwnerId = Convert.ToInt32(values[1]);
            AccommodationId = Convert.ToInt32(values[2]);
            UserId = Convert.ToInt32(values[3]);
            RenovationLevel = values[4];
            Comment = values[5];
            RecommendationDate = Convert.ToDateTime(values[6]);
        }
    }
}
