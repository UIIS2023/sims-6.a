using InitialProject.View;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.ObjectModel;
using InitialProject.View.Guest1;
using InitialProject.Repository;
using InitialProject.Domen.Model;
using InitialProject.Services;
using InitialProject.Aplication.Factory;
using InitialProject.Services.IServices;
using InitialProject.Presentation.WPF.View;
using InitialProject.Presentation.WPF.View.Guest1;
using InitialProject.Presentation.WPF.View.Owner;
using InitialProject.Presentation.WPF.View.Guide;
using InitialProject.Presentation.WPF.View.Guest2;

namespace InitialProject
{
    /// <summary>
    /// Interaction logic for UserRegistration.xaml
    /// </summary>
    public partial class UserLogIn : Window, INotifyPropertyChanged
    {
        private UserRepository _userRepository;
        public string Email { get; set; }
        public string Password { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public static ObservableCollection<Location> Locations { get; set; }
        private readonly AccommodationRepository _accommodationRepository;
        private readonly IUserService _userService;



        public UserLogIn()
        {
            InitializeComponent();
            DataContext = this;
            _userRepository = new UserRepository();
            _userService = Injector.CreateInstance<IUserService>();
            _accommodationRepository = new AccommodationRepository();
            Locations = new ObservableCollection<Location>(_accommodationRepository.GetAllLocationsFromAccommodations());

        }
        private void Btn_LogIn(object sender, RoutedEventArgs e)
        {
            List<User> users = _userRepository.GetAllUsers();
            bool match = false;
            foreach(User user in users)
            {
                if(user.Email == Email && user.Password == Password)
                {
                    match = true;
                    //StartWindow startWindow = new StartWindow(user.Id); 
                    // this.Close();
                    _userService.UpdateUserId(user.Id);
                    ChooseWindow(user);
                    break;
                }
            }
            if (!match) MessageBox.Show("User don't exist.");
        }
        private void CloseWindowClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        private void ChooseWindow(User user)
        {
            switch (user.TypeOfUser)
            {
                case (UserType.Guest1):
                    Guest1HomeWindow window = new Guest1HomeWindow();
                    window.Show();
                    this.Close();
                    break;
                case(UserType.Owner):
                    OwnerStartWindow start = new OwnerStartWindow(user.Id);
                    start.Show();
                    this.Close();
                    break;
                case (UserType.Guide):
                    GuideHomeScreen guideHomeScreen = new GuideHomeScreen(user.Id);
                    guideHomeScreen.Show();
                    this.Close();
                    break;
                case (UserType.Guest2):
                    //HomeWindow homeWindow = new HomeWindow(user.Id);
                    //homeWindow.Show();

                    MainWindow mainWindow = new MainWindow(user.Id);
                    mainWindow.Show();
                    this.Close();
                    break;

                default:
                    StartWindow startWindow = new StartWindow(user.Id);
                    startWindow.Show();
                    this.Close();
                    break;
                
            }
        }
    }
}
