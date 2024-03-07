using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.Presentation.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for AcceptedTourRequestView.xaml
    /// </summary>
    public partial class AcceptedTourRequestView : Window
    {
        public ObservableCollection<string> Countries { get; set; }
        List<string> CountriesList = new List<string>();
        string test1 = "cao";
        string test2 = "antonije";
        string test3 = "caosadsad";
        string test4 = "caoadsadasdas";



        public AcceptedTourRequestView()
        {
            CountriesList.Add(test1);
            CountriesList.Add(test2);
            CountriesList.Add(test3);
            CountriesList.Add(test4);

            Countries = new ObservableCollection<string>(CountriesList); 
            InitializeComponent();
            DataContext = this;
        }

       
    }
}
