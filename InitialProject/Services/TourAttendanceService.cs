using InitialProject.Aplication.Factory;
using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Repository;
using InitialProject.Services.IServices;
using System.Collections.Generic;
using System.Linq;
using InitialProject.Domen.Model;

namespace InitialProject.Services
{
    public class TourAttendanceService : ITourAttendanceService
    {
        private readonly ITourAttendanceRepository _tourAttendanceRepository;
        public TourAttendanceService()
        {
            _tourAttendanceRepository = Injector.CreateInstance<ITourAttendanceRepository>();
        }

        public List<TourAttendance> GetAll()
        {
            return _tourAttendanceRepository.GetAll();
        }

        public void ConfirmTourAttendance(TourAttendance tourAttendance)
        {
            tourAttendance.UserAttended = TourAttendance.AttendanceStatus.Present;
            tourAttendance.CanGiveReview = true;
            _tourAttendanceRepository.Update(tourAttendance);
        }

        public void RejectTourAttendance(TourAttendance tourAttendance)
        {
            tourAttendance.UserAttended = TourAttendance.AttendanceStatus.NotPresent;
            _tourAttendanceRepository.Update(tourAttendance);
        }

        public bool CheckPossibleComment(int userId, int tourId)
        {
            if (_tourAttendanceRepository.GetAll().FindAll(t => t.UserId == userId && t.TourId == tourId && t.CanGiveReview == true).Count != 0)
            {
                return true;
            }
            return false;
        }

        public int GetTourPointIdWhereUserActive(int userId, int tourId)
        {
            return (_tourAttendanceRepository.GetAll().FindAll(t => t.UserId == userId && t.TourId == tourId && t.UserAttended == TourAttendance.AttendanceStatus.Present).FirstOrDefault().TourPointId);
        }

        public void AddedComment(int userId, int tourId)
        {
            foreach (TourAttendance tourAttendance in _tourAttendanceRepository.GetAll().FindAll(t => t.UserId == userId && t.TourId == tourId && t.CanGiveReview == true))
            {
                tourAttendance.CanGiveReview = false;
                _tourAttendanceRepository.Update(tourAttendance);
            }

        }

        public List<TourAttendance> GetAllPresented(int userId)
        {
            return _tourAttendanceRepository.GetAll().Where(tour => tour.UserId == userId && tour.UserAttended == TourAttendance.AttendanceStatus.Present).ToList();
        }

        public List<TourAttendance> GetAllToCheckByUser(int userId)
        {
            return _tourAttendanceRepository.GetAll().Where(tour => tour.UserId == userId && tour.UserAttended == TourAttendance.AttendanceStatus.OnHold).ToList();
        }

    }
}
