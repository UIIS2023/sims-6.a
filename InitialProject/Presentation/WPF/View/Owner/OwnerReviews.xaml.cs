using InitialProject.Domen.Model;
using InitialProject.Services;
using InitialProject.View.Guest1;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using InitialProject.Presentation.WPF.View.Owner;

namespace InitialProject.View.Owner
{
    /// <summary>
    /// Interaction logic for OwnerReviews.xaml
    /// </summary>
    public partial class OwnerReviews : Window,INotifyPropertyChanged
    {
        private readonly OwnerRateService _ownerRateService = new OwnerRateService();
        public ObservableCollection<OwnerRate> _ownerRates;
        public int OwnerId { get; set; }

        public ObservableCollection<OwnerRate> OwnerRates
        {
            get { return _ownerRates; }
            set 
            {
                _ownerRates = value;
                OnPropertyChanged(nameof(OwnerRates));  
            
            }
        }
        public OwnerReviews(int userId)
        {
            InitializeComponent();
            DataContext= this;
            OwnerId = userId;

            OwnerRates = new ObservableCollection<OwnerRate>(_ownerRateService.RatingsFromRatedGuest(OwnerId));
            showSuperOwner(OwnerId);
        }

        public void showSuperOwner(int ownerId)
        {
            if(_ownerRateService.IsSuperOwner(ownerId))
            {
                zvjezda.Visibility = Visibility.Visible;
            }
            else
            {
                zvjezda.Visibility = Visibility.Collapsed;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OwnerChangeReservationRequests ownerChangeReservationRequests = new OwnerChangeReservationRequests(OwnerId);
            ownerChangeReservationRequests.Show();
        }
    }


}
