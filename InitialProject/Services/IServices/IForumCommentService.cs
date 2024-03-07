using InitialProject.Domen.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IForumCommentService
    {
        List<ForumComment> GetAll();
        ForumComment Save(ForumComment forum);
        void Delete(ForumComment forum);
        void Update(ForumComment forum);
        int GetCommentsNumberByForum(int forumId);
        List<int> GetCommentsIdByForumId(int forumId);
    }
}
