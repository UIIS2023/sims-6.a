using InitialProject.Domen.Model;
using InitialProject.Repository;
using System.Collections.ObjectModel;
using System.Windows;

namespace InitialProject.View.Guide
{
    /// <summary>
    /// Interaction logic for ChangeTourAttendanceStatus.xaml
    /// </summary>
    public partial class ChangeTourAttendanceStatus : Window
    {
        public TourAttendance selectedAttendance;
        public ObservableCollection<TourAttendance> attendances;
        private readonly TourAttendanceRepository _tourAttendanceRepository;
        public ChangeTourAttendanceStatus(TourAttendance selectedattendance, ObservableCollection<TourAttendance> Users)
        {
            InitializeComponent();
            selectedAttendance = selectedattendance;
            _tourAttendanceRepository = new TourAttendanceRepository();
            attendances = Users;
        }

        

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            if(StatusComboBox.Text == "OnHold")
            {
                selectedAttendance.UserAttended = TourAttendance.AttendanceStatus.OnHold;
                selectedAttendance.CanGiveReview = true;
            }
            else if(StatusComboBox.Text == "NotPresent")
            {
                selectedAttendance.UserAttended = TourAttendance.AttendanceStatus.NotPresent;
            }
            foreach(var attendance in attendances)
            {
                if(attendance.TourAttendanceId == selectedAttendance.TourAttendanceId)
                {
                    attendance.UserAttended = selectedAttendance.UserAttended;
                    _tourAttendanceRepository.Update(attendance);
                }
            }
            Close();
        }
    }
}
