namespace InitialProject.Presentation.WPF.View.Guest2
{

    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.ViewModel.Guest2;
    using InitialProject.Services.IServices;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;


    public partial class TourReservation : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private TourReservationViewModel _viewModel;

        public TourReservation(int userId, Tour tour)
        {
            _viewModel = new TourReservationViewModel(userId,tour);
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
