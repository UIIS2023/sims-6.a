namespace InitialProject.Aplication.Contracts.Repository
{
    using InitialProject.Domen.Model;
    using System.Collections.Generic;

    public interface ITourImageRepository
    {
        public List<TourImages> GetAll();

        public TourImages Save(TourImages tourImages);

        public int NextId();
    }
}
