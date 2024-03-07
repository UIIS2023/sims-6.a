using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class ForumUtilityService : IForumUtilityService
    {
        private readonly IForumCommentService _forumCommentService;
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;
        private readonly IAccommodationReservationService _accommodationReservationService;
        private readonly IAccommodationService _accommodationService;

        public ForumUtilityService()
        {
            _forumCommentService = Injector.CreateInstance<IForumCommentService>();
            _commentService = Injector.CreateInstance<ICommentService>();
            _userService = Injector.CreateInstance<IUserService>();
            _accommodationReservationService = Injector.CreateInstance<IAccommodationReservationService>();
            _accommodationService = Injector.CreateInstance<IAccommodationService>();
        }
        public string CheckUseful(Forum forum)
        {
            int guestCommentCounter = 0;
            int ownerCommentCounter = 0;
            List<int> commentsIds = _forumCommentService.GetCommentsIdByForumId(forum.ForumId);
            foreach (int commentId in commentsIds)
            {
                Comment comment = _commentService.GetByCommentId(commentId);
                User user = _userService.GetById(comment.UserId);
                if(WasOnLocation(user, forum))
                {
                    guestCommentCounter++;
                    
                }else if(HasAccommodationOnLocation(user, forum))
                {
                    
                    ownerCommentCounter++;
                }
            }
            if (guestCommentCounter >= 20 && ownerCommentCounter >= 10)
            {
                return "Yes";
            }
            else
            {
                return "No";
            }
        }
        private bool WasOnLocation(User user, Forum forum)
        {
            return user.TypeOfUser == UserType.Guest1 && _accommodationReservationService.WasOnLocation(user.Id, forum.Location);
        }
        private bool HasAccommodationOnLocation(User user, Forum forum)
        {
            return user.TypeOfUser == UserType.Owner && _accommodationService.HasAccommodationOnLocation(user.Id, forum.Location);
        }
    }
}
