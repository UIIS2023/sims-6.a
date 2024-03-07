namespace InitialProject.View.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.ViewModel.Guest2;
    using InitialProject.Services.IServices;
    using System.Windows;

    /// <summary>
    /// Interaction logic for CheckingTour.xaml
    /// </summary>
    public partial class CheckingTour : Window
    {
        private readonly CheckingTourViewModel _checkingTourViewModel;

        public CheckingTour(TourAttendance tourAttendance)
        {
            _checkingTourViewModel = new CheckingTourViewModel(tourAttendance);
            InitializeComponent();
            DataContext = this;
        }
    }
}
