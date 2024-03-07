namespace InitialProject.Aplication.Contracts.Repository
{
    using InitialProject.Domen.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ITourAttendanceRepository
    {
        public List<TourAttendance> GetAll();

        public TourAttendance Save(TourAttendance tourAttendance);
        public int NextId();

        public TourAttendance Update(TourAttendance tourAttendance);
    }
}
