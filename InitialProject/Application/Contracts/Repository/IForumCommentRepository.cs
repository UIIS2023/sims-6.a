using InitialProject.Domen.CustomClasses;
using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Application.Contracts.Repository
{
    public interface IForumCommentRepository
    {
        List<ForumComment> GetAll();
        ForumComment Save(ForumComment forum);
        void Delete(ForumComment forum);
        void Update(ForumComment forum);
    }
}
