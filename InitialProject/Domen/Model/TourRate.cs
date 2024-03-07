namespace InitialProject.Domen.Model
{
    using InitialProject.Domen;
    using System;
    using System.Collections.Generic;

    public class TourRate : ISerializable
    {
        public TourRate() { }
        public int GuestId { get; set; }
        public int TourId { get; set; }
        public int GuideKnowledge { get; set; }
        public int GuideLanguage { get; set; }
        public int TourInterest { get; set; }
        public string? Comment { get; set; }
        public List<string>? Images { get; set; }

        public DateTime? Date { get; set; }

        public bool? IsValid { get; set; }

        public TourRate(int guestId, int tourId, int guideKnowledge, int guideLanguage, int tourInterest, bool isValid, string? comment, List<string>? images)
        {
            GuestId = guestId;
            TourId = tourId;
            GuideKnowledge = guideKnowledge;
            GuideLanguage = guideLanguage;
            TourInterest = tourInterest;
            IsValid = isValid;
            Comment = comment;
            Images = images;
        }

        public string[] ToCSV()
        {

            string[] csvValues = {
                GuestId.ToString(),
                TourId.ToString(),
                GuideKnowledge.ToString(),
                GuideLanguage.ToString(),
                TourInterest.ToString(),
                IsValid.ToString(),
                Comment,
                ImagesListToString(),
                Date.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            GuestId = Convert.ToInt32(values[0]);
            TourId = Convert.ToInt32(values[1]);
            GuideKnowledge = Convert.ToInt32(values[2]);
            GuideLanguage = Convert.ToInt32(values[3]);
            TourInterest = Convert.ToInt32(values[4]);
            IsValid = Convert.ToBoolean(values[5]);
            Comment = values[6];
            Images = StringToImagesList(values[7]);
            Date = DateTime.Parse(values[8]);
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
