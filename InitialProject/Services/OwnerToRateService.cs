using InitialProject.Aplication.Contracts.Repository;
using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class OwnerToRateService : IOwnerToRateService
    {
        private readonly IOwnerToRateRepository _ownerToRateRepository;
        private readonly IAccommodationService _accommodationService;

        public OwnerToRateService()
        {
            _ownerToRateRepository = new OwnerToRateRepository();
            _accommodationService = new AccommodationService();
        }
        public List<OwnerToRate> GetOwnersToRate()
        {
            return _ownerToRateRepository.GetAll();
        }
        public void Save(OwnerToRate ownerToRate)
        {
            _ownerToRateRepository.Save(ownerToRate);
        }
        public void DeleteIfFiveDaysPassed()
        {
            DateTime fiveDaysAgo = DateTime.Now.AddDays(-5);
            List<OwnerToRate> tempList = _ownerToRateRepository.GetAll().Where(o => o.LeavingDay < fiveDaysAgo).ToList();
            foreach(OwnerToRate o in tempList)
            {
                _ownerToRateRepository.Delete(o);
            }
        }
        public Dictionary<int, string> GetAccommodationNamesByUser(int userId)
        {
            Dictionary<int, string> accommodationNames = new Dictionary<int, string>();
            List<OwnerToRate> ownersToRate = _ownerToRateRepository.GetAll();
            foreach (OwnerToRate o in (ownersToRate))
            {
                if (o.UserId == userId)
                {
                    Accommodation accommodation = _accommodationService.GetAccommodationById(o.AccommodationId);
                    accommodationNames.Add(accommodation.AccommodationID, accommodation.Name);
                }
            }
            return accommodationNames;
        }
        public void DeleteOwnerToRate(int accommodationId)
        {
            List<OwnerToRate> ownersToRate = _ownerToRateRepository.GetAll();
            OwnerToRate founded = ownersToRate.Find(or => or.AccommodationId == accommodationId);
            _ownerToRateRepository.Delete(founded);
        }
    }
}
