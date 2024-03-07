using InitialProject.Domen.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services.IServices
{
    public interface IForumService
    {
        List<Forum> GetAll();
        Forum Save(Forum forum);
        void Delete(Forum forum);
        void Update(Forum forum);
        List<Forum> GetForumsByCreatorId(int userId);
        List<Forum> GetActiveForumsByCreatorId(int userId);
        Forum GetForumById(int forumId);
        Dictionary<int, string> GetForumsByUserKeyValue(int userId);
        string GetTopic(int forumId);
        Location GetLocation(int forumId);
    }
}
