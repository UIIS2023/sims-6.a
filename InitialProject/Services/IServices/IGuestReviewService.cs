using InitialProject.CustomClasses;
using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IGuestReviewService 
    {
        List<UserToReview> GetUsersByID(int ownerId);
        void DeleteByIdAndDate(int id, DateTime date);
        void InitializeUsersToReview();
        bool CheckIfLeftReservation(Reservation reservation);
        int ReservationAccommodationId(Reservation reservation);
        int OwnerReservationId(int accommodationId);
        void RateNotification(int ownerId);
        void RateUser(int userID, int accommodationId, DateTime date);
        bool CheckDateRange(DateTime date);
    }
}
