using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.CustomClasses
{
    public class CreationType
    {
        public enum CreationTourType { CreatedByGuide, CreatedByRequest, CreatedByStatistics, CreatedByComplexRequest, SuperGuide }

        public CreationTourType Type { get; set; }

    }
}
