using System.Collections.Generic;
using System.Threading.Tasks;
using Autorisation.Models;
using qalqasneakershop.Models;

namespace Autorisation.Interfaces
{
    public interface ISneakersRepository
    {
        Task<IEnumerable<Item>> GetSneakersByIds(int[] ids);
    }
}
