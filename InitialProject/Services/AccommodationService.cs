using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Aplication.Factory;
using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class AccommodationService : IAccommodationService
    {
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IAccommodationReservationRepository _accommodationReservationRepository;
        private List<Accommodation> _accommodations;
        private List<AccommodationReservation> _accommodationReservations;
        public AccommodationService()
        {
            _accommodationRepository = Injector.CreateInstance<IAccommodationRepository>();
            _accommodationReservationRepository = Injector.CreateInstance<IAccommodationReservationRepository>();
            _accommodations = new List<Accommodation>();
            _accommodationReservations = new List<AccommodationReservation>();  
        }
        public string GetNameById(int accommodationId)
        {
            _accommodations = _accommodationRepository.GetAll();
            return _accommodations.Find(a => a.AccommodationID == accommodationId).Name;
        }
        public string GetNameByReservationId(int reservationId)
        {
            _accommodations = _accommodationRepository.GetAll();
            int accommodationId = GetAccommodationIdByReservationId(reservationId);
            return _accommodations.Find(a => a.AccommodationID == accommodationId).Name;
        }
        public int GetOwnerIdByAccommodationId(int accommodationId)
        {
            _accommodations = _accommodationRepository.GetAll();
            var accommodation = _accommodations?.Find(a => a.AccommodationID == accommodationId);
            if (accommodation != null)
            {
                return accommodation.UserId;
            }
            else
            {
                throw new Exception($"Accommodation not found for ID: {accommodationId}");
            }
        }
        public int GetOwnerIdByReservationId(int reservationId)
        {
            int accommodationId = GetAccommodationIdByReservationId(reservationId);
            return GetOwnerIdByAccommodationId(accommodationId);
        }
        public int GetAccommodationIdByReservationId(int reservationId)
        {
            _accommodationReservations = _accommodationReservationRepository.GetAll();
            if (_accommodationReservations.Find(a => a.ReservationId == reservationId) == null) return 0;
            return _accommodationReservations.Find(a => a.ReservationId == reservationId).AccommodationId;
        }
        public Accommodation GetAccommodationByReservationId(int reservationId)
        {
            _accommodations = _accommodationRepository.GetAll();
            int accommodationId = GetAccommodationIdByReservationId(reservationId);
            return _accommodations.Find(accommodation => accommodation.AccommodationID == accommodationId);
        }
        public Accommodation GetAccommodationById(int accommodationId)
        {
            _accommodations = _accommodationRepository.GetAll();
            return _accommodations.Find(accommodation => accommodation.AccommodationID == accommodationId);
        }
        public int GetReservationIdByAccommodationId(int accommodationId)
        {
            _accommodationReservations = _accommodationReservationRepository.GetAll();
            return _accommodationReservations.Find(accommodation => accommodation.AccommodationId == accommodationId).ReservationId;
        }
        public void DeleteReservation(int reservationId)
        {
            _accommodationReservationRepository.DeleteReservation(reservationId);
        }
        public Accommodation GetAccommodationByIdAndOwnerId(int ownerId, int accommodationId)
        {
            _accommodations = _accommodationRepository.GetAll();
            return _accommodations.Find(accommodation => accommodation.UserId == ownerId && accommodation.AccommodationID == accommodationId);
        }
        public int GetAccommodationIdByAccommodationName(string accommodationName)
        {
            _accommodations = _accommodationRepository.GetAll();
            return _accommodations.Find(a => a.Name == accommodationName).AccommodationID; 
        }

        public List<Accommodation>GetAccommodationsByOwnerId(int ownerId)
        {
            _accommodations = _accommodationRepository.GetAll();
            return _accommodations.Where(a => a.UserId== ownerId).ToList();
        }

        public List<Accommodation> GetAccommodationsByGuestsAndDaysReserved(int guestNumber, int reservationDays)
        {
            _accommodations = _accommodationRepository.GetAll();
            return _accommodations.Where(a => a.MaxGuestNumber >= guestNumber && a.MinReservationDays <= reservationDays).ToList();
        }
        public List<int> GetReservationIdsByAccommodationId(int accommodationId)
        {
            _accommodationReservations = _accommodationReservationRepository.GetAll();
            List<AccommodationReservation> founded = _accommodationReservations.Where(ar => ar.AccommodationId == accommodationId).ToList();
            List<int> result = new List<int>();
            foreach(AccommodationReservation foundedReservation in founded)
            {
                result.Add(foundedReservation.ReservationId);
            }
            return result;
        }

        public void Save(AccommodationReservation accommodationReservation)
        {
            _accommodationReservationRepository.Save(accommodationReservation);
        }
        public bool HasAccommodationOnLocation(int ownerId, Location location)
        {
            List<Accommodation> accommodations = GetAccommodationsByOwnerId(ownerId);
            foreach(Accommodation accommodation in accommodations)
            {
                if(IsMatchingLocation(accommodation, location))
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsMatchingLocation(Accommodation accommodation, Location location)
        {
            return accommodation.Location.Country == location.Country && accommodation.Location.City == location.City;
        }
        
        public void AccommodationUpdate(Accommodation accommodation)
        {
            _accommodationRepository.Update(accommodation);
        }
    }
}
