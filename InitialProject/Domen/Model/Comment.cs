using System;

namespace InitialProject.Domen.Model
{
    public class Comment : ISerializable
    {
        public int CommentId { get; set; }
        public DateTime CreationTime { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; }
        

        public Comment() { }

        public Comment(DateTime creationTime, string text, int userId)
        {
            CreationTime = creationTime;
            Text = text;
            UserId = userId;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { CommentId.ToString(), CreationTime.ToString(), Text, UserId.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            CommentId = Convert.ToInt32(values[0]);
            CreationTime = Convert.ToDateTime(values[1]);
            Text = values[2];
            UserId = Convert.ToInt32(values[3]);
        }
    }
}
