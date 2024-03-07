using InitialProject.Presentation.WPF.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.View.Guide
{
    public partial class CancelTour : Window
    {
        private readonly CancelTourViewModel viewModel;
        public CancelTour()
        {
            InitializeComponent();
            viewModel = new CancelTourViewModel(this);
            DataContext = viewModel;
        }
    }
}
