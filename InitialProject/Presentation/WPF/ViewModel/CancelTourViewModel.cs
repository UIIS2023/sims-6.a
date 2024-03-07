using InitialProject.Aplication.Factory;
using InitialProject.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.Presentation.WPF.ViewModel
{
    public class CancelTourViewModel : INotifyPropertyChanged
    {
        private readonly ICancelTourService _cancelTourService;
        public ObservableCollection<string> Tours { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand CloseCommand { get; set; }

        private readonly Window _window;
        public CancelTourViewModel(Window window)
        {
            _window = window;
            _cancelTourService = Injector.CreateInstance<ICancelTourService>();
            LoadTours();
            CancelCommand = new RelayCommand(CancelTour);
            CloseCommand = new RelayCommand(Close);

        }


        

        private bool _isCancelEnabled;
        public bool IsCancelEnabled
        {
            get => _isCancelEnabled;
            set
            {
                if (_isCancelEnabled != value)
                {
                    _isCancelEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _SelectedTour;
        public string SelectedTour
        {
            get => _SelectedTour;
            set
            {
                if (_SelectedTour != value)
                {
                    _SelectedTour = value;
                    OnPropertyChanged();
                    IsCancelEnabled = !string.IsNullOrEmpty(_SelectedTour);
                    
                }
            }
        }

        public void Close(object parameter)
        {
            _window.Close();
        }


        private void LoadTours()
        {
            Tours = new ObservableCollection<string>(_cancelTourService.GetAllTwoDaysFromNow().Select(c => c.TourId + " " + c.Name + " " + c.StartingDateTime));
        }

        public void CancelTour(object obj)
        {
            _cancelTourService.CancelTour(SelectedTour, null);
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
