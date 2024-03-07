using Eco.ViewModel.Runtime;
using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class ReservationChangeViewModel : INotifyPropertyChanged
    {
        private Window this_window;
        public event PropertyChangedEventHandler? PropertyChanged;
        private IChangeReservationRequestService _requestService;
        private IReservationService _reservationService;
        private IAccommodationService _accommodationService;
        private IAccommodationReservationService _accommodationReservationService;
        private IUserService _userService;
        private int _userId;
        private int _ownerId;
        public ObservableCollection<KeyValuePair<int, string>> ReservationsForChange { get; set; }
        public int SelectedReservationId { get; set; }
        public DateTime NewCheckInDate { get; set; }
        public DateTime NewCheckOutDate { get; set; }
        public RelayCommand SendRequest_Command { get; private set; }
        public RelayCommand Cancel_Command { get; private set; }
        private ObservableCollection<ChangeReservationRequest> _requests;
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
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ReservationChangeViewModel(int userId, ObservableCollection<ChangeReservationRequest> Requests, Window window)
        {
            InitializeServices();
            _userId = _userService.GetUserId();
            this.Requests = Requests;
            InitializeCommands();
            InitializeReservationsForChange();
            this_window = window;
        }

        private void InitializeCommands()
        {
            SendRequest_Command = new RelayCommand(SendRequest);
            Cancel_Command = new RelayCommand(Cancel);
        }

        private void InitializeServices()
        {
            _reservationService = Injector.CreateInstance<IReservationService>();
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            _requestService = Injector.CreateInstance<IChangeReservationRequestService>();
            _accommodationReservationService = Injector.CreateInstance<IAccommodationReservationService>();
            _userService = Injector.CreateInstance<IUserService>();
        }

        private void InitializeReservationsForChange()
        {
            ReservationsForChange = new ObservableCollection<KeyValuePair<int, string>>(_accommodationReservationService.GetReservationsByUserId(_userId));
        }
        private void SendRequest(object parameter)
        {
            _ownerId = _accommodationService.GetOwnerIdByReservationId(SelectedReservationId);
            string accommodationName = _accommodationService.GetNameByReservationId(SelectedReservationId);
            ChangeReservationRequest request = new ChangeReservationRequest(SelectedReservationId, accommodationName, NewCheckInDate, NewCheckOutDate, StatusType.Pending, _userId, _ownerId);
            _requestService.SaveRequest(request);
            UpdateRequests(request);
            this_window.Close();
        }
        private void Cancel(object parameter)
        {
            this_window.Close();
        }
        public void ComboBox_SelectionChanged(DatePicker CheckInPicker, DatePicker CheckOutPicker)
        {
            if (SelectedReservationId != 0)
            {
                CheckInPicker.SelectedDate = _reservationService.GetCheckInDate(_userId, SelectedReservationId);
                CheckOutPicker.SelectedDate = _reservationService.GetCheckOutDate(_userId, SelectedReservationId);
            }
        }
        private void UpdateRequests(ChangeReservationRequest request)
        {
            bool requestExists = Requests.Any(r => r.ReservationId == request.ReservationId);
            if (!requestExists)
            {
                Requests.Add(request);
            }
        }
    }
}
