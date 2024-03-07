using InitialProject.Aplication.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Serializer;
using System.Collections.Generic;
using System.Linq;

namespace InitialProject.Repository
{
    public class TourAttendanceRepository : ITourAttendanceRepository 
    {
        private const string FilePath = "../../../Infrastructure/Resources/Data/tourattendances.txt";

        private readonly Serializer<TourAttendance> _serializer;

        private List<TourAttendance> _tourAttendances;

        public TourAttendanceRepository()
        {
            _serializer = new Serializer<TourAttendance>();
            _tourAttendances = _serializer.FromCSV(FilePath);
        }


        public List<TourAttendance> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourAttendance Save(TourAttendance tourAttendance)
        {
            tourAttendance.TourAttendanceId = NextId();
            _tourAttendances.Add(tourAttendance);
            _serializer.ToCSV(FilePath, _tourAttendances);
            return tourAttendance;
        }
        public int NextId()
        {
            _tourAttendances = _serializer.FromCSV(FilePath);
            if (_tourAttendances.Count < 1)
            {
                return 1;
            }
            return _tourAttendances.Max(t => t.TourAttendanceId) + 1;
        }

        public TourAttendance Update(TourAttendance tourAttendance)
        {
            _tourAttendances = _serializer.FromCSV(FilePath);
            TourAttendance current = _tourAttendances.Find(tour => tour.TourAttendanceId == tourAttendance.TourAttendanceId);
            int index = _tourAttendances.IndexOf(current);
            _tourAttendances.Remove(current);
            _tourAttendances.Insert(index, tourAttendance);
            _serializer.ToCSV(FilePath, _tourAttendances);
            return tourAttendance;
        }

        


        


    }
}
