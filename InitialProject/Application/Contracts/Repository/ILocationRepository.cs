namespace InitialProject.Aplication.Contracts.Repository
{
    using InitialProject.Domen.Model;
    using System.Collections.Generic;

    internal interface ILocationRepository
    {
        public List<Location> getAll();
        public Location Save(Location locations);
        public void Delete(Location location);
        public Location GetById(int id);
    }
}
