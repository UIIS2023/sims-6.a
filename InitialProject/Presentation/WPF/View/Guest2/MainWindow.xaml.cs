using InitialProject.Presentation.WPF.ViewModel.Guest2;
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

namespace InitialProject.Presentation.WPF.View.Guest2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindowViewModel _viewModel { get; set; }
        public MainWindow(int userId)
        {
            InitializeComponent();
            this._viewModel = new MainWindowViewModel(this.frame.NavigationService, userId);
            this.DataContext = this._viewModel;
        }
    }
}
