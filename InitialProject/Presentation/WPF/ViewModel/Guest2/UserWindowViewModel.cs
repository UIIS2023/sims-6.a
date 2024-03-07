namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    using InitialProject.Presentation.WPF.View.Guest2.Views;
    using InitialProject.View.Guest2;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Navigation;

    public class UserWindowViewModel
    {
        public int UserId { get; set; }
        public NavigationService NavService { get; set; }
        public RelayCommand NavigateToFinishedToursCommand { get; set; }
        public RelayCommand NavigateToReservedToursCommand { get; set; }
        public RelayCommand NavigateToVouchersPageCommand { get; set; }
        public RelayCommand NavigateToRequestedToursCommand { get; set; }


        private void Execute_NavigateToFinishedToursCommand(object obj)
        {
            Page finishedTours = new FinishedTours(UserId);
            this.NavService.Navigate(finishedTours);
        }
         private void Execute_NavigateToVouchersViewCommand(object obj)
        {
            Page vouchersView = new VouchersView(UserId);
            this.NavService.Navigate(vouchersView);
        }
        private void Execute_NavigateToRequestedToursCommand(object obj)
        {
            Page tourRequests = new TourRequestsView(UserId);
            this.NavService.Navigate(tourRequests);
        }

        private void Execute_NavigateToReservedToursCommand(object obj)
        {
            Page reservedTours = new ReservedTours(UserId);
            this.NavService.Navigate(reservedTours);
        }

        public UserWindowViewModel(NavigationService navService, int userId)
        {
            UserId = userId;
            this.NavigateToFinishedToursCommand = new RelayCommand(Execute_NavigateToFinishedToursCommand);
            this.NavigateToReservedToursCommand = new RelayCommand(Execute_NavigateToReservedToursCommand);
            this.NavigateToVouchersPageCommand = new RelayCommand(Execute_NavigateToVouchersViewCommand);
            this.NavigateToRequestedToursCommand = new RelayCommand(Execute_NavigateToRequestedToursCommand);
            this.NavService = navService;

        }
    }
}
