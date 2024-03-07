using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Services;
using InitialProject.Services.IServices;
using InitialProject.View.Guest1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class CancelReservationViewModel : INotifyPropertyChanged
    {
        private int _userId;
        private int _ownerId;
        public ObservableCollection<KeyValuePair<int, string>> Reservations { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        private readonly IReservationService _reservationService;
        private readonly IAccommodationService _accommodationService;
        private readonly IAccommodationReservationService _accommodationReservationService;
        private readonly INotificationService _notificationService;
        private readonly IChangeReservationRequestService _requestService;
        private readonly IUserService _userService;
        private int _selectedReservationId;
        private bool _canCancelReservation;
        public int SelectedReservationId
        {
            get => _selectedReservationId;
            set
            {
                if (_selectedReservationId != value)
                {
                    _selectedReservationId = value;
                    CanCancelReservation = true;
                    OnPropertyChanged(nameof(SelectedReservationId));
                    OnPropertyChanged(nameof(CanCancelReservation));
                }
            }
        }
        public bool CanCancelReservation
        {
            get => _canCancelReservation;
            set
            {
                if (_canCancelReservation != value)
                {
                    _canCancelReservation = value;
                    OnPropertyChanged(nameof(CanCancelReservation));
                }
            }
        }
        public RelayCommand CancelReservationCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }

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
        public CancelReservationViewModel(ObservableCollection<ChangeReservationRequest> requests)
        {
            _reservationService = Injector.CreateInstance<IReservationService>();
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            _accommodationReservationService = Injector.CreateInstance<IAccommodationReservationService>();
            _notificationService = Injector.CreateInstance<INotificationService>();
            _requestService = Injector.CreateInstance<IChangeReservationRequestService>();
            _userService = Injector.CreateInstance<IUserService>();
            _userId = _userService.GetUserId();
            CanCancelReservation = false;
            CancelReservationCommand = new RelayCommand(ReservationCancel);
            CloseCommand = new RelayCommand(Close);
            InitializeReservations();
            Requests = requests;
        }
        public void ReservationCancel(object parameter)
        {
            if (_accommodationReservationService.IsCancellingPossible(DateTime.Now, SelectedReservationId))
            {
                DeleteRequest();
                CreateNotification();
                DeleteReservation();
                MessageBox.Show("You successfully cancelled reservation!");
                CloseWindow();
            }
            else
            {
                MessageBox.Show("You cannot cancel this reservation due to owner's accommodation policy");
            }
        }
        public void Close(object parameter)
        {
            CloseWindow();
        }
        private void InitializeReservations()
        {
            Reservations = new ObservableCollection<KeyValuePair<int, string>>(_accommodationReservationService.GetReservationsByUserId(_userService.GetUserId()));
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void DeleteReservation()
        {
            _reservationService.Delete(SelectedReservationId);
            _accommodationService.DeleteReservation(SelectedReservationId);
            _requestService.DeleteRequestByReservationId(SelectedReservationId);
        }
        private void CreateNotification()
        {
            _ownerId = _accommodationService.GetOwnerIdByReservationId(SelectedReservationId);
            Notification notification = new Notification(_userId, _ownerId,TypeNotification.ReservationCancelled, SelectedReservationId);
            _notificationService.SaveNotification(notification);
        }
        private void DeleteRequest()
        {
            try
            {
                ChangeReservationRequest requestToRemove = Requests?.First(request => request.ReservationId == SelectedReservationId);
                Requests.Remove(requestToRemove);
            }
            catch (InvalidOperationException ex)
            {
            }
        }
        private void CloseWindow()
        {
            App.Current.MainWindow = App.Current.Windows.OfType<CancelReservation>().FirstOrDefault();
            App.Current.MainWindow.Close();
        }
    }
}
