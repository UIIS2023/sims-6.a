using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace InitialProject.Presentation.WPF.ViewModel
{

    public class TourStatisticsViewModel : INotifyPropertyChanged
    {
        private readonly ITourService _tourService;

        private readonly ITourStatisticsService _tourStatisticsService;
        public ObservableCollection<string> Tours { get; set; }
        public ObservableCollection<string> Years { get; set; }
        public ObservableCollection<Statistic> Statistic { get; set; }
        public TourStatisticsViewModel()
        {
            _tourStatisticsService = Injector.CreateInstance<ITourStatisticsService>();
            _tourService = Injector.CreateInstance<ITourService>();
            Years = new ObservableCollection<string>(_tourStatisticsService.GetAllYears());
            Statistic = new ObservableCollection<Statistic>();
            LoadTours();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LoadTours()
        {
            Tours = new ObservableCollection<string>(_tourService.GetAllFinishedTours().Select(c => c.TourId + " " + c.Name));
        }

        private string _Year;
        public string Year
        {
            get => _Year;
            set
            {
                if (_Year != value)
                {
                    _Year = value;
                }

            }

        }

        private string _SelectedTour;
        public string SelectedTour
        {
            get => _SelectedTour;
            set
            {
                if (_SelectedTour != value)
                {
                    _SelectedTour = value;
                }
            }
        }

        public Tour GetMostVisitedTour(string year)
        {
            return _tourStatisticsService.GetMostVisitedTour(Year);
        }

        public void GenerateStatistic()
        {
            Statistic.Clear();
            Statistic.Add(_tourStatisticsService.GetSpecificStatistic(SelectedTour));

            /*Statistic.Clear();
            Statistic statistic= new Statistic();
            statistic.WithoutVoucher = 5;
            statistic.WithVoucher = 5;
            statistic.LessThen = 5;
            statistic.GreaterThan = 5;
            statistic.Between = 5;
            Statistic.Add(statistic);*/



        }


    }




}
