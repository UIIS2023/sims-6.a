using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel;
using InitialProject.Repository;
using InitialProject.View.Guest1;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for guest1view.xaml
    /// </summary>
    public partial class AccommodationDisplay : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private readonly ReservationRepository _reservationRepository;
        private int _userId;
        public RelayCommand ApplyFilters_Command { get; set; }
        public RelayCommand RestoreFilters_Command { get; set; }
        public RelayCommand FocusFilters_Command { get; set; }
        public RelayCommand FocusTable_Command { get; set; }
        public static ObservableCollection<Reservation> Reservations { get; set; }
        public static ObservableCollection<AccommodationReservation> AccommodationReservations { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<string> Countries { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        private ObservableCollection<Accommodation> _accommodations;
        public ObservableCollection<Accommodation> Accommodations
        {
            get { return _accommodations; }
            set
            {
                _accommodations = value;
                OnPropertyChanged(nameof(Accommodations));
            }
        }
        private string _accommodationName;
        public string AccommodationName
        {
            get => _accommodationName;
            set
            {
                if (value != _accommodationName)
                {
                    _accommodationName = value;
                    OnPropertyChanged("AccommodationName");
                }
            }
        }
        private string _selectedCity;
        public string SelectedCity
        {
            get => _selectedCity;
            set
            {
                if (value != _selectedCity)
                {
                    _selectedCity = value;
                    OnPropertyChanged("SelectedCity");
                }
            }
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
        public int NumberOfGuests { get; set; }
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
                        int.TryParse(value, out _numberOfGuests);
                        NumberOfGuests = _numberOfGuests;
                    }
                    catch (Exception) { }
                    _strNumberOfGuests = value;
                    OnPropertyChanged();
                }
            }
        }
        public int ReservationDays { get; set; }
        private string _strReservationDays;
        public string StrReservationDays
        {
            get => _strReservationDays;
            set
            {
                if (value != _strReservationDays)
                {
                    try
                    {
                        int _reservationDays;
                        int.TryParse(value, out _reservationDays);
                        ReservationDays = _reservationDays;
                    }
                    catch (Exception) { }
                    _strReservationDays = value;
                    OnPropertyChanged();
                }
            }
        }
        private bool _isAppartmentSelected;
        public bool IsAppartmentSelected
        {
            get => _isAppartmentSelected;
            set
            {
                if (_isAppartmentSelected != value)
                {
                    _isAppartmentSelected = value;
                    OnPropertyChanged(nameof(IsAppartmentSelected));
                }
            }
        }
        private bool _isHouseSelected;
        public bool IsHouseSelected
        {
            get => _isHouseSelected;
            set
            {
                if (_isHouseSelected != value)
                {
                    _isHouseSelected = value;
                    OnPropertyChanged(nameof(IsHouseSelected));
                }
            }
        }
        private bool _isShackSelected;
        public bool IsShackSelected
        {
            get => _isShackSelected;
            set
            {
                if (_isShackSelected != value)
                {
                    _isShackSelected = value;
                    OnPropertyChanged(nameof(IsShackSelected));
                }
            }
        }
        private int _accommodationsNumber;
        public int AccommodationsNumber
        {
            get => _accommodationsNumber;
            set
            {
                if (value != _accommodationsNumber)
                {
                    _accommodationsNumber = value;
                    OnPropertyChanged("AccommodationsNumber");
                }
            }
        }
        public AccommodationDisplay(int userId)
        {
            InitializeComponent();
            DataContext = this;
            _accommodationRepository = new AccommodationRepository();
            _reservationRepository = new ReservationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            InitializeCollecitons();
            InitializeCommands();
            InitializeReservationsByAccommodations();
            ReadCitiesAndCountries();
            AccommodationsNumber = Accommodations.Count;
            _userId = userId;
        }

        private void InitializeCommands()
        {
            ApplyFilters_Command = new RelayCommand(ApplyFilters);
            RestoreFilters_Command = new RelayCommand(RestoreFilters);
            FocusFilters_Command = new RelayCommand(FocusFilters);
            FocusTable_Command = new RelayCommand(FocusTable);
        }

        private void InitializeCollecitons()
        {
            Cities = new ObservableCollection<string>();
            Countries = new ObservableCollection<string>();
            Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            Locations = new ObservableCollection<Location>(_accommodationRepository.GetAllLocationsFromAccommodations());
            Reservations = new ObservableCollection<Reservation>(_reservationRepository.GetAll());
            AccommodationReservations = new ObservableCollection<AccommodationReservation>(_accommodationReservationRepository.GetAll());
        }

        private void InitializeNumberOfGuests(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmbx = (ComboBox)sender;
            if (!string.IsNullOrEmpty(cmbx.SelectedItem.ToString()))
            {
                NumberOfGuests = Convert.ToInt32(cmbx.SelectedItem);
            }
        }
        private void InitializeReservationsDays(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmbx = (ComboBox)sender;
            if (!string.IsNullOrEmpty(cmbx.SelectedItem.ToString()))
            {
                ReservationDays = Convert.ToInt32(cmbx.SelectedItem);
            }
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        bool AccommodationNameFilter(Accommodation accommodation)
        {
            return string.IsNullOrEmpty(AccommodationName) || accommodation.Name.ToLower().Contains(AccommodationName.ToLower());
        }
        bool CountryFilter(Accommodation accommodation)
        {
            return string.IsNullOrEmpty(SelectedCountry) || accommodation.Location.Country == SelectedCountry;
        }
        bool CityFilter(Accommodation accommodation)
        {
            return string.IsNullOrEmpty(SelectedCity) || accommodation.Location.City == SelectedCity;
        }
        bool GuestNumberFilter(Accommodation accommodation)
        {
            return string.IsNullOrEmpty(StrNumberOfGuests) || accommodation.MaxGuestNumber >= NumberOfGuests;
        }
        bool DaysReservationFilter(Accommodation accommodation)
        {
            return string.IsNullOrEmpty(StrReservationDays) || accommodation.MinReservationDays <= ReservationDays;
        }
        private bool AccommodationTypeFilter(Accommodation accommodation)
        {
            if (IsAppartmentSelected && IsHouseSelected && IsShackSelected)
            {
                return accommodation.TypeOfAccommodation == AccommodationType.Apartment ||
                    accommodation.TypeOfAccommodation == AccommodationType.House ||
                    accommodation.TypeOfAccommodation == AccommodationType.Shack;
            }
            else if (IsAppartmentSelected && IsHouseSelected)
            {
                return accommodation.TypeOfAccommodation == AccommodationType.Apartment ||
                    accommodation.TypeOfAccommodation == AccommodationType.House;
            }
            else if (IsAppartmentSelected && IsShackSelected)
            {
                return accommodation.TypeOfAccommodation == AccommodationType.Apartment ||
                    accommodation.TypeOfAccommodation == AccommodationType.Shack;
            }
            else if (IsHouseSelected && IsShackSelected)
            {
                return accommodation.TypeOfAccommodation == AccommodationType.House ||
                    accommodation.TypeOfAccommodation == AccommodationType.Shack;
            }
            else if (IsAppartmentSelected)
            {
                return accommodation.TypeOfAccommodation == AccommodationType.Apartment;
            }
            else if (IsHouseSelected)
            {
                return accommodation.TypeOfAccommodation == AccommodationType.House;
            }
            else if (IsShackSelected)
            {
                return accommodation.TypeOfAccommodation == AccommodationType.Shack;
            }
            else
            {
                return true;
            }
        }
        private void ApplyFilters(object parameter)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(Accommodations);
            view.Filter = (obj) =>
            {
                Accommodation accommodation = obj as Accommodation;
                if (accommodation == null) return false;
                return (AccommodationNameFilter(accommodation) &&
                    CountryFilter(accommodation) &&
                    CityFilter(accommodation) &&
                    GuestNumberFilter(accommodation) &&
                    DaysReservationFilter(accommodation) &&
                    AccommodationTypeFilter(accommodation));
            };
            AccommodationsNumber = view.Cast<object>().Count();
        }
        private void ReadCitiesAndCountries()
        {
            Cities.Clear();
            Countries.Clear();
            foreach (Location l in Locations)
            {
                Cities.Add(l.City);
                if (!Countries.Contains(l.Country))
                {
                    Countries.Add(l.Country);
                }
            }
            Countries.Insert(0, string.Empty);
            Cities.Insert(0, string.Empty);
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
                    if (loc.Country == SelectedCountry)
                    {
                        Cities.Add(loc.City);
                    }
                }
                Cities.Insert(0, string.Empty);
                SelectedCity = Cities[1];
            }
        }
        private void BookClick(object sender, RoutedEventArgs e)
        {
            if (SelectedAccommodation == null)
            {
                MessageBox.Show("Please select accommodation.");
            }
            else
            {
                AccommodationReservationForm accommodationReservationFormWindow = new AccommodationReservationForm(SelectedAccommodation, _userId);
                accommodationReservationFormWindow.Owner = this;
                accommodationReservationFormWindow.ShowDialog();
            }
        }
        private void InitializeReservationsByAccommodations()
        {
            foreach (AccommodationReservation accommodationReservation in AccommodationReservations)
            {
                Accommodation accommodation = Accommodations.ToList().Find(a => a.AccommodationID == accommodationReservation.AccommodationId);
                Reservation reservation = FindReservation(accommodationReservation);
                if (reservation != null)
                {
                    accommodation.Reservations.Add(reservation);
                }
            }
        }
        private Reservation FindReservation(AccommodationReservation accommodationReservation)
        {
            Reservation founded = Reservations.ToList().Find(r => r.ReservationId == accommodationReservation.ReservationId && r.Status != ReservationStatus.Finished);
            return founded;
        }
        private void RestoreFilters(object parameter)
        {
            AccommodationName = string.Empty;
            SelectedCity = string.Empty;
            SelectedCountry = string.Empty;
            IsAppartmentSelected = false;
            IsShackSelected = false;
            IsHouseSelected = false;
            StrNumberOfGuests = string.Empty;
            StrReservationDays = string.Empty;
            ApplyFilters(parameter);
        }
        private void FocusFilters(object parameter)
        {
            var textBox = parameter as TextBox;
            textBox.Focus();
        }
        private void FocusTable(object parameter)
        {
            var dataGrid = parameter as DataGrid;
            dataGrid.Focus();
            dataGrid.SelectedItem = dataGrid.Items[0];
            dataGrid.ScrollIntoView(dataGrid.SelectedItem);
        }

        private void accommodationsDataGrid_SelectionChanged()
        {

        }
    }
}
