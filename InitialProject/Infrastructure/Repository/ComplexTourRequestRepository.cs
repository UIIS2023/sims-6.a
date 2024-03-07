using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Infrastructure.Repository
{
    class ComplexTourRequestRepository : IComplexTourRequestRepository
    {
        private const string FilePath = "../../../Infrastructure/Resources/Data/complextourrequests.txt";

        private readonly Serializer<ComplexTourRequest> _serializer;

        private List<ComplexTourRequest> _complexTourRequests;

        public ComplexTourRequestRepository()
        {
            _serializer = new Serializer<ComplexTourRequest>();
            try
            {
                _complexTourRequests = _serializer.FromCSV(FilePath);
            }
            catch (Exception ex)
            {
                // Log the exception or show a message to the user
                // For example: Console.WriteLine($"Error initializing TourRepository: {ex.Message}");

                // Set _tours to an empty list to avoid NullReferenceException
                _complexTourRequests = new List<ComplexTourRequest>();
            }
        }

        public void Delete(ComplexTourRequest complexTourRequest)
        {
            _complexTourRequests = _serializer.FromCSV(FilePath);
            ComplexTourRequest foundedTourRequest = _complexTourRequests.Find(t => t.Id == complexTourRequest.Id);
            _complexTourRequests.Remove(foundedTourRequest);
            _serializer.ToCSV(FilePath, _complexTourRequests);
        }

        public List<ComplexTourRequest> GetAll()
        {
            return _complexTourRequests;
        }

        public ComplexTourRequest GetById(int id)
        {
            _complexTourRequests = _serializer.FromCSV(FilePath);
            ComplexTourRequest foundedTour = _complexTourRequests.Find(t => t.Id == id);
            return foundedTour;
        }

        public int NextId()
        {
            _complexTourRequests = _serializer.FromCSV(FilePath);
            if (_complexTourRequests.Count < 1)
            {
                return 1;
            }
            return _complexTourRequests.Max(t => t.TourId) + 1;
        }

        public ComplexTourRequest Save(ComplexTourRequest complexTourRequest)
        {
            _complexTourRequests = _serializer.FromCSV(FilePath);
            _complexTourRequests.Add(complexTourRequest);
            _serializer.ToCSV(FilePath, _complexTourRequests);
            return complexTourRequest;
        }

        public ComplexTourRequest Update(ComplexTourRequest complexTourRequest)
        {
            _complexTourRequests = _serializer.FromCSV(FilePath);
            ComplexTourRequest current = _complexTourRequests.Find(t => t.Id == complexTourRequest.Id && t.TourId == complexTourRequest.TourId);
            int index = _complexTourRequests.IndexOf(current);
            _complexTourRequests.Remove(current);
            _complexTourRequests.Insert(index, complexTourRequest);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _complexTourRequests);
            return complexTourRequest;
        }

    }
}

