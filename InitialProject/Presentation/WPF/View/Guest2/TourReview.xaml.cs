namespace InitialProject.View.Guest2
{
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.Constants;
    using InitialProject.Presentation.WPF.ViewModel.Guest2;
    using InitialProject.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows;

    public partial class TourReview : Window
    {
        private TourReviewViewModel _viewModel;
        public TourReview(int tourId, int userId)
        {
            _viewModel = new TourReviewViewModel(tourId, userId);
            InitializeComponent();
            DataContext = _viewModel;

        }
    }
}
