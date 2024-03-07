using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Aplication.Contracts.Repository
{
    public interface ITourPointRepository
    {
        void ClearTemp();
        void Delete(TourPoint tourPoint);
        void DeleteTemp(TourPoint tourPoint);
        TourPoint GetActiveTourPointOnTour(int tourId);
        List<TourPoint> GetAll();
        List<TourPoint> getAllTemp();
        List<TourPoint> GetTourPointsByTourId(int tourId);
        int NextId();
        int NextIdTemp();
        TourPoint Save(TourPoint tourPoint);
        TourPoint SaveTemp(TourPoint tourPoint);
        TourPoint Update(TourPoint tourPoint);
        TourPoint UpdateTemp(TourPoint tourPoint);
        TourPoint UpdateTempOrder(TourPoint tourPoint, int order);
    }
}