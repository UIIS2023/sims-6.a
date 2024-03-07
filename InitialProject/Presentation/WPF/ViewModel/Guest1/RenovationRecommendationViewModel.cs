using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guest1;
using InitialProject.Services.IServices;
using InitialProject.View.Guest1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Guest1
{
    public class RenovationRecommendationViewModel : INotifyPropertyChanged
    {
        private string _selectedLevel;
        private bool _canSubmit;
        private int _accommodationId;
        public event PropertyChangedEventHandler? PropertyChanged;
        public Dictionary<string, string> Levels { get; set; }
        public RelayCommand SubmitCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }
        public RelayCommand F3OpenLevels { get; private set; }
        public string Comment { get; set; }
        private readonly IRenovationRecommendationService _renovationService;
        private readonly IAccommodationService _accommodationService;
        private readonly IUserService _userService;
        public string SelectedLevel
        {
            get => _selectedLevel;
            set
            {
                if(_selectedLevel != value)
                {
                    _selectedLevel = value;
                    CanSubmit = true;
                    OnPropertyChanged(nameof(SelectedLevel));
                    OnPropertyChanged(nameof(CanSubmit));
                }
            }
        }
        public bool CanSubmit
        {
            get => _canSubmit;
            set
            {
                if (_canSubmit != value)
                {
                    _canSubmit = value;
                    OnPropertyChanged(nameof(CanSubmit));
                }
            }
        }
        public RenovationRecommendationViewModel(int accommdoationId)
        {
            _renovationService = Injector.CreateInstance<IRenovationRecommendationService>();
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
            _userService = Injector.CreateInstance<IUserService>();
            _accommodationId = accommdoationId;
            Levels = RenovationLevels.Levels;
            CanSubmit = false;
            SubmitCommand = new RelayCommand(SubmitRecommendation);
            CloseCommand = new RelayCommand(CloseWindow);
            F3OpenLevels = new RelayCommand(OpenRenovationLevelsOverview);
        }
        public void SubmitRecommendation(object parameter)
        {
            int userId = _userService.GetUserId();
            int ownerId = _accommodationService.GetOwnerIdByAccommodationId(_accommodationId);
            RenovationRecommendation renovationRecommendation = new RenovationRecommendation(ownerId, _accommodationId, userId, SelectedLevel, Comment);
            _renovationService.Save(renovationRecommendation);
            Window window = App.Current.MainWindow;
            window.Close();
        }
        public void CloseWindow(object parameter)
        {
            if (parameter is Window window)
            {
                window.Close();
            }
        }
        public void OpenRenovationLevelsOverview(object parameter)
        {
            RenovationLevelsOverview dialog = new RenovationLevelsOverview(); 
            dialog.Owner = App.Current.MainWindow;
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner; 
            dialog.ShowDialog();
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
