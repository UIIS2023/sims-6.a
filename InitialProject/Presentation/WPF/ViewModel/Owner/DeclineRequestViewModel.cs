using InitialProject.Aplication.Factory;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Presentation.WPF.ViewModel.Owner
{
    public class DeclineRequestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private readonly IChangeReservationRequestService _requestService;
        public int RequestId { get; set; }

        private string _ownerComment;
        public string OwnerComment
        {
            get { return _ownerComment; }
            set
            {
                _ownerComment = value;
                OnPropertyChanged("Description");
            }
        }

        public RelayCommand OKCommand { get; set; }

        public DeclineRequestViewModel(int requestId)
        {
            _requestService = Injector.CreateInstance<IChangeReservationRequestService>();
            RequestId = requestId;
            OwnerComment = string.Empty;

            OKCommand = new RelayCommand(Button_Click);
        }

        private void Button_Click(object parameter)
        {
            _requestService.DeclineRequest(RequestId, OwnerComment);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
