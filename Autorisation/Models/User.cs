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
        public string Username { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;

        public User()
        {

        }

        public static User Create(Guid id, string userName, string passwordHash, string email)
        {
            return new User(id, userName, passwordHash, email);
        }
    }
}
