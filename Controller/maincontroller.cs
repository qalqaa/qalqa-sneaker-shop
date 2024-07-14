using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qalqasneakershop.Data;
using qalqasneakershop.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace qalqasneakershop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly ApplicationDbContext _context;

        public ItemsController(IItemRepository itemRepository, ApplicationDbContext context)
        {
            _itemRepository = itemRepository;
            _context = context;
        }

        [HttpGet("sorted")]
        public async Task<IActionResult> GetSortedItems(string? sortOrder, string? searchString)
        {
            var items = await _itemRepository.GetAllItemsAsync(sortOrder, searchString);
            return Ok(items);
        }

        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetItems()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemById(int id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}
