using InitialProject.Presentation.WPF.ViewModel.Guide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.Presentation.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for GuideProfileView.xaml
    /// </summary>
    public partial class GuideProfileView : Window
    {
        private readonly GuideProfileViewModel _viewModel;
        public GuideProfileView(int? GuideId)
        {
            _viewModel = new GuideProfileViewModel(GuideId);
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
