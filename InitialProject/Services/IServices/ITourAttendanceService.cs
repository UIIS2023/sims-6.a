using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Services.IServices
{
    public interface ITourAttendanceService
    {
        void AddedComment(int userId, int tourId);
        bool CheckPossibleComment(int userId, int tourId);
        void ConfirmTourAttendance(TourAttendance tourAttendance);
        List<TourAttendance> GetAll();
        List<TourAttendance> GetAllPresented(int userId);
        List<TourAttendance> GetAllToCheckByUser(int userId);
        int GetTourPointIdWhereUserActive(int userId, int tourId);
        void RejectTourAttendance(TourAttendance tourAttendance);
    }
}