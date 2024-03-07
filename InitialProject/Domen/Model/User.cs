using System;

namespace InitialProject.Domen.Model
{
    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType TypeOfUser { get; set; }
        public int Age { get; set; }
        public bool IsSuperGuest { get; set; }
        public int Points { get; set; }
        public User(string name, string username, string email, string password, UserType typeOfUser, int age)
        {
            Name = name;
            Username = username;
            Email = email;
            Password = password;
            TypeOfUser = typeOfUser;
            Age = age;
            IsSuperGuest = false;
            Points = 0;
        }
        public User()
        {
            Id = -1;
            Name = "";
            Username = "";
            Email = "";
            Password = "";
        }
        public string[] ToCSV()
        {
            string[] csvValues = {
                Id.ToString(),
                Name,
                Username,
                Email,
                Password,
                TypeOfUser.ToString(),
                Age.ToString(),
                IsSuperGuest.ToString(),
                Points.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Username = values[2];
            Email = values[3];
            Password = values[4];
            TypeOfUser = (UserType)Enum.Parse(typeof(UserType), values[5]);
            Age = Convert.ToInt32(values[6]);
            IsSuperGuest = Convert.ToBoolean(values[7]);
            Points = Convert.ToInt32(values[8]);
        }
    }
}
