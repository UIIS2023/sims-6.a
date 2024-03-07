using InitialProject.Domen.Model;

namespace InitialProject.Services
{
    public interface IGuideStatusService
    {
        GuideStatus GetStatusByUserId(int id);

        void CheckIfGuideIsSuper(int guideId);
        void UpdateToUnemployed(int id);
    }
}