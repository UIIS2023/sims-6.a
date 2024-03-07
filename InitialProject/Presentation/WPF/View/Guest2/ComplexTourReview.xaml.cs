namespace InitialProject.Presentation.WPF.View.Guest2
{
    using System.Windows;

    /// <summary>
    /// Interaction logic for ComplexTourReview.xaml
    /// </summary>
    public partial class ComplexTourReview : Window
    {
        public ComplexTourReview(int tourId)
        {
            ViewModel.Guest2.ComplexTourRequestsViewModel viewModel = new ViewModel.Guest2.ComplexTourRequestsViewModel(tourId);
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
