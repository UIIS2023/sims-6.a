namespace InitialProject.Infrastructure.Repository
{
    using InitialProject.Aplication.Contracts.Repository;
    using InitialProject.Domen.Model;
    using InitialProject.Serializer;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TourRequestRepository : ITourRequestRepository
    {
        private const string FilePath = "../../../Infrastructure/Resources/Data/tourrequests.txt";

        private readonly Serializer<TourRequest> _serializer;

        private List<TourRequest> _tourRequests;

        public TourRequestRepository()
        {
            _serializer = new Serializer<TourRequest>();
            try
            {
                _tourRequests = _serializer.FromCSV(FilePath);
            }
            catch (Exception ex)
            {
                // Log the exception or show a message to the user
                // For example: Console.WriteLine($"Error initializing TourRepository: {ex.Message}");

                // Set _tours to an empty list to avoid NullReferenceException
                _tourRequests = new List<TourRequest>();
            }
        }

        public void Delete(TourRequest tourRequest)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            TourRequest foundedTourRequest = _tourRequests.Find(t => t.Id == tourRequest.Id);
            _tourRequests.Remove(foundedTourRequest);
            _serializer.ToCSV(FilePath, _tourRequests);
        }

        public List<TourRequest> GetAll()
        {
            return _tourRequests;
        }

        public TourRequest GetById(int id)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            TourRequest foundedTour = _tourRequests.Find(t => t.Id == id);
            return foundedTour;
        }

        public int NextId()
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            if (_tourRequests.Count < 1)
            {
                return 1;
            }
            return _tourRequests.Max(t => t.Id) + 1;
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            tourRequest.Id = NextId();
            _tourRequests = _serializer.FromCSV(FilePath);
            _tourRequests.Add(tourRequest);
            _serializer.ToCSV(FilePath, _tourRequests);
            return tourRequest;
        }

        public TourRequest Update(TourRequest tourRequest)
        {
            _tourRequests = _serializer.FromCSV(FilePath);
            TourRequest current = _tourRequests.Find(t => t.Id == tourRequest.Id);
            int index = _tourRequests.IndexOf(current);
            _tourRequests.Remove(current);
            _tourRequests.Insert(index, tourRequest);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _tourRequests);
            return tourRequest;
        }

    }
}
