using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel.Guest1;
using InitialProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    /// <summary>
    /// Interaction logic for CancelReservation.xaml
    /// </summary>
    public partial class CancelReservation : Window
    {
        private CancelReservationViewModel viewModel;
        public CancelReservation(ObservableCollection<ChangeReservationRequest> requests)
        {
            InitializeComponent();
            viewModel = new CancelReservationViewModel(requests);
            DataContext = viewModel;
        }

    }
}
