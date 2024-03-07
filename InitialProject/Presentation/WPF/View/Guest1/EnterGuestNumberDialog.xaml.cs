using InitialProject.Presentation.WPF.ViewModel;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for EnterGuestNumberDialog.xaml
    /// </summary>
    public partial class EnterGuestNumberDialog : Window, INotifyPropertyChanged
    {
        private bool _isEnabled;

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool CanExecute
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                OnPropertyChanged(nameof(CanExecute));
            }
        }
        private RelayCommand reserveCommand;
        public RelayCommand ReserveCommand
        {
            get
            {
                if (reserveCommand == null)
                {
                    reserveCommand = new RelayCommand(ReserveAccommodation, CanReserve);
                }
                return reserveCommand;
            }
        }
        public RelayCommand CancelCommand { get; private set; }
        public int NumberOfGuests
        {
            get;
            set;
        }
        private string _strNumberOfGuests;
        public string StrNumberOfGuests
        {
            get => _strNumberOfGuests;
            set
            {
                if (value != _strNumberOfGuests)
                {
                    try
                    {
                        int _numberOfGuests;
                        if (CanExecute = int.TryParse(value, out _numberOfGuests))
                        {
                            NumberOfGuests = _numberOfGuests;
                        }
                    }
                    catch (Exception) { }
                    _strNumberOfGuests = value;
                    OnPropertyChanged(nameof(StrNumberOfGuests));
                }
            }
        }
        public int MaxAccommodationGuestsNumber { get; set; }
        public EnterGuestNumberDialog(int maxAccommodationGuestsNumber)
        {
            InitializeComponent();
            DataContext = this;
            CanExecute = false;
            MaxAccommodationGuestsNumber = maxAccommodationGuestsNumber;
            //ReserveCommand = new RelayCommand(ReserveAccommodation, CanReserve);
            CancelCommand = new RelayCommand(CloseWindow);
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private void ReserveAccommodation(object parameter)
        {
            if (NumberOfGuests > MaxAccommodationGuestsNumber)
            {
                MessageBox.Show("Number of guests must be bellow " + MaxAccommodationGuestsNumber.ToString());
            }
            else
            {
                this.Close();
            }
        }
        private void CloseWindow(object parameter)
        {
            NumberOfGuests = 0;
            this.Close();
        }
        private bool CanReserve(object parameter)
        {
            return CanExecute;
        }
    }
}
