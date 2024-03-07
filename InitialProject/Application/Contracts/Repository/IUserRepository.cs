using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Aplication.Contracts.Repository
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        User GetById(int id);
        User GetByUsername(string username);
        int NextId();
        User Save(User user);
        User Update(User user);
    }
}