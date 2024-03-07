using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class EditingTimeOnTourViewModel : INotifyPropertyChanged
    {
        //public ObservableCollection<DateTime> Dates { get; set; }
        public ObservableCollection<Dates> ReservedDates { get; set; }

        public List<DateTime> Dates;


        public List<DateTime> AvailableDatesForReservation;
        public ICommand DeleteCommand { get; set; }
        public ICommand AddDateCommand { get; set; }
        public EditingTimeOnTourViewModel(List<DateTime> startingDates, List<DateTime> availableDates)
        {
            AddDateCommand = new RelayCommand(AddDate);
            DeleteCommand = new RelayCommand(DeleteDate);

            Hours = new ObservableCollection<string>();
            Minutes = new ObservableCollection<string>();
            ReservedDates = new ObservableCollection<Dates>();

            Dates = startingDates;
            AvailableDatesForReservation = availableDates;

            foreach(var date in startingDates)
            {
                Dates dates = new Dates();
                dates.Date = date;
                ReservedDates.Add(dates);
            }

            List<DateTime> tourStartingDates = startingDates;
            LoadHoursAndMinutes();

            if (availableDates != null)
                RestrictDates();

        }

        public void RestrictDates()
        {
            AvailableDates = new();
            foreach (var date in AvailableDatesForReservation)
            {
                AvailableDates.Add(date);
            }
        }

        public void AddDate(object obj)
        {
            if (SelectedDate.HasValue)
            {
                DateTime combinedDateTime = new DateTime(SelectedDate.Value.Year,
                                                         SelectedDate.Value.Month,
                                                         SelectedDate.Value.Day,
                                                         SelectedHour,
                                                         SelectedMinute,
                                                         0);
                Dates.Add(combinedDateTime);
                Dates dates = new Dates();
                dates.Date = combinedDateTime;
                ReservedDates.Add(dates);
            }
        }

        public void DeleteDate(object obj)
        {
            Dates dateToDelete = (Dates)obj;
            DateTime date = (DateTime)dateToDelete.Date;
            if (dateToDelete != null)
            {
                ReservedDates.Remove(dateToDelete);
                Dates.Remove(date);
            }
        }


        public void LoadHoursAndMinutes()
        {
            List<string> minutes = MinutesCounter();
            List<string> hours = HourCounter();

            // clear existing lists
            Hours.Clear();
            Minutes.Clear();

            // add hours and minutes to respective lists
            foreach (var hour in hours)
            {
                Hours.Add(hour);
            }
            foreach (var minute in minutes)
            {
                Minutes.Add(minute);
            }
        }

        public List<string> HourCounter()
        {
            List<string> hours = new List<string>();
            for (int i = 0; i < 24; i++)
            {
                hours.Add(i.ToString("D2"));
            }
            return hours;
        }

        public List<string> MinutesCounter()
        {
            List<string> minutes = new List<string>();
            for (int i = 0; i < 60; i++)
            {
                minutes.Add(i.ToString("D2"));
            }
            return minutes;
        }

        private ObservableCollection<string> _hours;
        public ObservableCollection<string> Hours
        {
            get => _hours;
            set
            {
                _hours = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> _minutes;


        public ObservableCollection<string> Minutes
        {
            get => _minutes;
            set
            {
                _minutes = value;
                OnPropertyChanged();
            }
        }

        private int _selectedHour;
        public int SelectedHour
        {
            get => _selectedHour;
            set
            {
                _selectedHour = value;
                OnPropertyChanged();
            }
        }

        private int _selectedMinute;
        public int SelectedMinute
        {
            get => _selectedMinute;
            set
            {
                _selectedMinute = value;
                OnPropertyChanged();
            }
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DateTime> _AvailableDates;
        public ObservableCollection<DateTime> AvailableDates
        {
            get { return _AvailableDates; }
            set
            {
                _AvailableDates = value;
                OnPropertyChanged(nameof(_AvailableDates)); // Notify UI to update
            }
        }

     
        private DateTime _startDate = DateTime.Today;
        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                _startDate = value;
                OnPropertyChanged("StartDate");
            }
        }

        private DateTime _endDate = DateTime.Today.AddYears(1);
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged("EndDate");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
