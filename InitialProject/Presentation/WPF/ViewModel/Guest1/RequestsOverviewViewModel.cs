using Eco.ViewModel.Runtime;
using InitialProject.Domen.Model;
using InitialProject.Services;
using InitialProject.View.Guest1;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class RequestsOverviewViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ChangeReservationRequest> _requests;
        private readonly UserService _userService;
        private Window _ownerWindow;
        public ObservableCollection<ChangeReservationRequest> Requests
        {
            get
            {
                return _requests;
            }
            set
            {
                _requests = value;
                OnPropertyChanged(nameof(Requests));
            }
        }
        private readonly ChangeReservationRequestService requestsService;

        public event PropertyChangedEventHandler? PropertyChanged;
        public RelayCommand ChangeReservation_Command { get; private set; }
        public RelayCommand CancelReservation_Command { get; private set; }
        public RequestsOverviewViewModel(Window window)
        {
            requestsService = new ChangeReservationRequestService();
            _userService = new UserService();
            Requests = new ObservableCollection<ChangeReservationRequest>(requestsService.GetRequests(_userService.GetUserId()));
            ChangeReservation_Command = new RelayCommand(OpenChangeReservation);
            CancelReservation_Command = new RelayCommand(OpenCancelReservation);
            _ownerWindow = window;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void OpenChangeReservation(object parameter)
        {
            ReservationChange reservationChange = new ReservationChange(_userService.GetUserId(), Requests);
            reservationChange.Owner = _ownerWindow;
            reservationChange.ShowDialog();
        }
        private void OpenCancelReservation(object parameter)
        {
            CancelReservation cancelReservation = new CancelReservation(Requests);
            cancelReservation.Owner = _ownerWindow;
            cancelReservation.ShowDialog();
        }
    }
}
