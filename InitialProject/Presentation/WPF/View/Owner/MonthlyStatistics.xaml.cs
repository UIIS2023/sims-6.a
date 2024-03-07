using InitialProject.Presentation.WPF.ViewModel.Owner;
using System;
using System.Collections.Generic;
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

namespace InitialProject.Presentation.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for MonthlyStatistics.xaml
    /// </summary>
    public partial class MonthlyStatistics : Window
    {
        private MonthlyStatisticsViewModel viewModel;
        public MonthlyStatistics(int accommodationId, int year)
        {
            InitializeComponent();
            viewModel = new MonthlyStatisticsViewModel(accommodationId,year);
            DataContext= viewModel;
        }
    }
}
