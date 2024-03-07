using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;

namespace InitialProject.Services.IServices
{
    public interface IReservationService
    {
        void Save(Reservation reservation);
        void Update(Reservation reservation);
        void Delete(int reservationId);
        void DeleteLogical(int reservationId);  
        DateTime GetCheckInDate(int userId, int reservationId);
        DateTime GetCheckOutDate(int userId, int reservationId);
        Reservation GetActiveReservation(int reservationId);
        List<Reservation> GetActiveReservationsByUser(int userId);
        List<Reservation> GetUpcomingReservationsByUser(int userId);
        List<Reservation> GetFinishedReservationsByUser(int userId);
        List<Reservation> GetAllReservationsByUser(int userid);
        Reservation GetReservationById(int reservationId);
    }
}