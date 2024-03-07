using System.Collections.Generic;
using System.Linq;
using InitialProject.Serializer;
using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Domen.Model;

namespace InitialProject.Repository
{
    public class OwnerRateRepository : IOwnerRateRepository
    {
        private const string FileParh = "../../../Infrastructure/Resources/Data/ownerRates.txt";
        private readonly Serializer<OwnerRate> _serializer;
        private List<OwnerRate> _ownerRates;
        
        public OwnerRateRepository()
        {
            _serializer = new Serializer<OwnerRate>();
            _ownerRates = new List<OwnerRate>();
        }
        public List<OwnerRate> GetAll()
        {
            return _serializer.FromCSV(FileParh);
        }
        public OwnerRate Save(OwnerRate ownerRate)
        {
            ownerRate.OwnerRateId = NextId();
            _ownerRates.Add(ownerRate);
            _serializer.ToCSV(FileParh, _ownerRates);
            return ownerRate;
        }
        public List<OwnerRate> GetAllRatesByOwner(int ownerId)
        {
            _ownerRates = GetAll();
            return _ownerRates.Where(r => r.OwnerId == ownerId).ToList();
        }
        public int NextId()
        {
            _ownerRates = GetAll();
            if (_ownerRates.Count < 1)
            {
                return 1;
            }
            return _ownerRates.Max(or => or.OwnerRateId) + 1;
        }
        public void Delete(OwnerRate ownerRate)
        {
            _ownerRates = GetAll();
            OwnerRate foundedRate = _ownerRates.Find(or => or.OwnerRateId == ownerRate.OwnerRateId);
            _ownerRates.Remove(foundedRate);
            _serializer.ToCSV(FileParh, _ownerRates);
        }
    }
}
