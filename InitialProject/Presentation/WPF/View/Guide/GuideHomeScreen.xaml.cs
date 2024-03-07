using InitialProject.Presentation.WPF.ViewModel.Guide;
using InitialProject.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace InitialProject.Presentation.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for GuideHomeScreen.xaml
    /// </summary>
    public partial class GuideHomeScreen : Window
    {
        private readonly GuideHomeScreenViewModel guideHomeScreenViewModel;
        public GuideHomeScreen(int? guideId)
        {
            guideHomeScreenViewModel = new GuideHomeScreenViewModel(this, guideId);
            DataContext = guideHomeScreenViewModel;
            InitializeComponent();
        }


    }
}
