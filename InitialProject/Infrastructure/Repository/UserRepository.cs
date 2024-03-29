﻿using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string FilePath = "../../../Infrastructure/Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;

        private List<User> _users;

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
        }
        public User Save(User user)
        {
            user.Id = NextId();
            _users.Add(user);
            _serializer.ToCSV(FilePath, _users);
            return user;
        }
        public User GetByUsername(string username)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Username == username);
        }
        public int NextId()
        {
            _users = _serializer.FromCSV(FilePath);
            if (_users.Count < 1)
            {
                return 1;
            }
            return _users.Max(acm => acm.Id) + 1;
        }
        public User GetById(int id)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetAllUsers()
        {
            _users = _serializer.FromCSV(FilePath);
            return _users;
        }
        public User Update(User user)
        {
            _users = _serializer.FromCSV(FilePath);
            User current = _users.Find(u => u.Id == user.Id);
            int index = _users.IndexOf(current);
            _users.Remove(current);
            _users.Insert(index, user);
            _serializer.ToCSV(FilePath, _users);
            return user;
        }
    }
}
