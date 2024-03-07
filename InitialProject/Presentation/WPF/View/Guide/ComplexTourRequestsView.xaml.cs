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
    /// Interaction logic for ComplexTourRequestsView.xaml
    /// </summary>
    public partial class ComplexTourRequestsView : Window
    {
        private readonly ComplexTourRequestsViewModel _complexTourRequestsViewModel;
        public ComplexTourRequestsView(int? GuideId)
        {
            _complexTourRequestsViewModel = new ComplexTourRequestsViewModel(GuideId);
            DataContext = _complexTourRequestsViewModel;
            InitializeComponent();
        }
    }
}
