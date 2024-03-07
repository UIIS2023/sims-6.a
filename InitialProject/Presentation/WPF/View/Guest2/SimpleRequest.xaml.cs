namespace InitialProject.Presentation.WPF.View.Guest2
{
    using Eco.ViewModel.Runtime;
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Presentation.WPF.ViewModel.Guest2;
    using InitialProject.Services.IServices;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for SimpleRequest.xaml
    /// </summary>
    public partial class SimpleRequest : Window
    {
        private SimpleRequestViewModel _viewModel;

        public SimpleRequest(int userId)
        {
            _viewModel = new SimpleRequestViewModel(userId);
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void DatePickerStart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePickerEnd.SelectedDate = DatePickerStart.SelectedDate;
            DatePickerEnd.DisplayDateStart = DatePickerStart.SelectedDate;
        }
    }
}
