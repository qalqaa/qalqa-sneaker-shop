using Microsoft.AspNetCore.Identity;

namespace qalqasneakershop.Data.Identity
{
    public class ApplicationIdentityUser : IdentityUser
    {
        public long ApplicationId { get; set; }
    }
}