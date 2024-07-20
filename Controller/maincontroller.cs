using Autorisation.Services;
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
        private readonly UsersService _usersService;
        private readonly IItemRepository _itemRepository;
        private readonly ApplicationDbContext _context;

        public ItemsController(IItemRepository itemRepository, ApplicationDbContext context, UsersService usersService)
        {
            _itemRepository = itemRepository;
            _context = context;
            _usersService = usersService;
        }

        [HttpGet("{userId}/favourites")]
        public async Task<IActionResult> GetFavourites(Guid userId)
        {
            try
            {
                var sneakers = await _usersService.GetFavouriteSneakers(userId);
                return Ok(sneakers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
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
