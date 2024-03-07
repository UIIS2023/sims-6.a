using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.CustomClasses
{
    public class TourRequestPerYear
    {
        public int Year { get; set; }
        public int? Month { get; set; }
        public int NumberOfRequestsLanguage { get; set; }
        public int NumberOfRequestsCity { get; set; }
        public int NumberOfRequestsCountry { get; set; }
    }



}
