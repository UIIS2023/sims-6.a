using InitialProject.Presentation.WPF.ViewModel.Guide;
using System.Windows;

namespace InitialProject.Presentation.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for TourRequestsView.xaml
    /// </summary>
    public partial class TourRequestsView : Window
    {
        public TourRequestsViewModel ViewModel { get; set; }
        public TourRequestsView(int? GuideId)
        {
            ViewModel = new TourRequestsViewModel(GuideId);
            DataContext = ViewModel;
            InitializeComponent();
        }
    }
}
