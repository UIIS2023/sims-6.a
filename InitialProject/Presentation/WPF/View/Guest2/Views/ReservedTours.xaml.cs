namespace InitialProject.Presentation.WPF.View.Guest2.Views
{
    using InitialProject.Presentation.WPF.ViewModel.Guest2;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for ReservedTours.xaml
    /// </summary>
    public partial class ReservedTours : Page
    {
        private ReservedToursViewModel _viewModel;
        public ReservedTours(int userId)
        {
            _viewModel = new ReservedToursViewModel(userId);
            InitializeComponent();
            DataContext = _viewModel;
        }


    }
}
