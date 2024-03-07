using InitialProject.Aplication.Factory;
using InitialProject.Application.Contracts.Repository;
using InitialProject.Domen.Model;
using InitialProject.Infrastructure.Repository;
using InitialProject.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Services
{
    public class ForumService : IForumService
    {
        private IForumRepository _forumRepository;
        public ForumService()
        {
            _forumRepository = Injector.CreateInstance<IForumRepository>();
        }
        public void Delete(Forum forum)
        {
            _forumRepository.Delete(forum);
        }

        public List<Forum> GetActiveForumsByCreatorId(int userId)
        {
            List<Forum> forums = GetAll();
            return forums.Where(forum => forum.CreatorId == userId && forum.Status == ForumStatus.Open).ToList();
        }

        public List<Forum> GetAll()
        {
            return _forumRepository.GetAll();
        }

        public Forum GetForumById(int forumId)
        {
            return GetAll().Find(forum => forum.ForumId == forumId);
        }

        public List<Forum> GetForumsByCreatorId(int userId)
        {
            List<Forum> forums = GetAll();
            return forums.Where(forum => forum.CreatorId == userId).ToList();
        }

        public Dictionary<int, string> GetForumsByUserKeyValue(int userId)
        {
            List<Forum> forums = GetActiveForumsByCreatorId(userId);
            Dictionary<int, string> result = new Dictionary<int, string>();
            foreach (Forum forum in forums)
            {
                string value = forum.Location.ToString() + " - " + forum.ForumTopic;
                result.Add(forum.ForumId, value);
            }
            return result;
        }

        public string GetTopic(int forumId)
        {
            Forum forum = GetForumById(forumId);
            return forum.ForumTopic;
        }

        public Forum Save(Forum forum)
        {
            return _forumRepository.Save(forum);
        }

        public void Update(Forum forum)
        {
            _forumRepository.Update(forum);
        }
        public Location GetLocation(int forumId)
        {
            Forum forum = GetForumById(forumId);
            return forum.Location;
        }
    }
}
