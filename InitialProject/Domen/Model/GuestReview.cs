using InitialProject.Validation;
using System;

namespace InitialProject.Domen.Model
{
    public class GuestReview :ValidationBase, ISerializable
    {
        public int GuestId { get; set; }
        public int AccommodationId { get; set; }
        public int Hygiene { get; set; }
        public int RuleFollowing { get; set; }
        public string Comment { get; set; }

        public GuestReview()
        {
            GuestId = 0;
            AccommodationId = 0;
            Hygiene = 0;
            RuleFollowing = 0;
            Comment = "";
            //LeavingDay = new DateTime();

        }

        public GuestReview(int id, int accommodationId, int hygieneGrade, int ruleFollowingGrade, string comment)
        {
            GuestId = id;
            AccommodationId = accommodationId;
            Hygiene = hygieneGrade;
            RuleFollowing = ruleFollowingGrade;
            Comment = comment;
            //LeavingDay = date;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                GuestId.ToString(),
                AccommodationId.ToString(),
                Hygiene.ToString(),
                RuleFollowing.ToString(),
                Comment,
                //LeavingDay.ToString()
            };

            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            GuestId = Convert.ToInt32(values[0]);
            AccommodationId = Convert.ToInt32(values[1]);
            Hygiene = Convert.ToInt32(values[2]);
            RuleFollowing = Convert.ToInt32(values[3]);
            Comment = values[4];
            //LeavingDay = Convert.ToDateTime(values[5]);
        }


        public int HygieneGrade
        {
            get => Hygiene;
            set
            {
                if (value != Hygiene)
                {
                    Hygiene = value;
                    OnPropertyChanged("HygieneGrade");
                }
            }
        }

        public int RuleFollowingGrade
        {
            get => RuleFollowing;
            set
            {
                if (value != RuleFollowing)
                {
                    RuleFollowing = value;
                    OnPropertyChanged("RuleFollowingGrade");
                }
            }
        }

        public string OwnerComment
        {
            get => Comment;
            set
            {
                if (value != Comment)
                {
                    Comment = value;
                    OnPropertyChanged("OwnerComment");
                }
            }
        }
        protected override void ValidateSelf()
        {
            if (this.RuleFollowing == 0)
            {
                this.ValidationErrors["RuleFollowingGrade"] = "Please enter a number";
            }
            if(this.Hygiene == 0)
            {
                this.ValidationErrors["HygieneGrade"] = "Please enter a number";
            }
            if (string.IsNullOrWhiteSpace(this.Comment))
            {
                this.ValidationErrors["OwnerComment"] = "Please enter a comment";
            }
        }

    }


}
