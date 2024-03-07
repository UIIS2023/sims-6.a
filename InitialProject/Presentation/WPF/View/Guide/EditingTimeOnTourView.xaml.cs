using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel.Guide;
using Microsoft.VisualBasic;
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

namespace InitialProject.Presentation.WPF.View.Guide
{
    /// <summary>
    /// Interaction logic for EditingTimeOnTourView.xaml
    /// </summary>
    public partial class EditingTimeOnTourView : Window
    {
        
        public EditingTimeOnTourViewModel viewModel { get; set; }
        public EditingTimeOnTourView(List<DateTime> startingDates, List<DateTime> availableDates)
        {
            viewModel= new EditingTimeOnTourViewModel(startingDates,availableDates);
            DataContext = viewModel;
            InitializeComponent();
        }

    }
}
