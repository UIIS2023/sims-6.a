using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.CustomClasses
{
    public class FilterRequests
    {
        public List<TourRequest> TourRequests { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public int? NumberOfGuests { get; set; } 
        public string? Language { get; set; }  
        public DateTime? StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
    }
}
