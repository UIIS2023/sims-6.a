namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.Constants;
    using InitialProject.Services.IServices;
    using System.Windows;

    public class ReservedTourViewModel
    {
        private ITourPointService _tourPointService;
        public ReservedTourViewModel()
        {
            _tourPointService = Injector.CreateInstance<ITourPointService>();
        }

        public void HandleMessageForDetails(Tour tour)
        {
            if (tour != null)
            {
                if (tour.TourStarted)
                {
                    if (_tourPointService.GetActiveTourPointOnTour(tour.TourId) == null)
                    {
                        MessageBox.Show(TourViewConstants.ActiveTourPointNotFound, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
                    }
                    else
                    {
                        MessageBox.Show("Trenutno je aktivna " + _tourPointService.GetActiveTourPointOnTour(tour.TourId).Name + " tacka!", TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
                    }
                }
                else
                {
                    MessageBox.Show(TourViewConstants.TourNotStarted, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
                }
            }
            else
            {
                MessageBox.Show(TourViewConstants.MustSelectTour, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.Yes);
            }
        }
    }
}
