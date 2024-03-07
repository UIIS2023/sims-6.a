using InitialProject.Domen.Model;
using System.Collections.Generic;

namespace InitialProject.Infrastructure.Repository
{
    public interface IGuideStatusRepository
    {
        void Delete(GuideStatus status);
        List<GuideStatus> getAll();
        GuideStatus GetById(int id);
        int NextId();
        GuideStatus Save(GuideStatus status);
        GuideStatus Update(GuideStatus status);
    }
}