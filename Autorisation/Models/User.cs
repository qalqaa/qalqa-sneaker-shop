using System;

namespace Autorisation.Models
{
    public class User
    {
        private User(Guid id, string userName, string passwordHash, string email)
        {
            UserId = id;
            Username = userName;
            Password = passwordHash;
            Email = email;
        }

        public Guid UserId { get; set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }

        public User()
        {
        }

        public static User Create(Guid id, string userName, string passwordHash, string email)
        {
            return new User(id, userName, passwordHash, email);
        }
    }
}
