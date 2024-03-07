using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;

namespace InitialProject.Services.IServices
{
    public interface ITourReservationService
    {
        List<TourReservation> GetAllReservations();
        List<Tour> GetAllReservedAndNotFinishedTour(int userId);
        List<TourReservation> GetReservationsByUserId(int userId);
        void MakeReservationWithoutVoucher(int userId,Tour tour, int numberOfGuests);
        void MakeReservationWithVoucher(int userId,Tour tour, int numberOfGuests, Voucher voucher);
    }
}