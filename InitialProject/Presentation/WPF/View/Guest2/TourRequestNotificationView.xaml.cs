namespace InitialProject.Presentation.WPF.View.Guest2
{
    using InitialProject.Domen.CustomClasses;
    using InitialProject.Presentation.WPF.ViewModel.Guest2;
    using System.Windows;

    /// <summary>
    /// Interaction logic for TourRequestNotificationView.xaml
    /// </summary>
    public partial class TourRequestNotificationView : Window
    {
        private TourRequestNotificationViewModel _viewModel;
        public TourRequestNotificationView(TourNotification notification)
        {
            _viewModel = new TourRequestNotificationViewModel(notification);
            InitializeComponent();
            DataContext = _viewModel;

        }

    }
}
