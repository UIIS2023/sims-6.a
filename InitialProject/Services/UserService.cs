using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Aplication.Factory;
using InitialProject.Domen.Model;
using InitialProject.Services.IServices;
using System.Collections.Generic;

namespace InitialProject.Services
{
    public class UserService : IUserService
    {
        private int _userId;

        private readonly IUserRepository _userRepository;
        public UserService()
        {
            _userRepository = Injector.CreateInstance<IUserRepository>();
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }
        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }
        public User GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }
        public int NextId()
        {
            return _userRepository.NextId();
        }
        public User Save(User user)
        {
            return _userRepository.Save(user);
        }
        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public void UsePoints(int userId)
        {
            var user = _userRepository.GetById(userId);
            if(user.Points > 0)
            {
                user.Points = user.Points - 1;
                Update(user);
            }
        }

        public int GetUserId()
        {
            return _userId;
        }

        public void UpdateUserId(int newUserId)
        {
            _userId = newUserId;
        }

        public string GetUsername()
        {
            User user = GetById(_userId);
            return user.Username;
        }
        public bool GetSuperGuest()
        {
            User user = GetById(_userId);
            return user.IsSuperGuest;
        }
        public int GetUserPoints()
        {
            var user = GetById(_userId);
            return user.Points;
        }
    }
}
