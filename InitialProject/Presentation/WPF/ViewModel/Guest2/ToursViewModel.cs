namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Services.IServices;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public class ToursViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly ITourService _tourService;

        public RelayCommand PreviewTourCommand { get; set; }
        public RelayCommand ApplyFiltersCommand { get; set; }
        public static ObservableCollection<string> Duration { get; set; }
        public static ObservableCollection<int> GuestNumber { get; set; }
        public static ObservableCollection<string> Languages { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<string> Countries { get; set; }
        public Tour SelectedTour { get; set; }
        private ObservableCollection<Tour> _tours { get; set; }
        public ObservableCollection<Tour> Tours
        {
            get { return _tours; }
            set
            {
                _tours = value;
                OnPropertyChanged(nameof(Tours));
            }
        }

        public int NumberOfGuests { get; set; }
        public string SelectedLanguage { get; set; }
        public string SelectedCity { get; set; }
        public string SelectedGuestNumber { get; set; }
        public string SelectedDurationFrom { get; set; }
        public string SelectedDurationTo { get; set; }


        public ToursViewModel()
        {
            _tourService = Injector.CreateInstance<ITourService>();
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            Tours = new ObservableCollection<Tour>(_tourService.GetAllNotStartedTours());
            ApplyFiltersCommand = new RelayCommand(ApplyFilters);
            PreviewTourCommand = new RelayCommand(PreviewTour);

            InitializeLanguages();
            InitializeLocations();
            InitializeGuestNumber();
            InitializeDuration();
            ReadCitiesAndCountries();

            
        }

        public void ApplyFilters(object parameter)
        {
            Tours = new ObservableCollection<Tour>(_tourService.GetAllFiltered(SelectedCity, SelectedCountry, SelectedDurationFrom, SelectedDurationTo, SelectedLanguage, SelectedGuestNumber));
        }

        public void PreviewTour(object parameter)
        {
            View.Guest2.Views.TourView tourView = new View.Guest2.Views.TourView(SelectedTour);
            //this.NavigationService.Navigate(tourView);
        }
        private string _selectedCountry;
        public string SelectedCountry
        {
            get => _selectedCountry;
            set
            {
                if (value != _selectedCountry)
                {
                    _selectedCountry = value;
                    FilterCities();
                    OnPropertyChanged("SelectedCountry");
                }
            }
        }

        private void InitializeLanguages()
        {
            Languages = new ObservableCollection<string>();
            Languages.Insert(0, string.Empty);
            foreach (Tour t in Tours)
            {

                if (!Languages.Contains(t.Language.Name))
                {
                    Languages.Add(t.Language.Name);
                }

            }
        }

        private void InitializeLocations()
        {
            Locations = new ObservableCollection<Location>();
            foreach (Tour t in Tours)
            {
                if (!Locations.Contains(t.Location))
                {
                    Locations.Add(t.Location);
                }
            }
        }

        private void InitializeGuestNumber()
        {
            GuestNumber = new ObservableCollection<int>();
            int max = 1;
            foreach (Tour t in Tours)
            {
                if (max < t.MaxGuestNumber)
                {
                    max = t.MaxGuestNumber;
                }
            }
            for (int i = 1; i <= max; i++)
            {
                GuestNumber.Add(i);
            }
        }

        private void InitializeDuration()
        {
            Duration = new ObservableCollection<string>();
            int max = 1;
            foreach (Tour t in Tours)
            {
                if (max < t.Duration)
                {
                    max = t.Duration;
                }
            }
            for (int i = 1; i <= max; i++)
            {
                Duration.Add(i.ToString());
            }
            Duration.Insert(0, string.Empty);
        }

        private void ReadCitiesAndCountries()
        {
            Cities.Clear();
            Countries.Clear();
            foreach (Location l in Locations)
            {
                if (!Cities.Contains(l.City))
                {
                    Cities.Add(l.City);
                }
                if (!Countries.Contains(l.Country))
                {
                    Countries.Add(l.Country);
                }
            }
            Countries.Insert(0, string.Empty);
            Cities.Insert(0, string.Empty);
        }

        private void Filter_Countries(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FilterCities()
        {
            if (string.IsNullOrEmpty(SelectedCountry))
            {
                ReadCitiesAndCountries();
                SelectedCity = Cities.FirstOrDefault();
            }
            else
            {
                Cities.Clear();
                foreach (Location loc in Locations)
                {
                    if (loc.Country == SelectedCountry && !Cities.Contains(loc.City))
                    {
                        Cities.Add(loc.City);
                    }
                }
                Cities.Insert(0, string.Empty);
                SelectedCity = Cities[0];
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
