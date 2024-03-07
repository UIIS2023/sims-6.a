using InitialProject.Domen.Model;
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

namespace InitialProject.Presentation.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerChangeReservationRequests.xaml
    /// </summary>
    public partial class OwnerChangeReservationRequests : Window , INotifyPropertyChanged
    {
        private readonly ChangeReservationRequestService  _requestService = new ChangeReservationRequestService();

        public ObservableCollection<OwnerChangeRequests> _requests;

        public OwnerChangeRequests SelectedRequest { get; set; }

        private int OwnerId { get; set; }
        
        public ObservableCollection<OwnerChangeRequests> Requests
        {
            get { return _requests; }
            set
            {
                _requests = value;
                OnPropertyChanged(nameof(Requests));
            }
        }
        public OwnerChangeReservationRequests(int _userId)
        {
            InitializeComponent();
            DataContext = this;
            OwnerId= _userId;

            Requests= new ObservableCollection<OwnerChangeRequests>(_requestService.OwnerChangeReservationRequest(OwnerId));
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedRequest == null)
            {
                MessageBox.Show("Please select request");
            }
            else
            {
                _requestService.AcceptRequest(SelectedRequest.RequestId);
                _requestService.ChangeReservationDateRange(SelectedRequest.NewStartDate, SelectedRequest.NewEndDate, SelectedRequest.ReservationId);
                Requests.Remove(SelectedRequest);
            }
        }

        private void Decline_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedRequest == null)
            {
                MessageBox.Show("Please select request");
            }
            else
            {
                DeclineRequest declineRequest = new DeclineRequest(SelectedRequest.RequestId);
                declineRequest.Show();

            }
        }
    }
}
