using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.CustomClasses;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Infrastructure.Repository
{
    public class ForumCommentRepository : IForumCommentRepository
    {
        private const string FilePath = "../../../Infrastructure/Resources/Data/forumComments.txt";
        private readonly Serializer<ForumComment> _serializer;
        private List<ForumComment> _forumComments;

        public ForumCommentRepository()
        {
            _serializer = new Serializer<ForumComment>();
            _forumComments = new List<ForumComment>();
        }

        public List<ForumComment> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public ForumComment Save(ForumComment forumComment)
        {
            _forumComments = GetAll();
            _forumComments.Add(forumComment);
            _serializer.ToCSV(FilePath, _forumComments);
            return forumComment;
        }
        public void Delete(ForumComment forumComment)
        {
            _forumComments = GetAll();
            ForumComment foundedComment = _forumComments.Find(fc => fc.CommentId == forumComment.CommentId);
            _forumComments.Remove(foundedComment);
            _serializer.ToCSV(FilePath, _forumComments);
        }

        public void Update(ForumComment forumComment)
        {
            _forumComments = GetAll();
            ForumComment existingComment = _forumComments.Find(fc => fc.CommentId == forumComment.CommentId);
            if (existingComment != null)
            {
                existingComment.ForumId = forumComment.ForumId;
                existingComment.CommentId = forumComment.CommentId;
                _serializer.ToCSV(FilePath, _forumComments);
            }
        }
    }

}
