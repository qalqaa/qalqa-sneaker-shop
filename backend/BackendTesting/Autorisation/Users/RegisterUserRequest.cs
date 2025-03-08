using System.ComponentModel.DataAnnotations;

namespace Autorisation.Users
{
    public record RegisterUserRequest(
        [Required] string UserName,
        [Required] string Password,
        [Required] string Email);
}