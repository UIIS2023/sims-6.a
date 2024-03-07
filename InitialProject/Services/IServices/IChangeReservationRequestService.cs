using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IChangeReservationRequestService
    {
        List<ChangeReservationRequest> GetRequests(int userId);
        void SaveRequest(ChangeReservationRequest request);
        void DeleteRequestByReservationId(int reservationId);
        ChangeReservationRequest FindRequestByReservationId(int reservationId);
        ChangeReservationRequest GetRequestByRequestId(int requestId);
        List<ChangeReservationRequest> GetRequestsByOwnerId(int userId);
        List<OwnerChangeRequests> OwnerChangeReservationRequest(int ownerId);
        void ChangeReservationDateRange(DateTime newStartDate, DateTime newEndDate, int reservationId);
        void AcceptRequest(int requestId);
        void DeclineRequest(int requestId, string comment);
    }
}
