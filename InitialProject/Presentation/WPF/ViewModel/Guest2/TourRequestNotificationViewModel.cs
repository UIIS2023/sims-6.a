using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guest2;
using InitialProject.Services.IServices;
using System.Linq;

namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    public class TourRequestNotificationViewModel
    {
        public RelayCommand OkCommand { get; set; }
        public TourRequest TourRequest { get; set; }
        private readonly ITourRequestService _tourRequestService;
        public TourRequestNotificationViewModel(TourNotification notification)
        {
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
            TourRequest = _tourRequestService.GetTourRequestById(notification.TourId);
            OkCommand = new RelayCommand(Close);
        }



        private void Close(object parameter)
        {
            App.Current.MainWindow = App.Current.Windows.OfType<TourRequestNotificationView>().FirstOrDefault();
            App.Current.MainWindow.Close();
        }
    }
}
