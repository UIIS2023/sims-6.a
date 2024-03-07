using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InitialProject.Domen.Model
{
    public class GuideStatus : ISerializable
    {
        public enum Status
        {
            Employeed,
            SuperGuide,
            Unemployed
        }
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Status EmploymentStatus { get; set; }

        public DateTime? DateOfPromotion { get; set; } = DateTime.MinValue;

        public string[] ToCSV()
        {

            string[] csvValues = {
                Id.ToString(),
                EmployeeId.ToString(),
                EmploymentStatus.ToString(),
                DateOfPromotion.ToString()
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id= Convert.ToInt32(values[0]);
            EmployeeId = Convert.ToInt32(values[1]);
            EmploymentStatus = (Status)Enum.Parse(typeof(Status), values[2]);
            DateOfPromotion = DateTime.Parse(values[3]);
        }
    }
}
