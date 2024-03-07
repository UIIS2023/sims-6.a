using InitialProject.Presentation.WPF.ViewModel.Guest2;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.View.Guest2.Views
{
    /// <summary>
    /// Interaction logic for TourRequestsView.xaml
    /// </summary>
    public partial class TourRequestsView : Page
    {
        public TourRequestsView(int userId)
        {
            TourRequestsViewModel viewModel = new TourRequestsViewModel(userId);
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
