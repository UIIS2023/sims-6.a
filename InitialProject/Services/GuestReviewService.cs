using InitialProject.Aplication.Contracts.Repository;
using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.Services
{
    public class GuestReviewService :IGuestReviewService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IAccommodationRepository _accommodationRepository;
        private readonly IAccommodationReservationRepository _accommodationReservationRepository;
        private readonly UserToReviewRepository _userToReviewRepository;


        public GuestReviewService()
        {
            _accommodationRepository = new AccommodationRepository();
            _reservationRepository = new ReservationRepository();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _userToReviewRepository = new UserToReviewRepository();

        }

        public List<UserToReview> GetUsersByID(int ownerId)
        {
            return _userToReviewRepository.GetAll().Where(u => u.OwnerId == ownerId).ToList();
        }
        public void DeleteByIdAndDate(int id, DateTime date)
        {
            _userToReviewRepository.DeleteByIdAndDate(id, date);
        }
        public void InitializeUsersToReview()
        {
            foreach (Reservation reservation in _reservationRepository.GetAll())
            {
                if (CheckIfLeftReservation(reservation))
                {
                    int accommodation_id = ReservationAccommodationId(reservation);
                    int owner_id = OwnerReservationId(accommodation_id);
                    UserToReview userToReview = new UserToReview(owner_id, accommodation_id, reservation.UserId, reservation.ReservationDateRange.EndDate);
                    _userToReviewRepository.Save(userToReview);
                    _reservationRepository.Delete(reservation);
                    _accommodationReservationRepository.DeleteReservation(reservation.ReservationId);
                    //users.Add(userToReview);

                }
            }
        }

        public bool CheckIfLeftReservation(Reservation reservation)
        {
            /* if (reservation.ReservationDateRange.EndDate < DateTime.Now)
             {
                 return true;
             }
             return false;
            */
            return reservation.ReservationDateRange.EndDate < DateTime.Now; 
        }

        public int ReservationAccommodationId(Reservation reservation)
        {
            foreach (AccommodationReservation accommodationReservation in _accommodationReservationRepository.GetAll())
            {
                if (accommodationReservation.ReservationId == reservation.ReservationId)
                {
                    return accommodationReservation.AccommodationId;
                }
            }
            return -1;
        }

        public int OwnerReservationId(int accommodationId)
        {
            foreach (Accommodation accommodation in _accommodationRepository.GetAll())
            {
                if (accommodation.AccommodationID == accommodationId)
                {
                    return accommodation.UserId;
                }
            }
            return -1;
        }

        public void RateNotification(int ownerId)
        {
            foreach (UserToReview userToReview in _userToReviewRepository.GetByOwnerId(ownerId)) 
            {
                if (CheckDateRange(userToReview.LeavingDay)) //&& userToReview.OwnerId == ownerId)
                {
                    RateUser(userToReview.Guest1Id, userToReview.AccommodationId, userToReview.LeavingDay);
                }
                else
                {
                    _userToReviewRepository.DeleteByIdAndDate(userToReview.Guest1Id, userToReview.LeavingDay);
                }
            }
        }

        public void RateUser(int userID, int accommodationId, DateTime date)
        {
            MessageBoxResult dialogResult = MessageBox.Show("Rate User", "You can still rate user", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                GuestReviewForm reviewForm = new GuestReviewForm(userID, accommodationId);
                reviewForm.ShowDialog();
                if (reviewForm.IsReviewd)
                {
                    _userToReviewRepository.DeleteByIdAndDate(userID, date);
                }
            }
        }

        public bool CheckDateRange(DateTime date)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = DateTime.Now.AddDays(-5);
            if (startDate >= date && endDate <= date)
            {
                return true;
            }
            return false;
        }

    }
}
