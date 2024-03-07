namespace InitialProject.Services
{
    using InitialProject.Aplication.Factory;
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Domen.Model;
    using InitialProject.Services.IServices;
    using System.Collections.Generic;

    public class LocationService : ILocationService
    {
        private readonly ILocationRepository _locationRepository;
        public LocationService()
        {
            _locationRepository = Injector.CreateInstance<ILocationRepository>();
        }

        public void Delete(Location location)
        {
            _locationRepository.Delete(location);
        }

        public List<Location> GetAll()
        {
            return _locationRepository.getAll();
        }

        public List<string> GetAllCities()
        {
            List<string> cities = new List<string>();
            List<Location> locations = _locationRepository.getAll();
            foreach(Location location in locations)
            {
                cities.Add(location.City);
            }
            return cities;
        }

        public List<string> GetAllCountries()
        {
            List<string> countries = new List<string>();
            List<Location> locations = _locationRepository.getAll();
            foreach (Location location in locations)
            {
                if (!countries.Contains(location.Country))
                {
                    countries.Add(location.Country);
                }
            }
            return countries;
        }

        public Location GetById(int id)
        {
            return _locationRepository.GetById(id);
        }

        public List<string> GetCitiesByCountry(string country)
        {
            List<string> result = new List<string>();
            List<Location> locations = GetAll();
            if (string.IsNullOrEmpty(country))
            {
                return new List<string>();
            }
            else
            {
                foreach (Location loc in locations)
                {
                    if (loc.Country == country)
                    {
                        result.Add(loc.City);
                    }
                }
                result.Insert(0, string.Empty);
            }
            return result;
        }

        public Location Save(Location locations)
        {
            return _locationRepository.Save(locations);
        }
    }
}
