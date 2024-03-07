namespace InitialProject.Services
{
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Aplication.Factory;
    using InitialProject.Domen.Model;
    using InitialProject.Repository;
    using InitialProject.Services.IServices;
    using System.Collections.Generic;
    using System.Linq;

    public class TourPointService : ITourPointService
    {
        private ITourPointRepository _tourPointRepository;

        public TourPointService()
        {
            _tourPointRepository = Injector.CreateInstance<ITourPointRepository>();
        }


        public List<TourPoint> GetAllTourPoints()
        {
            return _tourPointRepository.GetAll();
        }

        public bool TourStartedAndFinished(int tourId)
        {
            return _tourPointRepository.GetAll().Where(t => t.TourId == tourId).Where(t => t.CurrentStatus == TourPoint.Status.Active || t.CurrentStatus == TourPoint.Status.NotActive).Count() == 0;
        }

        public TourPoint GetActiveTourPointOnTour(int tourId)
        {
            return _tourPointRepository.GetAll().Where(t => t.TourId == tourId).Where(t => t.CurrentStatus == TourPoint.Status.Active).FirstOrDefault();
        }

        public int FindNextId()
        {
            return _tourPointRepository.GetAll().Count() + 1;
        }

        public TourPoint AddTempTourPoint(TourPoint tempTourPoint)
        {
            return _tourPointRepository.SaveTemp(tempTourPoint);
        }

        public void SaveTourPoints(List<TourPoint> tourPoints)
        {
            foreach (TourPoint tourPoint in tourPoints)
                _tourPointRepository.Save(tourPoint);
        }
    }
}
