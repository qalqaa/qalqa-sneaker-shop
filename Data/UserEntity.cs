namespace Autorisation.Data
{
    public class UserEntity
    {
        public Guid UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Favourites { get; set; } = string.Empty;
    }
}
