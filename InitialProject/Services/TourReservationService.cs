namespace InitialProject.Services
{
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Repository;
    using InitialProject.Services.IServices;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TourReservationService : ITourReservationService
    {
        private readonly ITourReservationRepository _repository;
        private readonly ITourService _tourService;
        private readonly IVoucherService _voucherService;
        public TourReservationService()
        {
            _repository = Injector.CreateInstance<ITourReservationRepository>();
            _tourService = Injector.CreateInstance<ITourService>();
            _voucherService = Injector.CreateInstance<IVoucherService>();
        }
        public List<TourReservation> GetReservationsByUserId(int userId)
        {
            return _repository.GetAll().Where(r => r.UserId == userId).ToList();
        }
        public void MakeReservationWithVoucher(int userId, Tour tour, int numberOfGuests, Voucher voucher)
        {
            TourReservation tourReservation = new TourReservation(userId, tour.TourId, tour.StartingDateTime, tour.Duration, numberOfGuests, voucher.Id);
            _repository.Save(tourReservation);
            _tourService.ReduceMaxGuestNumber(tour.TourId, numberOfGuests);
            _voucherService.Delete(voucher);
        }

        public void MakeReservationWithoutVoucher(int userId, Tour tour, int numberOfGuests)
        {
            TourReservation tourReservation = new TourReservation(userId, tour.TourId, tour.StartingDateTime ,tour.Duration , numberOfGuests, 0);
            _repository.Save(tourReservation);
            _tourService.ReduceMaxGuestNumber(tour.TourId, numberOfGuests);
        }

        public List<Tour> GetAllReservedAndNotFinishedTour(int userId)
        {
            List<Tour> tours = new List<Tour>();
            foreach (TourReservation r in GetReservationsByUserId(userId))
            {
                foreach (Tour tour in _tourService.GetAllNotFinishedTour())
                {
                    if (r.TourId == tour.TourId)
                    {
                        tours.Add(tour);
                    }
                }
            }
            return tours;
        }

        public List<TourReservation> GetAllReservations()
        {
            return _repository.GetAll().Where(r => r.TourId > 0).ToList();
        }
    }
}
