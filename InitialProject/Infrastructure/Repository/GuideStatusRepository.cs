using InitialProject.Domen.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Infrastructure.Repository
{
    public class GuideStatusRepository : IGuideStatusRepository
    {
        private const string FilePath = "../../../Infrastructure/Resources/Data/guidestatus.txt";

        private readonly Serializer<GuideStatus> _serializer;

        private List<GuideStatus> _statuses;


        public GuideStatusRepository()
        {
            _serializer = new Serializer<GuideStatus>();
            _statuses = new List<GuideStatus>();
        }

        public List<GuideStatus> getAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public GuideStatus Save(GuideStatus status)
        {
            status.Id = NextId();
            _statuses = _serializer.FromCSV(FilePath);
            _statuses.Add(status);
            _serializer.ToCSV(FilePath, _statuses);
            return status;
        }

        public void Delete(GuideStatus status)
        {
            _statuses = _serializer.FromCSV(FilePath);
            GuideStatus foundedStatus = _statuses.Find(t => t.Id == status.Id);
            _statuses.Remove(foundedStatus);
            _serializer.ToCSV(FilePath, _statuses);
        }

        public GuideStatus GetById(int id)
        {
            _statuses = _serializer.FromCSV(FilePath);
            GuideStatus foundedStatus = _statuses.Find(t => t.Id == id);
            return foundedStatus;
        }

        public int NextId()
        {
            _statuses = _serializer.FromCSV(FilePath);
            if (_statuses.Count < 1)
            {
                return 1;
            }
            return _statuses.Max(t => t.Id) + 1;
        }
        public GuideStatus Update(GuideStatus status)
        {
            _statuses = _serializer.FromCSV(FilePath);
            GuideStatus current = _statuses.Find(t => t.Id == status.Id);
            int index = _statuses.IndexOf(current);
            _statuses.Remove(current);
            _statuses.Insert(index, status);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _statuses);
            return status;
        }
    }
}
