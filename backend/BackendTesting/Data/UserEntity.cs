namespace Autorisation.Data
{
    public class UserEntity
    {
        public Guid UserID { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Favourites { get; set; } = string.Empty;
        public string FIO { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Sex { get; set; } = string.Empty;
    }

    public class ChangePasswordRequest
    {
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
