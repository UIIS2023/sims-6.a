using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guest2;
using InitialProject.Services.IServices;
using InitialProject.View.Guest2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    public class SimpleRequestViewModel : INotifyPropertyChanged
    {
        public RelayCommand SelectedDateChangedCommand { get; set; }
        public RelayCommand AcceptCommand { get; set; }
        public RelayCommand RejectCommand { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public TourRequest TourRequest { get; set; }
        public static ObservableCollection<string> Countries { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<Language> Languages { get; set; }
        private readonly ILanguageService _languageService;
        private readonly ITourRequestService _tourRequestService;
        private readonly Services.IServices.ILocationService _locationService;
        public int UserId { get; set; }

        private DateTime _startDay;
        public DateTime StartDay
        {
            get
            {
                return _startDay;
            }
            set
            {
                if (value != _startDay)
                {
                    _startDay = value;
                    OnPropertyChanged("StartDay");
                }
            }
        } 
        
        private DateTime _endDay;
        public DateTime EndDay
        {
            get
            {
                return _endDay;
            }
            set
            {
                if (value != _endDay)
                {
                    _endDay = value;
                    OnPropertyChanged("EndDay");
                }
            }
        }

        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged("City");
                }
            }
        }
        
        private string _country;
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged("Country");
                }
            }
        }

        private string _language;
        public string Language
        {
            get
            {
                return _language;
            }
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged("Language");
                }
            }
        }  
        
        private int _guestNumber;
        public int GuestNumber
        {
            get
            {
                return _guestNumber;
            }
            set
            {
                if (value != _guestNumber)
                {
                    _guestNumber = value;
                    OnPropertyChanged("GuestNumber");
                }
            }
        }    
        
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        public SimpleRequestViewModel(int userId)
        {
            userId = userId;
            TourRequest = new TourRequest();
            TourRequest.StartingDate = DateTime.Today.AddDays(2);
            TourRequest.EndingDate = DateTime.Today.AddDays(2);
            TourRequest.UserId = userId;
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
            _languageService = Injector.CreateInstance<ILanguageService>();
            _locationService = Injector.CreateInstance<Services.IServices.ILocationService>();
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            Locations = new ObservableCollection<Location>(_locationService.GetAll());
            Languages = new ObservableCollection<Language>(_languageService.GetAll());
            ReadCitiesAndCountries();
            SelectedDateChangedCommand = new RelayCommand(SelectedDateChanged);
            AcceptCommand = new RelayCommand(AcceptSimpleRequest);
            RejectCommand = new RelayCommand(Close);
            //DatePickerStart.DisplayDateStart = DateTime.Today.AddDays(2);                      Provjeriti kako se binduje preko viewmodela
            //DatePickerEnd.DisplayDateStart = DateTime.Today.AddDays(2);
        }

        private void SelectedDateChanged(object parameter)
        {
            _endDay = _startDay;
        }  
        
        private void AcceptSimpleRequest(object parameter)
        {
            TourRequest.Location = new Location { Country = _country, City = _city };
            _tourRequestService.MakeTourRequest(TourRequest);
            Close(parameter);
        } 
        
        private void Close(object parameter)
        {
            App.Current.MainWindow = App.Current.Windows.OfType<SimpleRequest>().FirstOrDefault();
            App.Current.MainWindow.Close();
        }

        private void ReadCitiesAndCountries()
        {
            Cities.Clear();
            Countries.Clear();
            Cities.Add("");
            Countries.Add("");


            foreach (Location location in Locations)
            {
                Cities.Add(location.City);
                if (!Countries.Contains(location.Country))
                {
                    Countries.Add(location.Country);
                }
            }
        }

        private void UpdateCitiesList(string country)
        {
            Cities.Clear();
            Cities.Add("");


            var filteredCities = Locations.Where(loc => loc.Country == country)
                                          .Select(loc => loc.City);

            foreach (string city in filteredCities)
            {
                Cities.Add(city);
            }

            //CityComboBox.SelectedIndex = 1;                                                  Provjeriti kako se binduje preko viewmodela
        }

        private void FilterCities(object sender, SelectionChangedEventArgs e)
        {
            if (sender is not ComboBox cmbx) return;
            string country = cmbx.SelectedItem?.ToString() ?? string.Empty;
            if (string.IsNullOrEmpty(country))
            {
                ReadCitiesAndCountries();
            }
            else
            {
                UpdateCitiesList(country);
            }
        }

        private void SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            //TourRequest.EndingDate = (DateTime)DatePickerStart.SelectedDate;               Provjeriti kako se binduje preko viewmodela
            //DatePickerEnd.SelectedDate = TourRequest.EndingDate;
            //DatePickerEnd.DisplayDateStart = TourRequest.EndingDate;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
