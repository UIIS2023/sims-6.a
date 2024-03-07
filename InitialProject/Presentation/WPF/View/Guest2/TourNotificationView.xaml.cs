namespace InitialProject.Presentation.WPF.View.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.CustomClasses;
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.ViewModel.Guest2;
    using InitialProject.Services.IServices;
    using System.Windows;

    /// <summary>
    /// Interaction logic for TourNotification.xaml
    /// </summary>
    public partial class TourNotificationView : Window
    {
        private TourNotificationViewModel _viewModel;
        public TourNotificationView(TourNotification notification)
        {
            _viewModel = new TourNotificationViewModel(notification);
            InitializeComponent();
            DataContext = _viewModel;
        }
    }
}
