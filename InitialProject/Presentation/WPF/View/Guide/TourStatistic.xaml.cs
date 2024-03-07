using InitialProject.Presentation.WPF.ViewModel;
using System.Windows;

namespace InitialProject.View.Guide
{
    /// <summary>
    /// Interaction logic for TourStatistic.xaml
    /// </summary>
    public partial class TourStatistic : Window
    {
        private readonly TourStatisticsViewModel _viewModel;
        public TourStatistic()
        {
            _viewModel = new TourStatisticsViewModel();
            DataContext = _viewModel;
            InitializeComponent();
        }

        private void FindTourButton(object sender, RoutedEventArgs e)
        {
            MostVisitedTour.Content = _viewModel.GetMostVisitedTour(_viewModel.Year).Name;
        }


        private void ShowStatisticButton(object sender, RoutedEventArgs e)
        {
            TourGrid.Visibility= Visibility.Visible;
            _viewModel.GenerateStatistic();
        }

    }
}
