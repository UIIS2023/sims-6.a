using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class FilterTourRequestViewModel : INotifyPropertyChanged
    {
        private readonly ILanguageService languageService;
        private readonly ILocationService locationService;
        private readonly ITourRequestService tourRequestService;

        private ObservableCollection<string> _cities;
        private string _selectedCity;

        private ObservableCollection<string> _countries;
        private string _selectedCountry;

        private ObservableCollection<string> _languages;
        private string _selectedLanguage;

        private int _numberOfGuests;
        private DateTime _startDate;
        private DateTime _endDate;

        public ICommand FilterCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public ObservableCollection<TourRequest> tourRequests { get; set; }
        public FilterTourRequestViewModel(ObservableCollection<TourRequest> _tourRequests)
        {
            languageService = Injector.CreateInstance<ILanguageService>();
            locationService = Injector.CreateInstance<ILocationService>();
            tourRequestService = Injector.CreateInstance<ITourRequestService>();

            FilterCommand = new RelayCommand(ApplyFilters);
            BackCommand = new RelayCommand(Back);

            Countries = new ObservableCollection<string>(locationService.GetAllCountries());
            Cities = new ObservableCollection<string>(locationService.GetAllCities());
            Languages = new ObservableCollection<string>(languageService.GetAllToString());

            tourRequests = _tourRequests;

        }

        public void ApplyFilters(object obj)
        {
            FilterRequests dataToFilter = new FilterRequests()
            {
                TourRequests = tourRequests.ToList(),
                Country = SelectedCountry,
                City = SelectedCity,
                Language = SelectedLanguage,
                NumberOfGuests = NumberOfGuests,
                StartingDate = StartDate,
                EndingDate = EndDate
            };

            List<TourRequest> FiltereData = new();

            FiltereData = tourRequestService.FilterRequests(dataToFilter);
            tourRequests.Clear();
            foreach(var request in FiltereData)
            {
                tourRequests.Add(request);
            }

        }

        public void Back(object obj)
        {

        }





        public ObservableCollection<string> Countries
        {
            get { return _countries; }
            set
            {
                _countries = value;
                OnPropertyChanged("Locations");
            }
        }

        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged("SelectedLocation");
            }
        }

        public ObservableCollection<string> Cities
        {
            get { return _cities; }
            set
            {
                _cities = value;
                OnPropertyChanged("Locations");
            }
        }

        public string SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                OnPropertyChanged("SelectedLocation");
            }
        }

        public ObservableCollection<string> Languages
        {
            get { return _languages; }
            set
            {
                _languages = value;
                OnPropertyChanged("Languages");
            }
        }

        public string SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged("SelectedLanguage");
            }
        }

        public int NumberOfGuests
        {
            get { return _numberOfGuests; }
            set
            {
                _numberOfGuests = value;
                OnPropertyChanged("NumberOfGuests");
            }
        }

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}