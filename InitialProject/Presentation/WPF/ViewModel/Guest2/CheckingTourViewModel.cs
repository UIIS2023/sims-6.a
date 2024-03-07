namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.ViewModel.Guest1;
    using InitialProject.Services;
    using InitialProject.Services.IServices;
    using InitialProject.View.Guest2;
    using System.Linq;

    public class CheckingTourViewModel
    {
        public RelayCommand ConfirmAttendanceCommand { get; set; }
        public RelayCommand RejectAttendanceCommand { get; set; }

        private readonly ITourAttendanceService _tourAttendanceService;
        private readonly ITourService _tourService;
        public TourAttendance TourAttendance { get; set; }
        public Tour Tour { get; }

        public CheckingTourViewModel(TourAttendance tourAttendance)
        {
            _tourAttendanceService = Injector.CreateInstance<ITourAttendanceService>();
            _tourService = Injector.CreateInstance<ITourService>();
            Tour = _tourService.GetTourById(tourAttendance.TourId);
            TourAttendance = tourAttendance;
            ConfirmAttendanceCommand = new RelayCommand(ConfirmTourAttendance);
            RejectAttendanceCommand = new RelayCommand(RejectTourAttendance);
        }

        private void RejectTourAttendance(object parameter)
        {
            _tourAttendanceService.RejectTourAttendance(TourAttendance);
            CloseWindow();
        }

        private void ConfirmTourAttendance(object parameter)
        {
            _tourAttendanceService.ConfirmTourAttendance(TourAttendance);
            CloseWindow();
        }

        private void CloseWindow()
        {
            App.Current.MainWindow = App.Current.Windows.OfType<CheckingTour>().FirstOrDefault();
            App.Current.MainWindow.Close();
        }
    }
}
