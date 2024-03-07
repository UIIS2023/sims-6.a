using Eco.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace InitialProject.Domen.Model
{
    public class Statistic
    {
        public int LessThen { get; set; }
        public int Between { get; set; }
        public int GreaterThan { get; set; }
        public double WithVoucher { get; set; }
        public double WithoutVoucher { get; set; }

        public Statistic()
        {
        }

        public Statistic(int lessThan, int between, int greaterThan, double withVoucher, double withoutVoucher)
        {
            LessThen = lessThan;
            Between = between;
            GreaterThan = greaterThan;
            WithVoucher = withVoucher;
            WithoutVoucher = withoutVoucher;
        }

    }
}
