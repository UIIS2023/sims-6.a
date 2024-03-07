using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System.Collections.ObjectModel;

namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    public class ComplexTourRequestsViewModel
    {
        public ObservableCollection<ComplexTourRequest> ComplexTourRequests { get; set; }
        private readonly IComplexTourRequestService _complexTourRequestService;
        public ComplexTourRequestsViewModel(int tourId)
        {
            _complexTourRequestService = Injector.CreateInstance<IComplexTourRequestService>();
            ComplexTourRequests = new ObservableCollection<ComplexTourRequest>(_complexTourRequestService.GetTourRequest(tourId));
        }
    }
}
