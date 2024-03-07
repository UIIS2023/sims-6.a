using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.Constants;
using InitialProject.Services;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    public class ReservedToursViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Tour CommandParameter { get; set; }

        private readonly ITourPointService _tourPointService;
        private readonly ITourReservationService _tourReservationService;
        public RelayCommand DetailsCommand { get; set; }
        public Tour SelectedTour { get; set; }
        private ObservableCollection<Tour> _tours { get; set; }
        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }



        public ReservedToursViewModel(int userId)
        {
            _tourPointService = Injector.CreateInstance<ITourPointService>();
            _tourReservationService = Injector.CreateInstance<ITourReservationService>();
            Tours = new ObservableCollection<Tour>(_tourReservationService.GetAllReservedAndNotFinishedTour(userId));
            DetailsCommand = new RelayCommand(TourDetails);

        }

        public void TourDetails(object obj)
        {
            HandleMessageForDetails(SelectedTour);
        }

        public void Decline(object parameter)
        {
            
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
