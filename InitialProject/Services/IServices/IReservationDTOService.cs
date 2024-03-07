using InitialProject.Domen.CustomClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IReservationDTOService
    {
        List<ReservationDTO> GetUpcomingReservationsByUser();
        List<ReservationDTO> GetPastReservationsByUser();
    }
}
