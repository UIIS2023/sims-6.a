namespace InitialProject.Presentation.WPF.ViewModel.Guest2
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Services.IServices;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TourReservationViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public int CommandParameter { get; set; }

        public RelayCommand ReserveCommand { get; set; }
        public RelayCommand DeclineCommand { get; set; }
        public Tour Tour { get; set; }
        public int UserId { get; set; }
        private ITourReservationService _tourReservationService;
        private IVoucherService _voucherService;
        public ObservableCollection<Voucher> Vouchers { get; set; }
        public ObservableCollection<string> VouchersString { get; set; }
        public string SelectedVoucher { get; set; }
        public string First { get; set; }
        public string Second { get; set; }
        public string Third { get; set; }

        public int NumberOfGuests { get; set; }
        private string _strNumberOfGuests;

        public string StrNumberOfGuests
        {
            get => _strNumberOfGuests;
            set
            {
                if (value != _strNumberOfGuests)
                {
                    try
                    {
                        int _numberOfGuests;
                        int.TryParse(value, out _numberOfGuests);
                        NumberOfGuests = _numberOfGuests;
                    }
                    catch (Exception) { }
                    _strNumberOfGuests = value;
                }
            }
        }

        public TourReservationViewModel(int userId, Tour tour)
        {
            ReserveCommand = new RelayCommand(Reserve);
            DeclineCommand = new RelayCommand(Decline);
            Tour = tour;
            UserId = userId;
            this.NumberOfGuests = NumberOfGuests;
            _tourReservationService = Injector.CreateInstance<ITourReservationService>();
            _voucherService = Injector.CreateInstance<IVoucherService>();
            VouchersString = new ObservableCollection<string>();
            Vouchers = new ObservableCollection<Voucher>(_voucherService.GetAllForUser(userId));
            InitializeVouchers();
            First = "Izabrali ste turu u " + Tour.Location.City + " koja se izvodi " + Tour.StartingDateTime.ToShortDateString() + " u " + Tour.StartingDateTime.TimeOfDay.ToString() + ".";
            Second = "Tura se izvodi na " + Tour.Language + " jeziku i traje " + Tour.Duration + "h.";
            Third = "Preostali broj slobodnih mjesta je " + Tour.MaxGuestNumber + ".";
        }


        public void Reserve(object parameter)
        {
            CommandParameter = Int32.Parse(parameter.ToString());

            if (NumberOfGuests > Tour.MaxGuestNumber)
            {
                ImpossibleMakeReservation();
            }
            else
            {
                MakeReservation(CommandParameter);
            }
        }

        public void Decline(object parameter)
        {
            CloseWindow();
        }

        private void ImpossibleMakeReservation()
        {
            string messageBoxText = "Nema dovoljno mjesta. Broj preostalih mjesta je " + Tour.MaxGuestNumber;
            string caption = "Rezervacija ture";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            if (result == MessageBoxResult.No)
            {
                CloseWindow();
            }
        }

        private void MakeReservation(int voucher)
        {
            if (voucher != 0)
            {
                _tourReservationService.MakeReservationWithVoucher(UserId, Tour, NumberOfGuests, Vouchers[voucher - 1]);
            }
            else
            {
                _tourReservationService.MakeReservationWithoutVoucher(UserId, Tour, NumberOfGuests);
            }
            MessageBox.Show("Rezervacija uspjesna");
            CloseWindow();
        }

        private void InitializeVouchers()
        {
            VouchersString.Add("Bez vaučera");
            foreach (Voucher v in Vouchers)
            {
                VouchersString.Add(v.Name + ", vrijedi do " + v.ValidUntil.ToShortDateString());
            }
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {
            CloseWindow();
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private void CloseWindow()
        {
            App.Current.MainWindow = App.Current.Windows.OfType<WPF.View.Guest2.TourReservation>().FirstOrDefault();
            App.Current.MainWindow.Close();
        }
    }
}

