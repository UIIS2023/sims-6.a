using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface ICommentService
    {
        List<Comment> GetAll();
        Comment Save(Comment comment);
        void Delete(Comment comment);
        Comment Update(Comment comment);
        List<Comment> GetByUser(int userId);
        Comment GetByCommentId(int commentId);
    }
}
