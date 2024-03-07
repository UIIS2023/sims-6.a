using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Infrastructure.Repository
{
    public class UserReservationCounterRepository : IUserReservationCounterRepository
    {
        private const string FilePath = "../../../Infrastructure/Resources/Data/userReservationCounter.txt";

        private readonly Serializer<UserReservationCounter> _serializer;

        private List<UserReservationCounter> _users;

        public UserReservationCounterRepository()
        {
            _serializer = new Serializer<UserReservationCounter>();
            _users = new List<UserReservationCounter>();
        }
        public List<UserReservationCounter> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public UserReservationCounter Save(UserReservationCounter user)
        {
            _users = GetAll();
            _users.Add(user);
            _serializer.ToCSV(FilePath, _users);
            return user;
        }
        public void Delete(UserReservationCounter user)
        {
            _users = GetAll();
            UserReservationCounter founded = _users.Find(u => u.UserId == user.UserId);
            _users.Remove(user);
            _serializer.ToCSV(FilePath, _users);
        }
        public UserReservationCounter Update(UserReservationCounter user)
        {
            _users = GetAll();
            UserReservationCounter current = _users.Find(u => u.UserId == user.UserId);
            int index = _users.IndexOf(current);
            _users.Remove(current);
            _users.Insert(index, user);
            _serializer.ToCSV(FilePath, _users);
            return user;
        }
    }
}
