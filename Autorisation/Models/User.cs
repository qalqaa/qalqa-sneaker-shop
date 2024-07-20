using System;

namespace Autorisation.Models
{
    public class User
    {
        private User(Guid id, string userName, string passwordHash, string email, string favourites)
        {
            UserId = id;
            Username = userName;
            Password = passwordHash;
            Email = email;
            Favourites = favourites;
        }

        public Guid UserId { get; set; }
        public string Username { get; private set; } = string.Empty;
        public string Password { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string Favourites { get; set; } = string.Empty;

        public int[] GetFavouritesAsArray()
        {
            return Favourites
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
        }
        public void SetFavouritesFromArray(int[] favouritesArray)
        {
            Favourites = string.Join(",", favouritesArray);
        }

        public User()
        {

        }

        public static User Create(Guid id, string userName, string passwordHash, string email, string favourites)
        {
            return new User(id, userName, passwordHash, email, favourites);
        }
    }
}
