namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.Constants;
    using InitialProject.Services.IServices;
    using System.Windows;

    public class TourViewModel
    {
        private readonly ITourService _tourService;
        public Tour Tour { get; set; }
        public RelayCommand ReserveCommand { get; set; }
        public TourViewModel(Tour tour)
        {
            ReserveCommand = new RelayCommand(Reserve);
            Tour = tour;
        }

        public void Reserve(object parameter)
        {
            if (Tour.MaxGuestNumber == 0)
            {
                HandleFullTourCapacity();
            }
            else
            {
                InitialProject.Presentation.WPF.View.Guest2.TourReservation tourReservation = new View.Guest2.TourReservation(1, Tour);
                tourReservation.ShowDialog();
            }
        }

        private void HandleFullTourCapacity()
        {
            MessageBoxResult result;
            result = MessageBox.Show(TourViewConstants.MaxGuestNumberIsZero, TourViewConstants.Caption, MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
            /* if (result == MessageBoxResult.Yes)
             {
                 Tours = new ObservableCollection<Tour>(_tourService.GetSimilarAsTourHasFullCapacity(SelectedTour.Location.Country, SelectedTour.Location.City));

                 if (Tours.Count == 0)
                 {
                     MessageBox.Show(TourViewConstants.ViewOtherTours, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Yes);
                     Tours = new ObservableCollection<Tour>(_tourService.GetAllNotStartedTours());
                 }

             }*/
        }
    }
}
