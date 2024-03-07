using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IAccommodationService
    {
        string GetNameById(int accommodationId);
        string GetNameByReservationId(int reservationId);
        int GetOwnerIdByAccommodationId(int accommodationId);
        int GetOwnerIdByReservationId(int reservationId);
        int GetAccommodationIdByReservationId(int reservationId);
        int GetAccommodationIdByAccommodationName(string accommodationName);
        Accommodation GetAccommodationByReservationId(int reservationId);
        Accommodation GetAccommodationById(int accommodationId);
        Accommodation GetAccommodationByIdAndOwnerId(int ownerId, int accommodationId);
        int GetReservationIdByAccommodationId(int accommodationId);
        void DeleteReservation(int reservationId);
        List<Accommodation> GetAccommodationsByGuestsAndDaysReserved(int guestNumber, int reservationDays);
        List<int> GetReservationIdsByAccommodationId(int accommodationId);
        void Save(AccommodationReservation accommodationReservation);
        bool HasAccommodationOnLocation(int userId, Location location);
        List<Accommodation> GetAccommodationsByOwnerId(int ownerId);
        public void AccommodationUpdate(Accommodation accommodation);

    }
}
