namespace InitialProject.Repository
{
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Domen.Model;
    using InitialProject.Serializer;
    using System.Collections.Generic;

    public class TourRateRepository :  ITourRateRepository
    {
        private const string FilePath = "../../../Infrastructure/Resources/Data/tourRates.txt";
        private readonly Serializer<TourRate> _serializer;

        private  List<TourRate> _tourRates;
        public TourRateRepository()
        {
            _serializer = new Serializer<TourRate>();
            _tourRates = new List<TourRate>(GetAll());
        }
        public List<TourRate> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public TourRate Save(TourRate TourRate)
        {
            _tourRates.Add(TourRate);
            _serializer.ToCSV(FilePath, _tourRates);
            return TourRate;
        }

        public TourRate Update(TourRate TourRate)
        {
            _tourRates = _serializer.FromCSV(FilePath);
            TourRate current = _tourRates.Find(t => t.TourId == TourRate.TourId && t.GuestId == TourRate.GuestId);
            int index = _tourRates.IndexOf(current);
            _tourRates.Remove(current);
            _tourRates.Insert(index, TourRate);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _tourRates);
            return TourRate;
        }
    }
}
