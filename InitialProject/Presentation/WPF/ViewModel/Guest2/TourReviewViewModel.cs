namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.Constants;
    using InitialProject.Services.IServices;
    using InitialProject.View.Guest2;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;

    public class TourReviewViewModel
    {
        public RelayCommand ConfirmCommand { get; set; }
        public RelayCommand RejectCommand { get; set; }
        public RelayCommand AddCommand { get; set; }

        private readonly ITourRateService _tourRateService;
        public static ObservableCollection<string> Rating { get; set; }
        public TourRate TourRate { get; set; }


        public TourReviewViewModel(int tourId, int userId)
        {
            ConfirmCommand = new RelayCommand(AddTourReview);
            RejectCommand = new RelayCommand(RejectReview);
            AddCommand = new RelayCommand(AddImage);
            _tourRateService = Injector.CreateInstance<ITourRateService>();
            TourRate = new TourRate();
            TourRate.TourId = tourId;
            TourRate.GuestId = userId;
            InitializeRating();
            TourRate.Images = new List<string>();
        }

        private void InitializeRating()
        {
            Rating = new ObservableCollection<string>();
            for (int i = 1; i <= 5; i++)
            {
                Rating.Add(i.ToString());
            }
        }

        private string _image;
        public string Image
        {
            get => _image;
            set
            {
                _image = value;
            }
        }

        public void RateTour(TourRate tourRate)
        {
            _tourRateService.MakeTourRate(tourRate);
            MessageBox.Show(TourViewConstants.CommentNoted, TourViewConstants.Caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.Yes);
        }

        private void AddTourReview(object parameter)
        {
            RateTour(TourRate);
            CloseWindow();
        }

        private void RejectReview(object parameter)
        {
            CloseWindow();
        }

        private void AddImage(object parameter)
        {
            if (!TourRate.Images.Contains(Image))
            {
                TourRate.Images.Add(Image);
            }
        }

        private void CloseWindow()
        {
            App.Current.MainWindow = App.Current.Windows.OfType<TourReview>().FirstOrDefault();
            App.Current.MainWindow.Close();
        }

    }
}
