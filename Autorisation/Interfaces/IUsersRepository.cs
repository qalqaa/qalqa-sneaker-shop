using Autorisation.Models;
using System.Threading.Tasks;

namespace Autorisation.Interfaces
{
    public interface IUsersRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
    }
}
