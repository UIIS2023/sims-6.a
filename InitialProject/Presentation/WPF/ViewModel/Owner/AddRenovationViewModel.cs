using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using InitialProject.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class AddRenovationViewModel : BindableBase,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        private readonly IRenovationService _renovationService;
        private int _accommodationId { get; set; }

        public DateRange SelectedDateRange { get; set; }

        
        private Accommodation _selectedAccommodation;
        public Accommodation SelectedAccommodation
        {
            get => _selectedAccommodation;
            set
            {
                if (_selectedAccommodation != value)
                {
                    _selectedAccommodation = value;
                    OnPropertyChanged(nameof(_selectedAccommodation));
                }
            }
        }
        
        public Renovation _renovation = new Renovation();

        public Renovation NewRenovation
        {
            get { return _renovation; }
            set
            {
                _renovation = value;
                OnPropertyChanged(nameof(NewRenovation));
            }
        }

        public ObservableCollection<DateRange> _availableDates;
        public ObservableCollection<DateRange> AvailableDates
        {
            get { return _availableDates; }
            set
            {
                _availableDates = value;
                OnPropertyChanged(nameof(AvailableDates));
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
                    //EndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, StartDay));
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
        /*
        private int _days;

        public int Days
        {
            get { return _days; }
            set
            {
                _days = value;
                OnPropertyChanged("Days");
            }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        */
        public RelayCommand AddCommand { get; set; }
        public RelayCommand SearchCommand { get; set; }

        public AddRenovationViewModel(Accommodation selectedAccommodation)
        {
            _renovationService = Injector.CreateInstance<IRenovationService>();
            _selectedAccommodation = selectedAccommodation;
            _accommodationId = selectedAccommodation.AccommodationID;

            AvailableDates = new ObservableCollection<DateRange>();
            // StartDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));
            // EndDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));

            AddCommand = new RelayCommand(AddRenovation_ButtonClick);
            SearchCommand = new RelayCommand(SearchDates_ButtonClick);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        
        public void SearchDates_ButtonClick(object parameter)
        {
            AvailableDates.Clear();
            AvailableDates = _renovationService.GetAvailableDates(_selectedAccommodation, StartDay, EndDay, NewRenovation.Days);
        }

        public void AddRenovation_ButtonClick(object parameter)
        {
            NewRenovation.Validate();
            if (NewRenovation.IsValid)
            {
            Renovation renovation = _renovationService.CreateNewRenovation(_selectedAccommodation.Name, _accommodationId, SelectedDateRange, NewRenovation.Description);
            _renovationService.SaveRenovation(renovation);
            }
            else
            {
                OnPropertyChanged(nameof(NewRenovation));  
            }

        }
        
    }
}
