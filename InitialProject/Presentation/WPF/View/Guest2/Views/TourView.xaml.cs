namespace InitialProject.Presentation.WPF.View.Guest2.Views
{
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.ViewModel.Guest2;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for TourView.xaml
    /// </summary>
    public partial class TourView : Page
    {
        private TourViewModel tourViewModel;
        public TourView(Tour tour)
        {
            tourViewModel = new TourViewModel(tour);
            InitializeComponent();
            DataContext = tourViewModel;
        }

    }
}
