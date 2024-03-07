using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.ViewModel.Owner;
using InitialProject.Services;
using InitialProject.Services.IServices;
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
    /// Interaction logic for AddRenovation.xaml
    /// </summary>
    public partial class AddRenovation : Window
    {
        private AddRenovationViewModel viewModel;
        public AddRenovation(Accommodation selectedAccommodation)
        {
            InitializeComponent();
            viewModel = new AddRenovationViewModel(selectedAccommodation);
            DataContext= viewModel;
        }
    }
}
