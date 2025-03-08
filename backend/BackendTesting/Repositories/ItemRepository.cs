using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using qalqasneakershop.Data;
using qalqasneakershop.Models;

public class ItemRepository : IItemRepository
{
    private readonly ApplicationDbContext _context;

    public ItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Item>> GetAllItemsAsync(string sortOrder, string searchString)
    {
        var items = from i in _context.Items select i;

        if (!string.IsNullOrEmpty(searchString))
        {
            items = items.Where(i => i.Title.Contains(searchString));
        }

        items = sortOrder switch
        {
            "title_desc" => items.OrderByDescending(i => i.Title),
            "price" => items.OrderBy(i => i.Price),
            "price_desc" => items.OrderByDescending(i => i.Price),
            _ => items.OrderBy(i => i.Title),
        };

        return await items.ToListAsync();
    }
}
