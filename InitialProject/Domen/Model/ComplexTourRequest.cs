using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.Model
{
    public class ComplexTourRequest : ISerializable
    {
        public enum Status { OnHold, Accepted, Rejected }
        public int TourId { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string TourName { get; set; }
        public Location Location { get; set; }
        public Language Language { get; set; }
        public string Description { get; set; }
        public int GuestNumber { get; set; } = 1;
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public Status RequestStatus { get; set; } = Status.OnHold;
        public int GuideId { get; set; } = -1;


        public ComplexTourRequest()
        {
            Location = new Location();
            Language = new Language();
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
                TourId.ToString(),
                Id.ToString(),
                UserId.ToString(),
                TourName,
                Location.ToString(),
                Language.ToString(),
                Description,
                GuestNumber.ToString(),
                StartingDate.ToString(),
                EndingDate.ToString(),
                RequestStatus.ToString(), 
                GuideId.ToString()};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            TourId = Convert.ToInt32(values[0]);
            Id = Convert.ToInt32(values[1]);
            UserId = Convert.ToInt32(values[2]);
            TourName = values[3];
            Location = Location.fromStringToLocation(values[4]);
            Language = Language.fromStringToLanguage(values[5]);
            Description = values[6];
            GuestNumber = Convert.ToInt32(values[7]);
            StartingDate = DateTime.Parse(values[8]);
            EndingDate = DateTime.Parse(values[9]);
            RequestStatus = (Status)Enum.Parse(typeof(Status), values[10]);
            GuideId= Convert.ToInt32(values[11]);
        }
    }
}
