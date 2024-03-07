using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.CustomClasses
{
    public class TourStatistic
    {
        public string Country { get; set; }
        public string City { get; set; }

        public string Language { get; set; }
        public int NumberOfRequests { get; set; }
        public DateTime Date { get; set; }
    }
}
