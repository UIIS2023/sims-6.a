using InitialProject.Presentation.WPF.ViewModel.Guest2;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.View.Guest2.Views
{
    /// <summary>
    /// Interaction logic for FinishedTours.xaml
    /// </summary>
    public partial class FinishedTours : Page
    {
        public FinishedTours(int userId)
        {
            FinishedToursViewModel _viewModel = new FinishedToursViewModel(userId);
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
