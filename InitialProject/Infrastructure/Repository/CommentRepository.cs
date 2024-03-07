﻿using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class CommentRepository : ICommentRepository
    {

        private const string FilePath = "../../../Infrastructure/Resources/Data/comments.csv";

        private readonly Serializer<Comment> _serializer;

        private List<Comment> _comments;

        public CommentRepository()
        {
            _serializer = new Serializer<Comment>();
            _comments = _serializer.FromCSV(FilePath);
        }

        public List<Comment> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Comment Save(Comment comment)
        {
            comment.CommentId = NextId();
            _comments = _serializer.FromCSV(FilePath);
            _comments.Add(comment);
            _serializer.ToCSV(FilePath, _comments);
            return comment;
        }

        public int NextId()
        {
            _comments = _serializer.FromCSV(FilePath);
            if (_comments.Count < 1)
            {
                return 1;
            }
            return _comments.Max(c => c.CommentId) + 1;
        }

        public void Delete(Comment comment)
        {
            _comments = _serializer.FromCSV(FilePath);
            Comment founded = _comments.Find(c => c.CommentId == comment.CommentId);
            _comments.Remove(founded);
            _serializer.ToCSV(FilePath, _comments);
        }

        public Comment Update(Comment comment)
        {
            _comments = _serializer.FromCSV(FilePath);
            Comment current = _comments.Find(c => c.CommentId == comment.CommentId);
            int index = _comments.IndexOf(current);
            _comments.Remove(current);
            _comments.Insert(index, comment);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _comments);
            return comment;
        }

        public List<Comment> GetByUser(int userId)
        {
            _comments = _serializer.FromCSV(FilePath);
            return _comments.FindAll(c => c.UserId == userId);
        }
    }
}