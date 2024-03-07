using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel.Owner;
using InitialProject.Repository;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for GuestReviewForm.xaml
    /// </summary>
    public partial class GuestReviewForm : Window
    {
        private GuestReviewFormViewModel viewModel;

        public bool IsReviewd
        {
            get { return viewModel.IsReviewd; }
            set { viewModel.IsReviewd = value; }
        }
        public GuestReviewForm(int guestId,int accommodationId)
        {
            InitializeComponent();
            viewModel = new GuestReviewFormViewModel(guestId, accommodationId);
            DataContext= viewModel;
            
        }
        
    }
}
