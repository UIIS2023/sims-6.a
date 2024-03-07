using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using Microsoft.TeamFoundation.Build.WebApi;
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

namespace InitialProject.Presentation.WPF.View.Guest2
{
    /// <summary>
    /// Interaction logic for TourStatistic.xaml
    /// </summary>
    public partial class TourStatistic : Window, INotifyPropertyChanged
    {
        private readonly ITourRequestService _tourRequestService;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int UserId { get; set; }
        public string AverageGuestNum{ get; set; }
        public string AcceptedTours { get; set; }
        public ObservableCollection<string> Years { get; set; }
        public string SelectedYear { get; set; }
        
        public TourStatistic(int userId)
        {
            InitializeComponent();
            DataContext = this;
            UserId = userId;
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
            SelectedYear = "Sve godine";
            GetAllData(SelectedYear);
            Years = new ObservableCollection<string>();
            InitializeYears();
            GetLocationStatistic(SelectedYear);
            AverageGuestNum = AverageGuestNumber(SelectedYear).ToString("0.##");
            AcceptedTours = AcceptedToursPercent(SelectedYear).ToString("0.##") + " %";
            var fill = new SolidColorBrush(Color.FromRgb(0, 108, 109));
            Textbox.Foreground = fill;
            Textbox2.Foreground = fill;



        }

       private void InitializeYears()
        {
            Years.Add("Sve godine");
            foreach (TourRequest t in _tourRequestService.GetAllTourRequests(UserId).DistinctBy(t => t.StartingDate.Year))
            {
               Years.Add(t.StartingDate.Year.ToString());
            }
            Years.OrderBy(i => i);
            
        }

        private void GetAllData(string selectedYear)
        {
            List<Language> languages = new List<Language>();
            List<int> vrijednosti = new List<int>();
            
            if (selectedYear=="Sve godine")
            {
                foreach (TourRequest t in _tourRequestService.GetAllTourRequests(UserId).DistinctBy(t => t.Language.Name))
                {
                    vrijednosti.Add(_tourRequestService.GetAllTourRequests(UserId).Count(tour => tour.Language.Name == t.Language.Name));
                    languages.Add(t.Language);
                }
            } 
            else
            {
                int year = Int32.Parse(selectedYear);
                foreach (TourRequest t in _tourRequestService.GetAllTourRequests(UserId).Where(t => t.StartingDate.Year==year).DistinctBy(t => t.Language.Name))
                {
                    vrijednosti.Add(_tourRequestService.GetAllTourRequests(UserId).Where(t => t.StartingDate.Year == year).Count(tour => tour.Language.Name == t.Language.Name));
                    languages.Add(t.Language);
                }

            }
           

            double mjera = 280 / vrijednosti.Max();
            double sirina = 0.5 * 700 / languages.Count();
            double razmak = 0.48 * 700 / (languages.Count()-1);

            for (int i=0; i < languages.Count; i++)
            {
                Rectangle rectangle1 = new Rectangle();
                rectangle1.Height = vrijednosti[i] * mjera;
                rectangle1.Width = sirina;
                rectangle1.Fill = new SolidColorBrush(Color.FromRgb(187,14,26));
               
                Canvas.SetLeft(rectangle1,i * sirina + i * razmak);
                Canvas.SetBottom(rectangle1,40);


                Label label = new Label();
                label.Content = languages[i].Name;
                Canvas.SetLeft(label, i * (sirina + razmak));
                Canvas.SetBottom(label, 0);
                Mreza.Children.Add(rectangle1);
                Mreza.Children.Add(label);
            }
                

            
        }

        private void GetLocationStatistic(string selectedYear)
        {
            List<Location> locations = new List<Location>();
            List<int> vrijednosti = new List<int>();

            if (selectedYear == "Sve godine")
            {
                foreach (TourRequest t in _tourRequestService.GetAllTourRequests(UserId).DistinctBy(t => t.Location.ToString()))
                {
                    vrijednosti.Add(_tourRequestService.GetAllTourRequests(UserId).Count(tour => tour.Location.ToString() == t.Location.ToString()));
                    locations.Add(t.Location);
                }
            }
            else
            {
                int year = Int32.Parse(selectedYear);
                foreach (TourRequest t in _tourRequestService.GetAllTourRequests(UserId).Where(t => t.StartingDate.Year == year).DistinctBy(t => t.Location.ToString()))
                {
                    vrijednosti.Add(_tourRequestService.GetAllTourRequests(UserId).Where(t => t.StartingDate.Year == year).Count(tour => tour.Location.ToString() == t.Location.ToString()));
                    locations.Add(t.Location);
                }

            }


            double mjera = 270 / vrijednosti.Max();
            double sirina = 0.5 * 700 / locations.Count();
            double razmak = 0.48 * 700 / (locations.Count() - 1);

            for (int i = 0; i < locations.Count; i++)
            {
                Rectangle rectangle1 = new Rectangle();
                rectangle1.Height = vrijednosti[i] * mjera;
                rectangle1.Width = sirina;
                rectangle1.Fill = new SolidColorBrush(Color.FromRgb(0,116 , 76));
                
                
                Canvas.SetLeft(rectangle1, i * (sirina + razmak));
                Canvas.SetBottom(rectangle1, 40);
                

                Label label = new Label();
                label.Content = locations[i].City;
                Canvas.SetLeft(label, i * (sirina + razmak));
                Canvas.SetBottom(label, 0);
                Dijagram.Children.Add(rectangle1);
                Dijagram.Children.Add(label);
            }



        }

        private double AverageGuestNumber(string selectedYear)
        {
            int guestNumber = 0;
            int numberOfAcceptedTours = 0;

            if (selectedYear=="Sve godine")
            {
                foreach (TourRequest t in _tourRequestService.GetAllTourRequests(UserId).Where(t => t.RequestStatus == ComplexTourRequest.Status.Accepted))
                {
                    guestNumber += t.GuestNumber;
                    numberOfAcceptedTours++;
                }
            } 
            else
            {
                int year = Int32.Parse(selectedYear);
                foreach (TourRequest t in _tourRequestService.GetAllTourRequests(UserId).Where(t => t.RequestStatus == ComplexTourRequest.Status.Accepted && t.StartingDate.Year==year))
                {
                    guestNumber += t.GuestNumber;
                    numberOfAcceptedTours++;
                }
            }
            

            if (guestNumber != 0)
            {
                return (double)guestNumber / numberOfAcceptedTours;
            }
            else
            {
                return guestNumber;
            }
        }

        private double AcceptedToursPercent(string selectedYear)
        {
            int tourNumber = _tourRequestService.GetAllTourRequests(UserId).Count;
            int numberOfAcceptedTours = 0;

            if (selectedYear == "Sve godine")
            {
                foreach (TourRequest t in _tourRequestService.GetAllTourRequests(UserId).Where(t => t.RequestStatus == ComplexTourRequest.Status.Accepted))
                {
                    numberOfAcceptedTours++;
                }
            } 
            else
            {
                int year = Int32.Parse(selectedYear);
                foreach (TourRequest t in _tourRequestService.GetAllTourRequests(UserId).Where(t => t.RequestStatus == ComplexTourRequest.Status.Accepted && t.StartingDate.Year==year))
                {
                    numberOfAcceptedTours++;
                }
            }
            

            if (numberOfAcceptedTours != 0)
            {
                return (double)numberOfAcceptedTours / tourNumber*100;
            }
            else
            {
                return numberOfAcceptedTours;
            }
        }

        private void YearsCmbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dijagram.Children.Clear();
            Mreza.Children.Clear();
            AverageGuestNum = AverageGuestNumber(SelectedYear).ToString("0.##");
            AcceptedTours = AcceptedToursPercent(SelectedYear).ToString("0.##") + " %";
            OnPropertyChanged(nameof(AverageGuestNum));
            OnPropertyChanged(nameof(AcceptedTours));
            GetAllData(SelectedYear);
            GetLocationStatistic(SelectedYear);
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
