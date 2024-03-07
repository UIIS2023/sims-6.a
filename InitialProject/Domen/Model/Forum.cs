using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domen.Model
{
    public enum ForumStatus { Open, Closed }
    public class Forum : ISerializable
    {
        private int _forumId;
        private int _creatorId;
        private string _forumTopic;
        private Location _location;
        private DateTime _dateCreated;
        private List<Comment> _comments;
        private ForumStatus _status;
        private bool _veryUseful;

        public int ForumId { get => _forumId; set => _forumId = value; }
        public int CreatorId { get => _creatorId; set => _creatorId = value; }
        public string ForumTopic { get => _forumTopic; set => _forumTopic = value; }
        public Location Location { get => _location; set => _location = value; }
        public DateTime DateCreated { get => _dateCreated; set => _dateCreated = value; }
        public List<Comment> Comments { get => _comments; set => _comments = value; }
        public ForumStatus Status { get => _status; set => _status = value; }
        public bool VeryUseful { get => _veryUseful; set => _veryUseful = value; }

        public Forum(int creatorId, string forumTopic, Location location, DateTime dateCreated)
        {
            CreatorId = creatorId;
            ForumTopic = forumTopic;
            Location = location;
            DateCreated = dateCreated;
            Comments = new List<Comment>();
            Status = ForumStatus.Open;
            VeryUseful = false;
        }
        public Forum()
        {
            Location = new Location();
            Comments = new List<Comment>();
        }
        public string[] ToCSV()
        {
            string[] csvValues = {
            ForumId.ToString(),
            CreatorId.ToString(),
            ForumTopic,
            Location.ToString(),
            DateCreated.ToString(),
            Status.ToString(),
            VeryUseful.ToString()
        };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            ForumId = Convert.ToInt32(values[0]);
            CreatorId = Convert.ToInt32(values[1]);
            ForumTopic = values[2];
            Location = Location.fromStringToLocation(values[3]);
            DateCreated = Convert.ToDateTime(values[4]);
            Status = (ForumStatus)Enum.Parse(typeof(ForumStatus), values[5]);
            VeryUseful = Convert.ToBoolean(values[6]);
            Comments = new List<Comment>();
        }
    }
}
