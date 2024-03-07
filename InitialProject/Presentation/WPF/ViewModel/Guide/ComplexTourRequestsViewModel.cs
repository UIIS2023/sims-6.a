using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guide;
using InitialProject.Services.IServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class ComplexTourRequestsViewModel : INotifyPropertyChanged
    {
        private readonly IComplexTourRequestService complexTourRequestService;
        public ObservableCollection<ComplexTourRequest> ComplexTourRequests { get; set; }
        public ObservableCollection<ComplexTourRequest> TourOfComplexRequests { get; set; }

        public ICommand InspectCommand { get; set; }

        public ICommand AcceptCommand { get; set; }

        public ICommand BackToComplexToursCommand { get; set; }

        private readonly int GuideId;

        private readonly ITourService tourService;
        public ComplexTourRequestsViewModel(int? guideId)
        {
            GuideId = (int)guideId;
            complexTourRequestService = Injector.CreateInstance<IComplexTourRequestService>();
            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>(complexTourRequestService.GetAllUniqueTourRequests());
            TourOfComplexRequests = new ObservableCollection<ComplexTourRequest>();
            InspectCommand = new RelayCommand(ShowTours);
            BackToComplexToursCommand = new RelayCommand(HideTours);
            AcceptCommand = new RelayCommand(AcceptTourPart);
            TourOfComplexRequestsVisibility = Visibility.Collapsed;
            tourService = Injector.CreateInstance<ITourService>();

        }

        public void HideTours(object parametar)
        {
            TourOfComplexRequestsVisibility = Visibility.Collapsed;
            ComplexTourRequestsVisibility = Visibility.Visible;
            TourOfComplexRequests.Clear();
            List<ComplexTourRequest> tours = new List<ComplexTourRequest>(complexTourRequestService.GetAllUniqueTourRequests());
            foreach (var tour in tours)
            {
                ComplexTourRequests.Add(tour);
            }
        }
        public void ShowTours(object parameter)
        {
            ComplexTourRequestsVisibility = Visibility.Collapsed;
            TourOfComplexRequestsVisibility = Visibility.Visible;
            ComplexTourRequest complexTourRequest = parameter as ComplexTourRequest;
            ComplexTourRequests.Clear();
            List<ComplexTourRequest> tours = new List<ComplexTourRequest>(complexTourRequestService.GetTourRequest(complexTourRequest.TourId).Where(c=>c.RequestStatus == ComplexTourRequest.Status.OnHold));
            foreach (var tour in tours)
            {
                TourOfComplexRequests.Add(tour);
            }
            
        }

        public void AcceptTourPart(object parameter)
        {
            ComplexTourRequest complexTourRequest = parameter as ComplexTourRequest;
            TourRequest tourRequest = new TourRequest()
            {
                GuideId = GuideId,
                StartingDate = complexTourRequest.StartingDate,
                EndingDate = complexTourRequest.EndingDate,
                RequestStatus = ComplexTourRequest.Status.Accepted,
                Location = complexTourRequest.Location,
                Language = complexTourRequest.Language,
                Description= complexTourRequest.Description,
                Id = complexTourRequest.Id
            };
            //pre toga sve mora da proveri da li uopste ima vremena da napravi tu turu
            if(tourService.GetAvailableDates(complexTourRequest.StartingDate, complexTourRequest.EndingDate).Count == 0)
            {
                MessageBox.Show("There is no available date for this tour");
                return;
            }

            CreationType creationType = new();

            creationType.Type = CreationType.CreationTourType.CreatedByComplexRequest;

            CreatingTourView creatingTourView = new CreatingTourView(tourRequest, creationType, complexTourRequest.TourId, GuideId);
            creatingTourView.ShowDialog();
            TourOfComplexRequests.Remove(complexTourRequest);
            //mora da napravi turu na osnovu ovog complexTourRequest-a i da postavi CreationType By ComplexRequest
            //mora da se zna koji guide je prihvatio
            //mora da promeni status complexTourRequest-a na Accepted i da upise svoj id u GuideId
            // i treba da promeni starting date na osnovu toga kada je on setovao da krene


        }

        private Visibility _ComplexTourRequestsVisibility = Visibility.Visible;
        public Visibility ComplexTourRequestsVisibility
        {
            get => _ComplexTourRequestsVisibility;
            set
            {
                _ComplexTourRequestsVisibility = value;
                OnPropertyChanged(nameof(ComplexTourRequestsVisibility));
            }
        }

        private Visibility _TourOfComplexRequestsVisibility = Visibility.Visible;
        public Visibility TourOfComplexRequestsVisibility
        {
            get => _TourOfComplexRequestsVisibility;
            set
            {
                _TourOfComplexRequestsVisibility = value;
                OnPropertyChanged(nameof(TourOfComplexRequestsVisibility));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
