using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AccommodationReservationForm.xaml
    /// </summary>
    public partial class AccommodationReservationForm : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ObservableCollection<DateRange> _dateRanges;
        private readonly ReservationRepository _reservationRepository;
        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private int _userId;
        public RelayCommand ApplyCommand { get; private set; }
        public RelayCommand CloseCommand { get; private set; }
        public DateRange SelectedDateRange { get; set; }
        public Accommodation SelectedAccommodation { get; set; }
        public string AccommodationName { get; set; }
        public List<Reservation> Reservations { get; set; }
        public ObservableCollection<DateRange> DateRanges
        {
            get { return _dateRanges; }
            set
            {
                _dateRanges = value;
                OnPropertyChanged(nameof(DateRanges));
            }
        }
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
                    EndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, StartDay));
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
                        IsEnabled = int.TryParse(value, out _reservationDays);
                        ReservationDays = _reservationDays;
                    }
                    catch (Exception) { }
                    _strReservationDays = value;
                    OnPropertyChanged(nameof(StrReservationDays));
                }
            }
        }
        private int _numberOfGuests;
        public int NumberOfGuests
        {
            get { return _numberOfGuests; }
            set
            {
                _numberOfGuests = value;
                OnPropertyChanged("NumberOfGuests");
            }
        }
        private bool _isEnabled;
        public bool IsEnabled
        {
            get 
            { 
                return _isEnabled; 
            }
            set 
            { 
                _isEnabled = value; 
                OnPropertyChanged(nameof(IsEnabled)); 
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public NumericValidation validation { get; set; }
        public AccommodationReservationForm(Accommodation accommodation, int userId)
        {
            InitializeComponent();
            _userId = userId;
            this.DataContext = this;
            SelectedAccommodation = accommodation;
            AccommodationName = accommodation.Name;
            Reservations = accommodation.Reservations;
            _reservationRepository = new ReservationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            InitializeDatePickers();
            DateRanges = new ObservableCollection<DateRange>();
            IsEnabled = false;
            ApplyCommand = new RelayCommand(ApplyFilters_Command);
            CloseCommand = new RelayCommand(CloseWindow_Command);
            validation = new NumericValidation();
        }

        private void InitializeDatePickers()
        {
            StartDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            EndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
        }
        private void ApplyFilters_Command(object parameter)
        {
            ApplyFilters();
            var dataGrid = parameter as DataGrid;
            if(dataGrid != null && dataGrid.Items.Count > 0)
            {
                dataGrid.Focus();
                dataGrid.SelectedItem = dataGrid.Items[0];
                dataGrid.ScrollIntoView(dataGrid.SelectedItem);
            }
        }
        private void CloseWindow_Command(object parameter)
        {
            this.Close();
        }
        private List<DateRange> ExtractFreeDates(DateTime StartDay, DateTime EndDay)
        {
            List<DateRange> allDates = GetAllPossibleDates(StartDay, EndDay);
            List<DateRange> datesToRemove = new List<DateRange>();
            foreach (Reservation reservation in Reservations)
            {
                foreach (DateRange range in allDates)
                {
                    if (reservation.ReservationDateRange.WithinRange(range) && !datesToRemove.Contains(range))
                    {
                        datesToRemove.Add(range);
                    }
                }
            }
            RemoveUnavailableDates(allDates, datesToRemove);
            return allDates;
        }
        private void RemoveUnavailableDates(List<DateRange> allDates, List<DateRange> datesToRemove)
        {
            foreach (DateRange range in datesToRemove)
            {
                DateRange dateRange = allDates.Find(r => r.StartDate == range.StartDate && r.EndDate == range.EndDate);
                allDates.Remove(dateRange);
            }
        }
        private List<DateRange> GetAllPossibleDates(DateTime StartDay, DateTime EndDay)
        {
            List<DateRange> result = new List<DateRange>();
            for (var day = StartDay; day.Date <= EndDay; day = day.AddDays(1))
            {
                if(day.AddDays(ReservationDays).Date <= EndDay)
                {
                    DateRange range = new DateRange(day.Date, day.AddDays(ReservationDays).Date);
                    result.Add(range);
                }
            }
            return result;
        }
        private void ApplyFiltersButton(object sender, RoutedEventArgs e)
        {
            ApplyFilters();
        }

        private void ApplyFilters()
        {
            NoFreeReservation.Visibility = Visibility.Hidden;
            DateRanges.Clear();
            ShowFreeDatesForReservation();
        }

        private void ShowFreeDatesForReservation()
        {
            List<DateRange> freeDates = ExtractFreeDates(StartDay, EndDay);
            if (freeDates.Count == 0)
            {
                NoFreeReservation.Visibility = Visibility.Visible;
                DateRanges = new ObservableCollection<DateRange>(ExtractFreeDates(DateTime.Now, DateTime.Now.AddDays(90)));
            }
            else
            {
                DateRanges = new ObservableCollection<DateRange>(freeDates);
            }
        }
        private void DataGritMenuItemClick(object sender, RoutedEventArgs e)
        {
            EnterGuestNumberDialog guestNumberInputDialog = new EnterGuestNumberDialog(SelectedAccommodation.MaxGuestNumber);
            guestNumberInputDialog.Owner = this;
            guestNumberInputDialog.ShowDialog();
            if(guestNumberInputDialog.NumberOfGuests != 0)
            {
                NumberOfGuests = guestNumberInputDialog.NumberOfGuests;
                ReserveAccommodation(SelectedAccommodation.AccommodationID, _userId, SelectedDateRange, NumberOfGuests);
                MessageBox.Show("You successfuly reserved " + ReservationDays.ToString() + " day(s) at " + AccommodationName);
                guestNumberInputDialog.Close();
            }
        }
        private void ReserveAccommodation(int accommodationID, int userID, DateRange dateRange, int numberOfGuests)
        {
            Reservation reservation = new Reservation(dateRange, numberOfGuests, userID);
            AccommodationReservation accommodationReservation = new AccommodationReservation(accommodationID, _reservationRepository.NextId());
            SelectedAccommodation.Reservations.Add(reservation);
            Reservations.Add(reservation);
            DateRanges.Clear();
            ShowFreeDatesForReservation();
            _reservationRepository.Save(reservation);
            _accommodationReservationRepository.Save(accommodationReservation);
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}