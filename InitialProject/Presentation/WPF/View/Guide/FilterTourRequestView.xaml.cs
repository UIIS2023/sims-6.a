using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel.Guide;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for FilterTourRequestView.xaml
    /// </summary>
    public partial class FilterTourRequestView : Window
    {
        private readonly FilterTourRequestViewModel _viewModel;
        public FilterTourRequestView(ObservableCollection<TourRequest>? _tourRequests)
        {
            _viewModel = new FilterTourRequestViewModel(_tourRequests);
            DataContext = _viewModel;
            InitializeComponent();
        }
    }
}
