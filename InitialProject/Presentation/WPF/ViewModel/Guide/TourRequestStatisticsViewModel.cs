using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class TourRequestStatisticsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<FilteredTourRequestStatistics> TourStatistics { get; set; }

        private readonly ITourStatisticsService statisticsService;

        private readonly ILanguageService languageService;

        private readonly ILocationService locationService;

        private readonly ITourRequestService tourRequestService;

        public RelayCommand FillterCommand { get; set; }
        public TourRequestStatisticsViewModel()
        {
            TourStatistics = new ObservableCollection<FilteredTourRequestStatistics>();

            statisticsService = Injector.CreateInstance<ITourStatisticsService>();
            languageService = Injector.CreateInstance<ILanguageService>();
            locationService = Injector.CreateInstance<ILocationService>();
            tourRequestService = Injector.CreateInstance<ITourRequestService>();

            Languages = new ObservableCollection<string>(languageService.GetAllToString());
            Countries = new ObservableCollection<string>(locationService.GetAllCountries());
            Cities = new ObservableCollection<string>(locationService.GetAllCities());
            Years = new ObservableCollection<string>(tourRequestService.GetAllYearsOfTourReqeusts());

            FillterCommand = new RelayCommand(ApplyFilters);
        }

        public void ApplyFilters(object obj)
        {
            List<FilteredTourRequestStatistics> filteredData = new();
            FilterStatisticDTO selectedData = new()
            {
                Year = SelectedYear,
                Language = SelectedLanguage,
                Country = SelectedCountry,
                City = SelectedCity,
            };

            filteredData = statisticsService.FilterData(selectedData);

            TourStatistics.Clear();

            foreach (var data in filteredData) 
            {
                TourStatistics.Add(data);
            }

        }

        private ObservableCollection<string> _years;
        public ObservableCollection<string> Years
        {
            get { return _years; }
            set
            {
                _years = value;
                OnPropertyChanged();
            }
        }

        private string _selectedYear;
        public string SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                _selectedYear = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _languages;
        public ObservableCollection<string> Languages
        {
            get { return _languages; }
            set
            {
                _languages = value;
                OnPropertyChanged();
            }
        }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                _selectedLanguage = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _countries;
        public ObservableCollection<string> Countries
        {
            get { return _countries; }
            set
            {
                _countries = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCountry;
        public string SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                _selectedCountry = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _cities;
        public ObservableCollection<string> Cities
        {
            get { return _cities; }
            set
            {
                _cities = value;
                OnPropertyChanged();
            }
        }

        private string _selectedCity;
        public string SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
