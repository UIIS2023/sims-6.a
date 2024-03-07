using InitialProject.Domen.CustomClasses;
using InitialProject.Presentation.WPF.View.Owner;
using InitialProject.Services;
using InitialProject.Validation;
using Microsoft.VisualStudio.Services.Profile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class AccommodationStatisticsViewModel :BindableBase,INotifyPropertyChanged 
    {

        public event PropertyChangedEventHandler? PropertyChanged;


        private readonly AccommodationStatisticsService _accommodationStatisticsService;


        public ObservableCollection<StatisticsByYearDTO> _statisticsByYear;
        public ObservableCollection<StatisticsByYearDTO> StatisticsByYearDTOs
        {
            get { return _statisticsByYear; }
            set
            {
                _statisticsByYear = value;
                OnPropertyChanged(nameof(StatisticsByYearDTOs));
            }
        }

        private int AccommodationId;

        public StatisticsByYearDTO SelectedYear { get; set; }

        public RelayCommand MonthStatistics { get; set; }


        public AccommodationStatisticsViewModel(int accommodationId)
        {
            _accommodationStatisticsService = new AccommodationStatisticsService();
            AccommodationId= accommodationId;

            StatisticsByYearDTOs = new ObservableCollection<StatisticsByYearDTO>(_accommodationStatisticsService.YearStatisticsForAccommodation(AccommodationId));

            MonthStatistics = new RelayCommand(Month_ButtonClick);
        }

        private void Month_ButtonClick(object parameter)
        {
            if(SelectedYear == null)
            {
                MessageBox.Show("Please selecte a year");
            }
            else
            {
                MonthlyStatistics monthlyStatistics = new MonthlyStatistics(AccommodationId,SelectedYear.Year);
                monthlyStatistics.Show();
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
