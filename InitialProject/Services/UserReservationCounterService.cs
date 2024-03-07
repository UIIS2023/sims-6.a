using InitialProject.Aplication.Factory;
using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Infrastructure.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.VisualStudio.Services.Graph.GraphResourceIds;

namespace InitialProject.Services
{
    public class UserReservationCounterService : IUserReservationCounterService
    {
        private readonly IUserReservationCounterRepository _userReservationCounterRepository;
        private readonly IUserService _userService;
        private readonly IVoucherService _voucherService;
        public UserReservationCounterService()
        {
            _userReservationCounterRepository = Injector.CreateInstance<IUserReservationCounterRepository>();
            _userService = Injector.CreateInstance<IUserService>();
            _voucherService = Injector.CreateInstance<IVoucherService>();
        }
        public void UpdateReservationCounter(int userId)
        {
            List<UserReservationCounter> users = _userReservationCounterRepository.GetAll();
            UserReservationCounter user = users.Find(u => u.UserId == userId);
            user.ReservationCount++;
            _userReservationCounterRepository.Update(user);
        }
        public void InitializeReservationCounter(int userId)
        {
            List<UserReservationCounter> users = _userReservationCounterRepository.GetAll();
            UserReservationCounter user = users.Find(u => u.UserId == userId);
            if (user == null)
            {
                UserReservationCounter newUser = new UserReservationCounter(userId, 0, new DateTime(DateTime.Now.Year, 1, 1));
                _userReservationCounterRepository.Save(newUser);
            }
            else
            {
                if (user.InitialDate.Year == DateTime.Now.Year - 1)
                {
                    CheckUserForSuperGuest(userId, user.ReservationCount);
                    ResetData(userId, users);
                }
            }
        }
        public UserReservationCounter GetReservationCounterByUserId(int userId)
        {
            List<UserReservationCounter> users = _userReservationCounterRepository.GetAll();
            return users.Find(user => user.UserId == userId);
        }
        private void CheckUserForSuperGuest(int userId, int reservationCount)
        {
            User user = _userService.GetById(userId);
            if (reservationCount >= 10)
            {
                user.Points = 5;
                user.IsSuperGuest = true;
            }
            else
            {
                user.Points = 0;
                user.IsSuperGuest = false;
            }
            _userService.Update(user);
        }
        private void ResetData(int userId, List<UserReservationCounter> users)
        {
            UserReservationCounter user = users.Find(u => u.UserId == userId);
            user.InitialDate = new DateTime(DateTime.Now.Year, 1, 1);
            user.ReservationCount = 0;
            _userReservationCounterRepository.Update(user);
        }

        private void CheckUsersForVoucher(int userId)
        {
            UserReservationCounter user = _userReservationCounterRepository.GetAll().Find(u => u.UserId == userId);
            if (user != null)
            { 
                if (user.ReservationCount >= 5)
                {
                    _voucherService.CreateVoucher(userId);
                    user.ReservationCount = user.ReservationCount-5;
                    _userReservationCounterRepository.Update(user);
                }
            }
            
        }

        public void CountTourReservations(int userId, int number)
        {  
            UserReservationCounter user = _userReservationCounterRepository.GetAll().Find(u => u.UserId == userId);
            if (user != null)
            {
                user.ReservationCount +=number;
                _userReservationCounterRepository.Update(user);
                CheckUsersForVoucher(userId);
            }
            else
            {
                UserReservationCounter newUser = new UserReservationCounter(userId, number, new DateTime(DateTime.Now.Year, 1, 1));
                _userReservationCounterRepository.Save(newUser);
            }

        }

    }

}
