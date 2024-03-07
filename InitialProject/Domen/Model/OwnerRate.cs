using InitialProject.Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Domen.Model
{
    public class OwnerRate : ISerializable
    {
        public int OwnerRateId { get; set; }
        public int OwnerId { get; set; }
        public int AccommodationId { get; set; }
        public int UserId { get; set; }
        public int HygieneRate { get; set; }
        public int CorrectnessRate { get; set; }
        public string? Comment { get; set; }
        public List<string>? Images { get; set; }
        public OwnerRate(int userId, int ownerId, int accommodationId, int cleanlinessRate, int correctnessRate, string? comment, List<string>? images)
        {
            this.UserId = userId;
            this.OwnerId = ownerId;
            AccommodationId = accommodationId;
            HygieneRate = cleanlinessRate;
            CorrectnessRate = correctnessRate;
            Comment = comment;
            Images = images;
        }
        public OwnerRate()
        {
            UserId = -1;
            OwnerId = -1;
            AccommodationId = -1;
            HygieneRate = 1;
            CorrectnessRate = 1;
            Comment = "";
            Images = new List<string>();
        }
        public string[] ToCSV()
        {

            string[] csvValues = {
                OwnerRateId.ToString(),
                OwnerId.ToString(),
                UserId.ToString(),
                AccommodationId.ToString(),
                HygieneRate.ToString(),
                CorrectnessRate.ToString(),
                Comment,
                ImagesListToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            OwnerRateId = Convert.ToInt32(values[0]);
            OwnerId = Convert.ToInt32(values[1]);
            UserId = Convert.ToInt32(values[2]);
            AccommodationId = Convert.ToInt32(values[3]);
            HygieneRate = Convert.ToInt32(values[4]);
            CorrectnessRate = Convert.ToInt32(values[5]);
            Comment = values[6];
            Images = StringToImagesList(values[7]);
        }
        private string ImagesListToString()
        {
            string s = "";
            foreach (string image in Images)
            {
                if (Images.IndexOf(image) == Images.Count - 1)
                {
                    s += image;
                }
                else
                {
                    s += image + ";";
                }
            }
            return s;
        }
        private List<string> StringToImagesList(string s)
        {
            return new List<string>(s.Split(";"));
        }
    }
}
