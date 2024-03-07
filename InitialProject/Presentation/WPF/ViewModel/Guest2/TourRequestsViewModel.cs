using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guest2;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    public class TourRequestsViewModel
    {
        public RelayCommand StatisticViewCommand { get; set; }
        public RelayCommand CreateComplexRequestCommand { get; set; }
        public RelayCommand CreateSimpleRequestCommand { get; set; }
        public RelayCommand ComplexTourReviewCommand { get; set; }

        private readonly ITourRequestService _tourRequestService;
        private readonly IComplexTourRequestService _complexTourRequestService;
        public ObservableCollection<TourRequest> RequestedTours { get; set; }
        public static ObservableCollection<ComplexTourRequestDTO> ComplexTours { get; set; }
        public ComplexTourRequestDTO ComplexTour { get; set; }
        public int UserId { get; set; }
        public TourRequestsViewModel(int userId)
        {
            UserId = userId;
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
            _complexTourRequestService = Injector.CreateInstance<IComplexTourRequestService>();
            RequestedTours = new ObservableCollection<TourRequest>(_tourRequestService.GetAllTourRequests(userId));
            ComplexTours = new ObservableCollection<ComplexTourRequestDTO>();
            StatisticViewCommand = new RelayCommand(ViewStatistic);
            CreateComplexRequestCommand = new RelayCommand(CreateComplexRequest);
            CreateSimpleRequestCommand = new RelayCommand(CreateSimpleRequest);
            ComplexTourReviewCommand = new RelayCommand(ComplexTourReview);
            GetComplexTourRequests();
        }

        private void ComplexTourReview(object parameter)
        {
            ComplexTourReview complexTourReview = new ComplexTourReview(ComplexTour.TourId);
            complexTourReview.ShowDialog();
        }

        private void ViewStatistic(object parameter)
        {
            View.Guest2.TourStatistic tourStatistic = new View.Guest2.TourStatistic(UserId);
            tourStatistic.Show();
        }

        private void CreateComplexRequest(object parameter)
        {
            ComplexRequest complexRequest = new ComplexRequest(UserId);
            complexRequest.ShowDialog();
        }
        private void CreateSimpleRequest(object parameter)
        {
            SimpleRequest simpleRequest = new SimpleRequest(UserId);
            simpleRequest.ShowDialog();
        }

        private void GetComplexTourRequests()
        {
            List<ComplexTourRequest> _tourRequests = new List<ComplexTourRequest>(_complexTourRequestService.GetAllTourRequestsByUser(UserId));
            var ture = _tourRequests.GroupBy(tour => tour.TourId);
            foreach (var t in ture)
            {
                List<ComplexTourRequest> lista = new List<ComplexTourRequest>();
                foreach (ComplexTourRequest complex in t)
                {
                    lista.Add(complex);
                }
                ComplexTourRequestDTO _complexTourRequestDTO;
                if (lista.Where(complex => complex.RequestStatus == ComplexTourRequest.Status.Accepted).Count() == lista.Count)
                {
                    _complexTourRequestDTO = new ComplexTourRequestDTO(t.ElementAt(0).TourId, t.ElementAt(0).TourName, ComplexTourRequest.Status.Accepted, lista.Min(complex => complex.StartingDate));
                }
                else if (lista.Where(complex => complex.RequestStatus == ComplexTourRequest.Status.Rejected).Count() != 0)
                {
                    _complexTourRequestDTO = new ComplexTourRequestDTO(t.ElementAt(0).TourId, t.ElementAt(0).TourName, ComplexTourRequest.Status.Rejected, lista.Min(complex => complex.StartingDate));
                }
                else
                {
                    _complexTourRequestDTO = new ComplexTourRequestDTO(t.ElementAt(0).TourId, t.ElementAt(0).TourName, ComplexTourRequest.Status.OnHold, lista.Min(complex => complex.StartingDate));
                }

                ComplexTours.Add(_complexTourRequestDTO);
            }
        }
        
    }
}
