using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class AddAccommodationService : IAddAccommodationService
    {

        private readonly IAccommodationRepository _accommodationRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly AccommodationImageRepository _accommodationImageRepository;
        

        public AddAccommodationService()
        {
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            _accommodationImageRepository = new AccommodationImageRepository();
           
        }

        public List<Location> GetAllLocations()
        {
            return _locationRepository.getAll();
        }

        public List<string> GetCities(List<Location> locations)
        {
            List<string> cities = new List<string>();
            cities.Add("");
            foreach (Location l in locations)
            {
                cities.Add(l.City);
            }
            return cities;
        }

        public List<string> GetCountries(List<Location> locations)
        {
            List<string> countries = new List<string>();
            countries.Add("");
            foreach (Location l in locations)
            {
                if (!countries.Contains(l.Country))
                {
                    countries.Add(l.Country);
                }
            }
            return countries;
        }

        public Accommodation CreateNewAccommodation(int _userId, string accommodationName, int accommodationMaxGuests, int accommodationCancelationDays, int accommodationMinDays, string country, string city, string type)
        {
            return new Accommodation
            {
                UserId = _userId,
                Name = accommodationName,
                MaxGuestNumber = accommodationMaxGuests,
                DaysBeforeCancelling = accommodationCancelationDays,
                MinReservationDays = accommodationMinDays,
                Location = new Location(country, city),
                TypeOfAccommodation = GetAccommodationType(type)
                
            };
        }

        public AccommodationType GetAccommodationType(string type)
        {
            switch (type)
            {
                case "Appartment":
                    return AccommodationType.Apartment;
                case "Shack":
                    return AccommodationType.Shack;
                default:
                    return AccommodationType.House;

            }
        }

        public void SaveAccommodation(Accommodation accommodation)
        {
            _accommodationRepository.Save(accommodation);
        }

        public void SaveAccommodationImages(List<AccommodationImage> images)
        {
            foreach (var image in images)
            {
                _accommodationImageRepository.Save(image, _accommodationRepository.GetLastAccommodationId());
            }
        }
    }
}
