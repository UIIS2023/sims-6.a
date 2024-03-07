using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel;
using InitialProject.Presentation.WPF.ViewModel.Guest1;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.View.Guest1
{
    /// <summary>
    /// Interaction logic for ReservationChange.xaml
    /// </summary>
    public partial class ReservationChange : Window
    {
        private ReservationChangeViewModel viewModel;
        public ReservationChange(int userId, ObservableCollection<ChangeReservationRequest> Requests)
        {
            InitializeComponent();
            viewModel = new ReservationChangeViewModel(userId, Requests, this);
            DataContext = viewModel;
            Send_Button.IsEnabled = false;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.ComboBox_SelectionChanged(CheckInPicker, CheckOutPicker);
            Send_Button.IsEnabled = true;
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
