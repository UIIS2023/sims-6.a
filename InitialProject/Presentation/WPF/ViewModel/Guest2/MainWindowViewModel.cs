namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.View.Guest2.Views;
    using InitialProject.Services;
    using InitialProject.Services.IServices;
    using InitialProject.View.Guest2;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Navigation;

    public class MainWindowViewModel
    {
        private readonly ITourNotificationService _tourNotificationService;
        private readonly IUserReservationCounterService _userReservationCounterService;
        private readonly ITourAttendanceService _tourAttendanceService;
        public static ObservableCollection<TourAttendance> TourAttendances { get; set; }
        public System.Windows.Navigation.NavigationService NavService { get; set; }
        public RelayCommand NavigateToUserPageCommand { get; set; }
        public RelayCommand NavigateToMainPageCommand { get; set; }
        public RelayCommand NavigateToTourPageCommand { get; set; }

        private void Execute_NavigateToTourPageCommand(object obj)
        {
            Page toursView = new ToursView();
            this.NavService.Navigate(toursView);
        }

        private void Execute_NavigateToUserPageCommand(object obj)
        {
            Page usersView = new UserView(1);
            this.NavService.Navigate(usersView);
        }

        public MainWindowViewModel(System.Windows.Navigation.NavigationService navService, int userId)
        {
            this.NavigateToUserPageCommand = new RelayCommand(Execute_NavigateToUserPageCommand);
            this.NavigateToMainPageCommand = new RelayCommand(Execute_NavigateToTourPageCommand);
            this.NavigateToTourPageCommand = new RelayCommand(Execute_NavigateToTourPageCommand);
            this.NavService = navService;

            _tourNotificationService = Injector.CreateInstance<ITourNotificationService>();
            _userReservationCounterService = Injector.CreateInstance<IUserReservationCounterService>();
            _tourAttendanceService = Injector.CreateInstance<ITourAttendanceService>();
            _tourNotificationService.NotifyGuest(userId);
            CheckTourAttendance(userId);
        }

        private void CheckTourAttendance(int userId)
        {
            TourAttendances = new ObservableCollection<TourAttendance>(_tourAttendanceService.GetAllToCheckByUser(userId));
            if (TourAttendances.Count() != 0)
            {
                foreach (var t in TourAttendances)
                {
                    CheckingTour checkingTour = new CheckingTour(t);
                    checkingTour.ShowDialog();
                }

                int number = TourAttendances.GroupBy(t => t.TourId).Count();
                _userReservationCounterService.CountTourReservations(userId, number);

            }
        }


    }
}
