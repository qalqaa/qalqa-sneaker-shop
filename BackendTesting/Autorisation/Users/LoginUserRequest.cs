using System.ComponentModel.DataAnnotations;

namespace Autorisation.Users
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password);
}
