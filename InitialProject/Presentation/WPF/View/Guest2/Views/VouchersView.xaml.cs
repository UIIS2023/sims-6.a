using InitialProject.Presentation.WPF.ViewModel.Guest2;
using System.Windows.Controls;

namespace InitialProject.Presentation.WPF.View.Guest2.Views
{
    /// <summary>
    /// Interaction logic for VouchersView.xaml
    /// </summary>
    public partial class VouchersView : Page
    {
        public VouchersView(int userId)
        {
            VouchersViewModel vouchersViewModel = new VouchersViewModel(userId);
            InitializeComponent();
            DataContext = vouchersViewModel;
        }
    }
}
