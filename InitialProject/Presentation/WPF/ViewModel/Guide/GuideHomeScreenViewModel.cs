using InitialProject.Aplication.Factory;
using InitialProject.Presentation.WPF.View.Guide;
using InitialProject.Services;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class GuideHomeScreenViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public RelayCommand CheckBoxCommand { get; }
        public RelayCommand FirstQuestionCommand { get; }
        public RelayCommand SecondQuestionCommand { get; }
        public RelayCommand ThirdQuestionCommand { get; }
        public RelayCommand CreateTourCommand { get; set; }
        public RelayCommand TourRequestCommand { get; set; }
        public RelayCommand HamburgerCommand { get; set; }
        public RelayCommand TourStatisticsCommand { get; set; }
        public RelayCommand ProfileCommand { get; set; }//GuideProfileView

        public IGuideStatusService guideStatusService;

        public int GuideId { get; private set; }

        private System.Timers.Timer popupTimer;

        public Window _window { get; set; }
        public GuideHomeScreenViewModel(Window window, int? guideId)
        {
            CheckBoxCommand = new RelayCommand(CheckBoxChanged);
            FirstQuestionCommand = new RelayCommand(FirstAnswer);
            SecondQuestionCommand = new RelayCommand(SecondAnswer);
            ThirdQuestionCommand = new RelayCommand(ThirdAnswer);
            CreateTourCommand = new RelayCommand(CreateTourView);
            TourRequestCommand = new RelayCommand(CreateTourRequestView);
            HamburgerCommand = new RelayCommand(ShowHamburgerMenu);
            TourStatisticsCommand = new RelayCommand(ShowStatistics);
            ProfileCommand = new RelayCommand(ShowProfile);

            guideStatusService = Injector.CreateInstance<IGuideStatusService>();

            _window = window;
            GuideId = (int)guideId;

            if (guideStatusService.GetStatusByUserId(GuideId).EmploymentStatus == Domen.Model.GuideStatus.Status.Unemployed)
                IsGuideEmployed = false;
            else
                IsGuideEmployed = true;


            CheckIfGuideIsSuper();

        }

        private void CheckIfGuideIsSuper()
        {
            guideStatusService.CheckIfGuideIsSuper(GuideId);
        }

        public void ShowProfile(object ob)
        {
            var profileView = new GuideProfileView(GuideId);
            profileView.Show();
        }

        public void ShowStatistics(object ob)
        {
            var statisticsView = new TourRequestStatisticsView();
            statisticsView.Show();
        }

        public void ShowHamburgerMenu(object ob)
        {
            if (GridVisibility != Visibility.Collapsed)
            {
                GridVisibility = Visibility.Collapsed;
                BurgerMenuVisibility = Visibility.Visible;
            }
            else
            {
                GridVisibility = Visibility.Visible;
                BurgerMenuVisibility = Visibility.Collapsed;

            }
        }

        private Visibility _buttonsVisibility = Visibility.Collapsed;
        public Visibility ButtonsVisibility
        {
            get => _buttonsVisibility;
            set
            {
                _buttonsVisibility = value;
                OnPropertyChanged(nameof(ButtonsVisibility));
            }
        }

        private Visibility _gridVisibility = Visibility.Visible;
        public Visibility GridVisibility
        {
            get => _gridVisibility;
            set
            {
                _gridVisibility = value;
                OnPropertyChanged(nameof(GridVisibility));
            }
        }

        

        private Visibility _burgerMenuVisibility = Visibility.Collapsed;
        public Visibility BurgerMenuVisibility
        {
            get => _burgerMenuVisibility;
            set
            {
                _burgerMenuVisibility = value;
                OnPropertyChanged(nameof(BurgerMenuVisibility));
            }
        }

        private bool isPopupVisible;
        public bool IsPopupVisible
        {
            get { return isPopupVisible; }
            set
            {
                isPopupVisible = value;
                OnPropertyChanged(nameof(IsPopupVisible));
            }
        }

        private string popupMessage;
        public string PopupMessage
        {
            get { return popupMessage; }
            set
            {
                popupMessage = value;
                OnPropertyChanged(nameof(PopupMessage));
            }
        }


        private void ShowInfoPopup(string message)
        {
            IsPopupVisible = true;

            // Close the Popup after a certain duration (e.g., 3 seconds)
            var timer = new System.Timers.Timer(3000);
            timer.Elapsed += (sender, e) =>
            {
                IsPopupVisible = false;
                timer.Stop();
                timer.Dispose();
            };
            timer.Start();
        }


        private void CheckBoxChanged(object isChecked)
        {
            bool IsChecked = (bool)isChecked;
            // Update the visibility of the buttons based on the CheckBox's state
            ButtonsVisibility = IsChecked ? Visibility.Visible : Visibility.Collapsed;
        }
        private void CreateTourView(object isChecked)
        {
            CreatingTourView tourForm = new CreatingTourView(null,null,null, GuideId);
            tourForm.Show();
            _window.Close();
        }

        private void CreateTourRequestView(object isChecked)
        {
            TourRequestsView tourRequestForm = new TourRequestsView(GuideId);
            tourRequestForm.Show();
            _window.Close();
        }


        private void FirstAnswer(object isChecked)
        {
            PopupMessage = "Temporary information 1";
        }

        private void SecondAnswer(object isChecked)
        {
            ShowInfoPopup("Temporary information 2");

        }

        private void ThirdAnswer(object isChecked)
        {
            ShowInfoPopup("Temporary information 3");

        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;


        private bool _isMenuVisible;
        public bool IsMenuVisible
        {
            get { return _isMenuVisible; }
            set
            {
                _isMenuVisible = value;
                OnPropertyChanged(nameof(IsMenuVisible));
            }
        }

        private bool _IsGuideEmployed;
        public bool IsGuideEmployed
        {
            get { return _IsGuideEmployed; }
            set
            {
                _IsGuideEmployed = value;
                OnPropertyChanged(nameof(IsGuideEmployed));
            }
        }

    }
}
