using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.Constants;
using InitialProject.Services.IServices;
using InitialProject.View.Guest2;
using System.Collections.ObjectModel;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    class FinishedToursViewModel
    {
        public RelayCommand RateCommand { get; set; }
        public ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        public int UserId { get; set; }
        private readonly ITourService _tourService;
        private readonly ITourAttendanceService _tourAttendanceService;
        public FinishedToursViewModel(int userId)
        {
            UserId = userId;
            _tourService = Injector.CreateInstance<ITourService>();
            _tourAttendanceService = Injector.CreateInstance<ITourAttendanceService>();
            Tours = new ObservableCollection<Tour>(_tourService.GetAllFinished(userId));
            RateCommand = new RelayCommand(Rate);
        }

        private void Rate(object parameter)
        {
            RateTour(SelectedTour, UserId);
        }

        public void RateTour(Tour tour, int userId)
        {
            if (tour != null)
            {
                if (_tourAttendanceService.CheckPossibleComment(userId, tour.TourId))
                {
                    TourReview tourReview = new TourReview(tour.TourId, userId);
                    tourReview.Show();
                }
                else
                {
                    MessageBox.Show(TourViewConstants.TourReviewed, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Yes);
                }
            }
        }
    }
}
