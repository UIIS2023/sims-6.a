using InitialProject.Domen.CustomClasses;
using InitialProject.Services;
using InitialProject.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class MonthlyStatisticsViewModel : BindableBase , INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private AccommodationStatisticsService _accommodationStatisticsService;
        private int AccommodationId;
        private int Year;

        public ObservableCollection<StatisticsByMonthDTO> _monthlyStatistics;

        public ObservableCollection<StatisticsByMonthDTO> StatisticsByMonthDTOs
        {
            get { return _monthlyStatistics; }
            set
            {
                _monthlyStatistics = value;
                OnPropertyChanged(nameof(StatisticsByMonthDTOs));
            }
        }

        public MonthlyStatisticsViewModel(int accommodationId,int year) 
        {
            _accommodationStatisticsService = new AccommodationStatisticsService();
            AccommodationId= accommodationId;
            Year= year;

            StatisticsByMonthDTOs = new ObservableCollection<StatisticsByMonthDTO>(_accommodationStatisticsService.MonthStatisticsForYear(AccommodationId,Year));
        
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
