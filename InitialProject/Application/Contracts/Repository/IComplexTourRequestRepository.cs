using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Application.Contracts.Repository
{
    interface IComplexTourRequestRepository
    {
        List<ComplexTourRequest> GetAll();
        int NextId();
        ComplexTourRequest Save(ComplexTourRequest tourRequest);
        ComplexTourRequest Update(ComplexTourRequest tourRequest);
        ComplexTourRequest GetById(int id);
        void Delete(ComplexTourRequest tourRequest);
    }
}
