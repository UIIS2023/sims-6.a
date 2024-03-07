namespace InitialProject.Aplication.Contracts.Repository
{
    using InitialProject.Domen.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITourRateRepository
    {
        public List<TourRate> GetAll();
        public TourRate Save(TourRate TourRate);

        public TourRate Update(TourRate TourRate);

    }
}
