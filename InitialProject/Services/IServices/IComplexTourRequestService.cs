using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    interface IComplexTourRequestService
    {
        List<ComplexTourRequest> GetAllTourRequests();
        List<ComplexTourRequest> GetAllTourRequestsByUser(int userId);
        void MakeTourRequest(List<ComplexTourRequest> complexTourRequests);

        void Update(ComplexTourRequest tourRequest);

        void CheckRequests();

        List<ComplexTourRequest> GetTourRequest(int tourId);

        List<ComplexTourRequest> GetAllUniqueTourRequests();

        void UpdateSpecificRequest(int complexId, int tourId, int GuideId, DateTime startingDate);
    }
}
