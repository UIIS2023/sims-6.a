using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guest2;
using InitialProject.Presentation.WPF.View.Guide;
using InitialProject.Repository;
using InitialProject.View;
using InitialProject.View.Guest1;
using InitialProject.View.Owner;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace InitialProject.Presentation.WPF.View
{
    /// <summary>
    /// Interaction logic for StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        private readonly LocationRepository _locationRepository;
        private readonly TourPointRepository _tourPointRepository;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly GuestReviewRepository _guestReviewRepository;
        private readonly UserToReviewRepository _userToReviewRepository;
        private readonly ReservationRepository _reservationRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;

        private readonly int _userId;
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<Reservation> Reservations { get; set; }
        public static List<GuestReview> GuestReviews {get; set;}
        public static ObservableCollection<UserToReview> UsersToReview { get; set;}
        public static ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public StartWindow(int userId)
        {
            InitializeComponent();
            DataContext = this;
            _locationRepository = new LocationRepository();
            _accommodationRepository = new AccommodationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _guestReviewRepository= new GuestReviewRepository();
            _reservationRepository= new ReservationRepository();
            _userToReviewRepository = new UserToReviewRepository();

            Reservations = new ObservableCollection<Reservation>(_reservationRepository.GetAll());
            GuestReviews = new List<GuestReview>(_guestReviewRepository.GetAll());
            Locations = new ObservableCollection<Location>(_accommodationRepository.GetAllLocationsFromAccommodations());
            Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationRepository.GetAll());
            
            _userId = userId;
            InitializeUsersToReview();
        }

        private void Guest1_ButtonClick(object sender, RoutedEventArgs e)
        {
        }
        private void Guest2_ButtonClick(object sender, RoutedEventArgs e)
        {
            HomeWindow homeWindow = new HomeWindow(1);
            homeWindow.Show();
/*            MainWindow main = new MainWindow();
            main.Show();*/
        }
        private void Owner_ButtonClick(object sender, RoutedEventArgs e)
        {
            RegisterNewAccommodation newAccommodation = new RegisterNewAccommodation(_userId);
            newAccommodation.Show();
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OwnerReviews ownerReviews = new OwnerReviews(_userId);
            ownerReviews.Show();
        }
        private void Guide_ButtonClick(object sender, RoutedEventArgs e)
        {

            GuideHomeScreen guideHomeScreen = new GuideHomeScreen(null);
            guideHomeScreen.Show();
        }
        private void OpenRegistrationForm(object sender, RoutedEventArgs e)
        {
            UserLogIn registrationForm = new UserLogIn();
            registrationForm.Show();
        }
        private void InitializeUsersToReview()
        {
            foreach(Reservation reservation in Reservations){
                if (CheckIfLeftReservation(reservation))
                {
                    _reservationRepository.Delete(reservation);
                    int accommodation_id = ReservationAccommodationId(reservation);
                    int owner_id = OwnerReservationId(accommodation_id);
                    //tourReservationRepository.Delete(reservation);
                    UserToReview userToReview = new UserToReview(owner_id,accommodation_id, reservation.UserId, reservation.ReservationDateRange.EndDate); //bez sign in forme defaultni ownerId je 0
                    _userToReviewRepository.Save(userToReview);
                }
            }
        }
        private bool CheckIfLeftReservation(Reservation reservation)
        {
            if(reservation.ReservationDateRange.EndDate < DateTime.Now)
            {
                return true;
            }
            return false;
        }

        private int ReservationAccommodationId(Reservation reservation)
        {
            foreach(AccommodationReservation accommodationReservation in AccommodationReservations)
            {
                if(accommodationReservation.ReservationId == reservation.ReservationId)
                {
                    return accommodationReservation.AccommodationId;
                    
                }
            }
            return -1;
        }

        private int OwnerReservationId(int accommodationId)
        {
            foreach(Accommodation accommodation in Accommodations)
            {
                if(accommodation.AccommodationID == accommodationId)
                {
                    return accommodation.UserId;
                }
            }
            return -1;
        }
    }
        
}
