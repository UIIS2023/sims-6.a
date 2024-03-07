using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel.Guide;
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
    /// Interaction logic for CreatingTourPointView.xaml
    /// </summary>
    public partial class CreatingTourPointView : Window
    {

       
        public CreateTourPointViewModel tourPointViewModel { get; set; }
        public CreatingTourPointView(int tourPointView, List<TourPoint> tourPoints)
        {
            tourPointViewModel = new CreateTourPointViewModel(tourPointView,tourPoints);
            DataContext = tourPointViewModel;

            InitializeComponent();
        }
    }
}
