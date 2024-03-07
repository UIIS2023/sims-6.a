using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.View.Guest2
{
    /// <summary>
    /// Interaction logic for ComplexRequest.xaml
    /// </summary>
    public partial class ComplexRequest : Window, INotifyPropertyChanged
    {
        public ComplexTourRequest ComplexTourRequest { get; set; }
        public ObservableCollection<ComplexTourRequest> ComplexTourRequests { get; set; }
        public static ObservableCollection<string> Countries { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<Language> Languages { get; set; }
        private int _userId { get; set; }
        private readonly ILanguageService _languageService;
        private readonly IComplexTourRequestService _complexTourRequestService;
        private readonly Services.IServices.ILocationService _locationService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ComplexRequest(int userId)
        {
            InitializeComponent();

            _userId = userId;
            ComplexTourRequest = new ComplexTourRequest();
            ComplexTourRequest.StartingDate = DateTime.Today.AddDays(2);
            ComplexTourRequest.EndingDate = DateTime.Today.AddDays(2);
            DataContext = this;
            ComplexTourRequest.UserId = userId;
            _complexTourRequestService = Injector.CreateInstance<IComplexTourRequestService>();
            _languageService = Injector.CreateInstance<ILanguageService>();
            _locationService = Injector.CreateInstance<Services.IServices.ILocationService>();
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            Locations = new ObservableCollection<Location>(_locationService.GetAll());
            Languages = new ObservableCollection<Language>(_languageService.GetAll());
            ReadCitiesAndCountries();
            DatePickerStart.DisplayDateStart = DateTime.Today.AddDays(2);
            DatePickerEnd.DisplayDateStart = DateTime.Today.AddDays(2);
            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            ComplexTourRequest.Id = ComplexTourRequests.Count() + 1;
            ComplexTourRequest.Location = new Location { Country = CountryComboBox.Text, City = CityComboBox.Text };
            ComplexTourRequests.Add(ComplexTourRequest);
            ComplexTourRequest = new ComplexTourRequest();
            ComplexTourRequest.StartingDate = DateTime.Today.AddDays(2);
            ComplexTourRequest.EndingDate = DateTime.Today.AddDays(2);
            ComplexTourRequest.UserId = _userId;
            CountryComboBox.SelectedIndex = 0;
            CityComboBox.SelectedIndex = 0;
            LanguageComboBox.SelectedIndex = 0;
            ComplexTourRequest.TourName = ComplexTourRequests.ElementAt(0).TourName;
            TourName.IsReadOnly = true;
            //    OnPropertyChanged(nameof(ComplexTourRequest));
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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

            CityComboBox.SelectedIndex = 1;
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

        private void DatePickerStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ComplexTourRequest.EndingDate = (DateTime)DatePickerStart.SelectedDate;
            DatePickerEnd.SelectedDate = ComplexTourRequest.EndingDate;
            DatePickerEnd.DisplayDateStart = ComplexTourRequest.EndingDate;
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
            _complexTourRequestService.MakeTourRequest(new List<ComplexTourRequest>(ComplexTourRequests));
            this.Close();
        }
    }
}
