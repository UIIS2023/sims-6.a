using InitialProject.Aplication.Factory;
using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class ReviewInfoService
    {
        private readonly IAccommodationService accommodationService;
        private readonly IOwnerRateService ownerRateService;
        private readonly GuestReviewRepository guestReviewRepository;
        private readonly IUserService userService;

        public ReviewInfoService()
        {
            accommodationService = Injector.CreateInstance<IAccommodationService>();
            ownerRateService = Injector.CreateInstance<IOwnerRateService>();
            guestReviewRepository = new GuestReviewRepository();
            userService = Injector.CreateInstance<IUserService>();
        }
        public List<ReviewInfoDTO> GetReviewInfo(int userId)
        {
            List<ReviewInfoDTO> result = new List<ReviewInfoDTO>();
            List<OwnerRate> ownerRates = ownerRateService.GetRatesByUserId(userId);
            List<GuestReview> guestReviews = guestReviewRepository.GetByUserId(userId);
            foreach(GuestReview guestReview in guestReviews)
            {
                foreach(OwnerRate ownerRate in ownerRates)
                {
                    if(IsMatchingGuestAndOwnerReview(guestReview, ownerRate))
                    {
                        ReviewInfoDTO reviewInfo = MakeReview(ownerRate.OwnerId, ownerRate.AccommodationId, guestReview.Hygiene, guestReview.RuleFollowing, guestReview.Comment);
                        result.Add(reviewInfo);
                        break;
                    }
                }
            }
            return result;
        }
        private ReviewInfoDTO MakeReview(int ownerId, int accommodationId, int hygiene, int followingRules, string comment)
        {
            Accommodation foundedAccommodation = accommodationService.GetAccommodationByIdAndOwnerId(ownerId, accommodationId);
            User foundedUser = userService.GetById(ownerId);
            return new ReviewInfoDTO(foundedUser.Name, foundedAccommodation.Name, hygiene, followingRules, comment);
        }
        private bool IsMatchingGuestAndOwnerReview(GuestReview guestReview, OwnerRate ownerRate)
        {
            return guestReview.GuestId == ownerRate.UserId && guestReview.AccommodationId == ownerRate.AccommodationId;
        }

    }
}
