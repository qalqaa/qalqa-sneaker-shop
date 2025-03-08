using System.Collections.Generic;
using System.Threading.Tasks;
using qalqasneakershop.Models;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetAllItemsAsync(string sortOrder, string searchString);
}
