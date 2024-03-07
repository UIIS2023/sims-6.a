using InitialProject.Repository;
using InitialProject.Services;
using InitialProject.Services.IServices;
using System.Collections.Generic;
using System;
using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Infrastructure.Repository;
using InitialProject.Application.Contracts.Repository;

namespace InitialProject.Aplication.Factory
{
    public static class Injector
    {
        private static readonly Dictionary<Type, Lazy<object>> _implementations = new Dictionary<Type, Lazy<object>>
        {
            { typeof(ICancelTourService), new Lazy<object>(() => new CancelTourService()) },
            { typeof(ITourRepository), new Lazy<object>(() => new TourRepository()) },
            { typeof(IReservationRepository), new Lazy<object>(() => new ReservationRepository()) },
            { typeof(IVoucherRepository), new Lazy<object>(() => new VoucherRepository()) },
            { typeof(ITourStatisticsService), new Lazy<object>(() => new TourStatisticsService()) },
            { typeof(ITourService), new Lazy<object>(() => new TourService()) },
            { typeof(IUserRepository), new Lazy<object>(() => new UserRepository()) },
            { typeof(IReservationService), new Lazy<object>(() => new ReservationService()) },
            { typeof(ITourReservationService), new Lazy<object>(() => new TourReservationService()) },
            { typeof(ITourReservationRepository), new Lazy<object>(() => new TourReservationRepository()) },
            { typeof(ITourPointRepository), new Lazy<object>(() => new TourPointRepository()) },
            { typeof(ITourRateService), new Lazy<object>(() => new TourRateService()) },
            { typeof(IUserService), new Lazy<object>(() => new UserService()) },
            { typeof(ITourAttendanceService), new Lazy<object>(() => new TourAttendanceService()) },
            { typeof(ITourAttendanceRepository), new Lazy<object>(() => new TourAttendanceRepository()) },
            { typeof(ITourPointService), new Lazy<object>(() => new TourPointService()) },
            { typeof(IVoucherService), new Lazy<object>(() => new VoucherService()) },
            { typeof(ILocationService), new Lazy<object>(() => new LocationService()) },
            { typeof(ILocationRepository), new Lazy<object>(() => new LocationRepository()) },
            { typeof(ITourRequestRepository), new Lazy<object>(() => new TourRequestRepository()) },
            { typeof(ITourRequestService), new Lazy<object>(() => new TourRequestService()) },
            { typeof(IOwnerToRateService), new Lazy<object>(() => new OwnerToRateService()) },
            { typeof(IAccommodationReservationService), new Lazy<object>(() => new AccommodationReservationService()) },
            { typeof(IAccommodationService), new Lazy<object>(() => new AccommodationService()) },
            { typeof(IUserReservationCounterService), new Lazy<object>(() => new UserReservationCounterService()) },
            { typeof(ILanguageRepository), new Lazy<object>(() => new LanguageRepository()) },
            { typeof(ILanguageService), new Lazy<object>(() => new LanguageService()) },
            { typeof(IOwnerRateService), new Lazy<object>(() => new OwnerRateService()) },
            { typeof(INotificationService), new Lazy<object>(() => new NotificationService()) },
            { typeof(IChangeReservationRequestService), new Lazy<object>(() => new ChangeReservationRequestService()) },
            { typeof(IRenovationRecommendationService), new Lazy<object>(() => new RenovationRecommendationService()) },
            { typeof(IReservationCompletionService), new Lazy<object>(() => new ReservationCompletionService()) },
            { typeof(IAccommodationReservationRepository), new Lazy<object>(() => new AccommodationReservationRepository()) },
            { typeof(IAccommodationRepository), new Lazy<object>(() => new AccommodationRepository()) },
            { typeof(INotificationRepository), new Lazy<object>(() => new NotificationRespository()) },
            { typeof(IOwnerRateRepository), new Lazy<object>(() => new OwnerRateRepository()) },
            { typeof(IOwnerToRateRepository), new Lazy<object>(() => new OwnerToRateRepository()) },
            { typeof(IRenovationRecommendationRepository), new Lazy<object>(() => new RenovationRecommendationRepository()) },
            { typeof(IUserReservationCounterRepository), new Lazy<object>(() => new UserReservationCounterRepository()) },
            { typeof(IForumRepository), new Lazy<object>(() => new ForumRepository()) },
            { typeof(IForumCommentRepository), new Lazy<object>(() => new ForumCommentRepository()) },
            { typeof(IForumService), new Lazy<object>(() => new ForumService()) },
            { typeof(IForumCommentService), new Lazy<object>(() => new ForumCommentService()) },
            { typeof(ICommentRepository), new Lazy<object>(() => new CommentRepository()) },
            { typeof(ICommentService), new Lazy<object>(() => new CommentService()) },
            { typeof(IForumIdService), new Lazy<object>(() => new ForumIdService()) },
            { typeof(IForumUtilityService), new Lazy<object>(() => new ForumUtilityService()) },
            { typeof(IAddAccommodationService), new Lazy<object>(() => new AddAccommodationService()) },
            { typeof(IGuestReviewService), new Lazy<object>(() => new GuestReviewService()) },
            { typeof(IRenovationService), new Lazy<object>(() => new RenovationService()) },
            { typeof(IComplexTourRequestService), new Lazy<object>(() => new ComplexTourRequestService()) },
            { typeof(IComplexTourRequestRepository), new Lazy<object>(() => new ComplexTourRequestRepository()) },            
            { typeof(ITourRateRepository), new Lazy<object>(() => new TourRateRepository()) },            
            { typeof(IReservationDTOService), new Lazy<object>(() => new ReservationDTOService()) },
            { typeof(ICheckingInService), new Lazy<object>(() => new CheckingInService()) },
            { typeof(IGuideStatusRepository), new Lazy<object>(() => new GuideStatusRepository()) },
            { typeof(IGuideStatusService), new Lazy<object>(() => new GuideStatusService()) },            
            { typeof(ITourNotificationRepository), new Lazy<object>(() => new TourNotificationRepository()) },
            { typeof(ITourNotificationService), new Lazy<object>(() => new TourNotificationService()) },


        };

        public static T CreateInstance<T>()
        {
            Type type = typeof(T);

            if (_implementations.ContainsKey(type))
            {
                return (T)_implementations[type].Value;
            }

            throw new ArgumentException($"No implementation found for type {type.FullName}");
        }
    }
}
