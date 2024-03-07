using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Presentation.WPF.View.Guide;
using InitialProject.Services;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static InitialProject.Domen.CustomClasses.CreationType;

namespace InitialProject.Presentation.WPF.ViewModel.Guide
{
    public class CreateTourViewModel : INotifyPropertyChanged
    {
        public ICommand FillterCommand { get; set; }
        public ICommand EditDatesCommand { get; set; }
        public ICommand EditTourPointsCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand SuggestedLanguageCommand { get; set; }
        public ICommand SuggestedLocationCommand { get; set; }
        public ObservableCollection<string> Countries { get; set; }
        public ObservableCollection<string> Cities { get; set; }
        public ObservableCollection<string> Languages { get; set; }

        private readonly ILocationService _locationService;

        private readonly ILanguageService _languageService;

        private readonly ITourService _tourService;

        private readonly ITourPointService _tourPointService;

        private readonly ITourRequestService _tourRequestService;

        private readonly ITourStatisticsService _tourStatisticsService;

        private readonly IComplexTourRequestService _complexTourRequestService;

        private readonly int nextTourId;

        public List<TourPoint> tourPoints;

        public List<DateTime> tourStartingDates;

        public TourRequest? TourRequest;
        public List<DateTime> availableDates { get; set; }
        public CreationTourType? Type { get; set; }
        public int? ComplexTourId { get; set; }
        public Window window { get; set; }
        public int? GuideId { get; set; }
        public CreateTourViewModel(Window _window,TourRequest? tourRequest, CreationType? type, int? ComplexId, int? GuideId)
        {
            _locationService = Injector.CreateInstance<ILocationService>();
            _languageService = Injector.CreateInstance<ILanguageService>();
            _tourService = Injector.CreateInstance<ITourService>();
            _tourPointService = Injector.CreateInstance<ITourPointService>();
            _tourRequestService = Injector.CreateInstance<ITourRequestService>();
            _tourStatisticsService = Injector.CreateInstance<ITourStatisticsService>();
            _complexTourRequestService = Injector.CreateInstance<IComplexTourRequestService>();

            Countries = new ObservableCollection<string>(_locationService.GetAllCountries());
            Cities = new ObservableCollection<string>(_locationService.GetAllCities());
            Languages = new ObservableCollection<string>(_languageService.GetAllToString());

            EditTourPointsCommand = new RelayCommand(ivokeTourPoints);
            EditDatesCommand = new RelayCommand(inovkeDates);
            AddCommand = new RelayCommand(CreateTour);

            SuggestedLanguageCommand = new RelayCommand(SuggestedLanguage);
            SuggestedLocationCommand = new RelayCommand(SuggestedLocation);


            nextTourId = _tourService.FindNextId();
            tourPoints = new List<TourPoint>();
            tourStartingDates = new List<DateTime>();
            window = _window;

            if(ComplexId != null)
                ComplexTourId = (int)ComplexId;
            if(type != null)
                Type = type.Type;
            if(GuideId != null)
                this.GuideId = (int)GuideId;


            if (tourRequest != null)
            {
                TourRequest = tourRequest;
                SetValues(tourRequest);
            }

        }

        public void SuggestedLanguage(object ob)
        {
            Language language = _tourStatisticsService.GetMostPopularLanguage();
            Language = language.Name;
            Type = CreationTourType.CreatedByStatistics;

        }

        public void SuggestedLocation(object ob)
        {
            Location location = _tourStatisticsService.GetMostPopularLocation();
            Country = location.Country;
            City = location.City;
            Type = CreationTourType.CreatedByStatistics;
        }



        public void SetValues(TourRequest tourRequest)
        {
            City = tourRequest.Location.City;
            Country = tourRequest.Location.Country;
            Language = tourRequest.Language.Name;
            Description = tourRequest.Description;
            MaxGuests = tourRequest.GuestNumber;
            GetAvailableDates(tourRequest);
        }

        public void GetAvailableDates(TourRequest tourRequest)
        {
            availableDates = new();
            availableDates = _tourService.GetAvailableDates(tourRequest.StartingDate, tourRequest.EndingDate);
        }

        

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void FilterCities()
        {
            List<string> cities = _locationService.GetCitiesByCountry(Country);
            Cities.Clear();
            foreach (var c in cities)
            {
                Cities.Add(c);
            }
        }

        public void ivokeTourPoints(object obj)
        {
            CreatingTourPointView dsa = new CreatingTourPointView(nextTourId,tourPoints);
            dsa.Show();
        }

        public void inovkeDates(object obj)
        {
            EditingTimeOnTourView dsa = new EditingTimeOnTourView(tourStartingDates, availableDates);
            dsa.Show();
        }

        public void CreateTour(object obj)
        {
            if (Type != CreationTourType.CreatedByComplexRequest && Type != null)
                foreach (var date in tourStartingDates)
                {
                    Language language = new();
                    Tour tour = new Tour()
                    {
                        Name = Name,
                        Location = new Location { City = City, Country = Country },
                        Description = Description,
                        Language = language.fromStringToLanguage(Language),
                        MaxGuestNumber = MaxGuests,
                        StartingDateTime = date,
                        Duration = TourDuratation,
                        TourStarted = false,
                        CreatedType = (CreationTourType)Type,
                        GuideId = (int)GuideId
                    };

                    if (TourRequest != null)
                    {
                        TourRequest.RequestStatus = ComplexTourRequest.Status.Accepted;
                        _tourRequestService.Update(TourRequest);
                        _tourRequestService.MakeNotificationsForGuests(tour, TourRequest.UserId);
                    }

                    _tourService.Save(tour);
                    //_tourRequestService.MakeNotificationsForGuests(tour, TourRequest.UserId);

                    _tourPointService.SaveTourPoints(tourPoints);
                    foreach (var tourPoint in tourPoints)
                    {
                        tourPoint.TourId++;
                    }
                }
            else if(TourRequest != null)
            {
                foreach (var date in tourStartingDates)
                {
                    Language language = new();
                    Tour tour = new Tour()
                    {
                        GuideId = TourRequest.GuideId,
                        Name = Name,
                        Location = new Location { City = City, Country = Country },
                        Description = Description,
                        Language = language.fromStringToLanguage(Language),
                        MaxGuestNumber = MaxGuests,
                        StartingDateTime = date,
                        Duration = TourDuratation,
                        TourStarted = false,
                        CreatedType = (CreationTourType)Type
                    };
                    //sada je potrebno da onobovimo ComplexTourId sa tim tourid
                    //u kompleksnim postaviti na true i staviti id vodica na true

                    _tourService.Save(tour);
                    _tourPointService.SaveTourPoints(tourPoints);
                    _complexTourRequestService.UpdateSpecificRequest((int)ComplexTourId, TourRequest.Id, TourRequest.GuideId, tour.StartingDateTime);
                    foreach (var tourPoint in tourPoints)
                    {
                        tourPoint.TourId++;
                    }
                }
                window.Close();
            }
            else
            {
                foreach (var date in tourStartingDates)
                {
                    Language language = new();
                    Tour tour = new Tour()
                    {
                        Name = Name,
                        Location = new Location { City = City, Country = Country },
                        Description = Description,
                        Language = language.fromStringToLanguage(Language),
                        MaxGuestNumber = MaxGuests,
                        StartingDateTime = date,
                        Duration = TourDuratation,
                        TourStarted = false,
                        GuideId = (int)GuideId
                    };

                    if (TourRequest != null)
                    {
                        TourRequest.RequestStatus = ComplexTourRequest.Status.Accepted;
                        _tourRequestService.Update(TourRequest);
                    }

                    _tourService.Save(tour);

                    _tourPointService.SaveTourPoints(tourPoints);
                    foreach (var tourPoint in tourPoints)
                    {
                        tourPoint.TourId++;
                    }
                }

            }


        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(_Name));
            }
        }

        private string _Country;
        public string Country
        {
            get { return _Country; }
            set
            {
                _Country = value;
                OnPropertyChanged(nameof(Country));
                FilterCities();
            }
        }

        private string _City;
        public string City
        {
            get { return _City; }
            set
            {
                _City = value;
                OnPropertyChanged(nameof(City));
            }
        }

        private string _language;
        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                OnPropertyChanged(nameof(Language)); // <-- This should be the property name
            }
        }

        private int _MaxGuests;
        public int MaxGuests
        {
            get { return _MaxGuests; }
            set
            {
                _MaxGuests = value;
                OnPropertyChanged(nameof(_MaxGuests));
            }
        }

        private int _TourDuratation;

        public int TourDuratation
        {
            get { return _TourDuratation; }
            set
            {
                _TourDuratation = value;
                OnPropertyChanged(nameof(_TourDuratation));
            }
        }

        


        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                OnPropertyChanged(nameof(_Description));
            }
        }

        
        private string _imageUrl;

        public string TourImageUrl
        {
            get => _imageUrl;
            set
            {
                if (value != _imageUrl)
                {
                    _imageUrl = value;
                    OnPropertyChanged(nameof(_imageUrl));
                }
            }
        }
    }
}
