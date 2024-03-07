using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel.Owner;
using InitialProject.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace InitialProject.Presentation.WPF.View.Owner
{
    /// <summary>
    /// Interaction logic for DeclineRequest.xaml
    /// </summary>
    public partial class DeclineRequest : Window
    {
        private DeclineRequestViewModel viewModel;

        public DeclineRequest(int requestId)
        {
            InitializeComponent();
            viewModel = new DeclineRequestViewModel(requestId);
            DataContext= viewModel;
        }
    }
}
