using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace InitialProject.Presentation.WPF.ViewModel
{
    public class ReviewCommentsViewModel
    {
        private readonly ITourService _tourService;

        private readonly ITourRateService _tourRateService;

        private readonly ITourPointService _tourPointService;

        private readonly ITourAttendanceService _tourAttendanceService;

        public ObservableCollection<string> Tours { get; set; }
        public ObservableCollection<TourAttendance> UserOnTours { get; set; }
        public ObservableCollection<TourRate> Reviews { get; set; }

        private TourRate _SelectedReview;
        public TourRate SelectedReview
        {
            get => _SelectedReview;
            set
            {
                if (_SelectedReview != value)
                {
                    _SelectedReview = value;
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

        public ReviewCommentsViewModel()
        {
            _tourService = Injector.CreateInstance<ITourService>();
            _tourRateService = Injector.CreateInstance<ITourRateService>();
            _tourPointService = Injector.CreateInstance<ITourPointService>();
            _tourAttendanceService = Injector.CreateInstance<ITourAttendanceService>();
            Reviews = new ObservableCollection<TourRate>();
            UserOnTours = new ObservableCollection<TourAttendance>();
            LoadTours();
        }


        public void GetAllRatesForSelectedTour()
        {
            int tourId = int.Parse(SelectedTour.Split(' ')[0]);
            List<TourRate> tourRates = _tourRateService.GetAllRates().Where(c => c.TourId == tourId).ToList();
            Reviews.Clear();
            foreach (var rate in tourRates)
            {
                Reviews.Add(rate);
            }
        }

        public void ShowAllTourPointsForId()
        {
            int tourId = int.Parse(SelectedTour.Split(' ')[0]);
            List<TourAttendance> attendances = _tourAttendanceService.GetAll().Where(t => t.TourId == tourId && t.UserId == SelectedReview.GuestId).ToList();

            UserOnTours.Clear();
            foreach (var attendance in attendances)
            {
                UserOnTours.Add(attendance);
            }
        }

        public void SetReviewToNotValid()
        {
            SelectedReview.IsValid = false;
            _tourRateService.Update(SelectedReview);
            GetAllRatesForSelectedTour();
        }

        private void LoadTours()
        {
            Tours = new ObservableCollection<string>(_tourService.GetAllFinishedTours().Select(c => c.TourId + " " + c.Name));
        }
    }
}
