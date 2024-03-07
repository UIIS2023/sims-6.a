using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guest1;
using InitialProject.Services;
using InitialProject.Services.IServices;
using InitialProject.View;
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

    public class Guest1HomeWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ReservationDTO> _upcomingReservations;
        private ObservableCollection<ReservationDTO> _previousReservation;
        private int _userId;
        private string _superguest;
        private string _username;
        private int _points;
        private IReservationCompletionService _reservationCompletionService;
        private IUserService _userService;
        private IUserReservationCounterService _userReservationCounterService;
        private IOwnerToRateService _ownerToRateService;
        private IReservationService _reservationService;
        private IReservationDTOService _reservationDTOService;
        private ICheckingInService _checkingInService;
        public event PropertyChangedEventHandler? PropertyChanged;
        public RelayCommand OpenAccommodationDisplay_Command { get; set; }
        public RelayCommand OpenForums_Command { get; set; }
        public RelayCommand OpenMyRatings_Command { get; set; }
        public RelayCommand OpenOwnerRating_Command { get; set; }
        public RelayCommand OpenChangeReservation_Command { get; set; }
        public RelayCommand OpenCancelReservation_Command { get; set; }
        public RelayCommand OpenAnywhereAnytime_Command { get; set; }
        public RelayCommand OpenForumCreate_Command { get; set; }
        public RelayCommand LogUserOut_Commend { get; set; }
        public ObservableCollection<ReservationDTO> UpcomingReservations
        {
            get
            {
                return _upcomingReservations;
            }
            set
            {
                _upcomingReservations = value;
                OnPropertyChanged(nameof(UpcomingReservations));
            }
        }
        public ObservableCollection<ReservationDTO> PreviousReservations
        {
            get
            {
                return _previousReservation;
            }
            set
            {
                _previousReservation = value;
                OnPropertyChanged(nameof(PreviousReservations));
            }
        }
        public string Username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }
        }
        public string Superguest
        {
            get => _superguest;
            set
            {
                if (_superguest != value)
                {
                    _superguest = value;
                    OnPropertyChanged(nameof(Superguest));
                }
            }
        }
        public int Points
        {
            get => _points;
            set
            {
                if (_points != value)
                {
                    _points = value;
                    OnPropertyChanged(nameof(Points));
                }
            }
        }

        public Guest1HomeWindowViewModel()
        {
            InitializeServices();
            _userId = _userService.GetUserId();
            InitializeCommands();
            InitializeCollections();
            SetupReservationSystem();
        }
        private void InitializeCollections()
        {
            UpcomingReservations = new ObservableCollection<ReservationDTO>();
            PreviousReservations = new ObservableCollection<ReservationDTO>();
        }
        private void InitializeServices()
        {
            _userService = Injector.CreateInstance<IUserService>();
            _reservationCompletionService = Injector.CreateInstance<IReservationCompletionService>();
            _userReservationCounterService = Injector.CreateInstance<IUserReservationCounterService>();
            _ownerToRateService = Injector.CreateInstance<IOwnerToRateService>();
            _reservationService = Injector.CreateInstance<IReservationService>();
            _reservationDTOService = Injector.CreateInstance<IReservationDTOService>();
            _checkingInService = Injector.CreateInstance<ICheckingInService>();
        }
        private void InitializeCommands()
        {
            OpenAccommodationDisplay_Command = new RelayCommand(OpenAccommodationDisplay);
            OpenForums_Command = new RelayCommand(OpenForums);
            OpenMyRatings_Command = new RelayCommand(OpenMyRatings);
            OpenOwnerRating_Command = new RelayCommand(OpenOwnerRating);
            OpenChangeReservation_Command = new RelayCommand(OpenChangeReservation);
            OpenCancelReservation_Command = new RelayCommand(OpenCancelReservation);
            OpenAnywhereAnytime_Command = new RelayCommand(OpenAnywhereAnytime);
            OpenForumCreate_Command = new RelayCommand(OpenForumCreate);
            LogUserOut_Commend = new RelayCommand(LogUserOut);
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void InitializePoints()
        {
            Points = _userService.GetUserPoints();
        }
        private void InitializeUsername()
        {
            Username = _userService.GetUsername();
        }
        private void InitializeSuperGuest()
        {
            if (_userService.GetSuperGuest())
            {
                Superguest = "Yes";
            }
            else
            {
                Superguest = "No";
            }
        }
        private void InitializeUpcomingReservations()
        {
            UpcomingReservations.Clear();
            UpcomingReservations = new ObservableCollection<ReservationDTO>(_reservationDTOService.GetUpcomingReservationsByUser());
        }
        private void InitializePastReservations()
        {
            PreviousReservations.Clear();
            PreviousReservations = new ObservableCollection<ReservationDTO>(_reservationDTOService.GetPastReservationsByUser());
        }
        private void HandleCheckingIn() //HENDLUJ
        {
            _checkingInService.HandleCheckingIn();
        }
        private void HandleReservationCompletion()
        {
            foreach (Reservation reservation in _reservationService.GetActiveReservationsByUser(_userId))
            {
                _reservationCompletionService.HandleReservationCompletion(_userId, reservation.ReservationId);
            }
        }
        private void InitializeReservationCounter()
        {
            _userReservationCounterService.InitializeReservationCounter(_userId);
        }
        private void UpdateOwnerToRate()
        {
            _ownerToRateService.DeleteIfFiveDaysPassed();
        }
        private void SetupReservationSystem()
        {
            InitializeReservationCounter();
            HandleReservationCompletion();
            UpdateOwnerToRate();
            InitializeUpcomingReservations();
            InitializePastReservations();
            InitializeUsername();
            InitializeSuperGuest(); 
            InitializePoints();
            HandleCheckingIn();
        }
        private void OpenAccommodationDisplay(object parameter)
        {
            AccommodationDisplay accommodationDisplay = new AccommodationDisplay(_userService.GetUserId());
            AdjustWindow(accommodationDisplay);
            accommodationDisplay.ShowDialog();
            InitializeUpcomingReservations();
            InitializePoints();
        }
        private void LogUserOut(object parameter)
        {
            CloseWindow();
            UserLogIn logInWindow = new UserLogIn();
            logInWindow.Show();
        }
        private void OpenForums(object parameter)
        {
            ForumsOverviewWindow forumsOverviewWindow = new ForumsOverviewWindow();
            AdjustWindow(forumsOverviewWindow);
            forumsOverviewWindow.ShowDialog();
        }

        private void OpenMyRatings(object parameter)
        {
            ReviewsOverview reviewsOverview = new ReviewsOverview(_userId);
            AdjustWindow(reviewsOverview);
            reviewsOverview.ShowDialog();
        }
        private void OpenOwnerRating(object parameter)
        {
            OwnerRating ownerRating = new OwnerRating(_userId);
            AdjustWindow(ownerRating);
            ownerRating.ShowDialog();
        }
        private void OpenChangeReservation(object parameter)
        {
            RequestsOwerview requestsOwerview = new RequestsOwerview();
            AdjustWindow(requestsOwerview);
            requestsOwerview.ShowDialog();
            InitializeUpcomingReservations();
        }
        private void OpenCancelReservation(object parameter)
        {
            RequestsOwerview requestsOwerview = new RequestsOwerview();
            AdjustWindow(requestsOwerview);
            requestsOwerview.ShowDialog();
            InitializeUpcomingReservations();
        }
        private void OpenAnywhereAnytime(object parameter)
        {
            AnywhereAnytimeWindow anywhereAnytimeWindow = new AnywhereAnytimeWindow();
            AdjustWindow(anywhereAnytimeWindow);
            anywhereAnytimeWindow.ShowDialog();
            InitializeUpcomingReservations();
        }
        private void OpenForumCreate(object parameter)
        {
            ForumsOverviewWindow forumsOverviewWindow = new ForumsOverviewWindow();
            AdjustWindow(forumsOverviewWindow);
            forumsOverviewWindow.ShowDialog();
        }
        private void AdjustWindow(Window anywhereAnytimeWindow)
        {
            anywhereAnytimeWindow.Owner = App.Current.Windows.OfType<Guest1HomeWindow>().FirstOrDefault();
            anywhereAnytimeWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }
        private void CloseWindow()
        {
            App.Current.MainWindow = App.Current.Windows.OfType<Guest1HomeWindow>().FirstOrDefault();
            App.Current.MainWindow.Close();
        }
    }
}
