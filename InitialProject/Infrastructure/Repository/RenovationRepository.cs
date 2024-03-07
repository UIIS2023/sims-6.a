using InitialProject.Domen.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Infrastructure.Repository
{
    public class RenovationRepository
    {
        private const string FilePath = "../../../Infrastructure/Resources/Data/renovations.txt";
        private readonly Serializer<Renovation> _serializer;
        private List<Renovation> _renovations;

        public RenovationRepository()
        {
            _serializer = new Serializer<Renovation>();
            _renovations = new List<Renovation>();
        }

        public List<Renovation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Renovation Save(Renovation renovation)
        {
            renovation.RenovationId = NextId();
            _renovations.Add(renovation);
            _serializer.ToCSV(FilePath, _renovations);
            return renovation;
        }

        public int NextId()
        {
            _renovations = _serializer.FromCSV(FilePath);
            if (_renovations.Count < 1)
            {
                return 1;
            }
            return _renovations.Max(acm => acm.RenovationId) + 1;
        }

        public void Delete(Renovation renovation)
        {
            _renovations = _serializer.FromCSV(FilePath);
            Renovation foundedRenovation = _renovations.Find(acm => acm.RenovationId == renovation.RenovationId);
            _renovations.Remove(foundedRenovation);
            _serializer.ToCSV(FilePath, _renovations);
        }
        public Renovation Update(Renovation renovation)
        {
            _renovations = _serializer.FromCSV(FilePath);
            Renovation current = _renovations.Find(acm => acm.RenovationId == renovation.RenovationId);
            int index = _renovations.IndexOf(current);
            _renovations.Remove(current);
            _renovations.Insert(index, renovation);
            _serializer.ToCSV(FilePath, _renovations);
            return renovation;
        }

    }
}
