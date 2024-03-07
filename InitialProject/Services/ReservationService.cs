using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _repository;
        public ReservationService()
        {
            _repository = Injector.CreateInstance<IReservationRepository>();
        }
        public List<Reservation> GetActiveReservationsByUser(int userId)
        {
            return _repository.GetAll().Where(r => r.UserId == userId && r.Status != ReservationStatus.Finished).ToList();
        }
        public List<Reservation> GetAllReservationsByUser(int userId)
        {
            return _repository.GetAll().Where(r => r.UserId == userId).ToList();
        }
        public DateTime GetCheckInDate(int userId, int reservationId)
        {
            List<Reservation> reservations = GetActiveReservationsByUser(userId);
            return reservations.Find(r => r.ReservationId == reservationId).ReservationDateRange.StartDate;
        }
        public DateTime GetCheckOutDate(int userId, int reservationId)
        {
            List<Reservation> reservations = GetActiveReservationsByUser(userId);
            return reservations.Find(r => r.ReservationId == reservationId).ReservationDateRange.EndDate;
        }
        public Reservation GetActiveReservation(int reservationId)
        {
            return _repository.GetAll().Find(r => r.ReservationId == reservationId && r.Status != ReservationStatus.Finished);
        }
      
        public void Delete(int reservationId)
        {
            Reservation reservation = GetActiveReservation(reservationId);
            _repository.Delete(reservation);
        }
        public void DeleteLogical(int reservationId)
        {
            Reservation reservation = GetActiveReservation(reservationId);
            reservation.Status = ReservationStatus.Finished;
            _repository.Update(reservation);
        }
        public List<Reservation> GetAllWithoutFinished()
        {
            return _repository.GetAll().Where(r => r.Status != ReservationStatus.Finished && r.Status != ReservationStatus.CheckedIn).ToList();
        }
        public void Save(Reservation reservation)
        {
            _repository.Save(reservation);
        }

        public List<Reservation> GetUpcomingReservationsByUser(int userId)
        {
            return _repository.GetAll().Where(r => r.UserId == userId && r.Status == ReservationStatus.Reserved).ToList();
        }

        public Reservation GetReservationById(int reservationId)
        {
            return _repository.GetAll().Find(r => r.ReservationId == reservationId);
        }

        public List<Reservation> GetFinishedReservationsByUser(int userId)
        {
            return _repository.GetAll().Where(r => r.UserId == userId && r.Status == ReservationStatus.Finished).ToList();
        }

        public void Update(Reservation reservation)
        {
            _repository.Update(reservation);
        }
    }
}
