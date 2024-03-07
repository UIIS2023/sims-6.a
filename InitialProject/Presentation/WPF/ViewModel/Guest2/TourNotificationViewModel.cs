using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guest2;
using InitialProject.Services.IServices;
using System.Linq;

namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    public class TourNotificationViewModel
    {
        public RelayCommand OkCommand { get; set; }
        public Tour Tour { get; set; }
        private readonly ITourService _tourService;
        public TourNotificationViewModel(TourNotification notification)
        {
            _tourService = Injector.CreateInstance<ITourService>();
            Tour = _tourService.GetTourById(notification.TourId);
            OkCommand = new RelayCommand(Close);
        }

        private void Close(object parameter)
        {
            App.Current.MainWindow = App.Current.Windows.OfType<TourNotificationView>().FirstOrDefault();
            App.Current.MainWindow.Close();
        }


    }
}
