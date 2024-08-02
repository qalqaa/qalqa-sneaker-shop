using Autorisation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qalqasneakershop.Data;
using qalqasneakershop.Models;
using Autorisation.Data;
using Autorisation.Models;
using System.Collections.Generic;
using System.Security.Claims;
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
        private readonly ApplicationUserDbContext _userContext;

        public ItemsController(IItemRepository itemRepository, ApplicationDbContext context, UsersService usersService, ApplicationUserDbContext userContext)
        {
            _itemRepository = itemRepository;
            _context = context;
            _usersService = usersService;
            _userContext = userContext;
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
            var result = items.Select(item => new
            {
                item.Id,
                item.Title,
                item.Price,
                item.ImageUrl,
                item.Description,
                item.Rating
            }).ToList();
            return Ok(result);
        }

        [HttpGet("description")]
        public async Task<ActionResult<List<ItemDescription>>> GetDescriptionItems()
        {
            var descriptions = await _context.Items
                                             .Select(item => item.Description)
                                             .ToListAsync();
            return Ok(descriptions);
        }
        [HttpGet("rating")]
        public async Task<ActionResult<List<ItemRating>>> GetRatingItems()
        {
            var ratings = await _context.Items
                                        .Select(item => item.Rating)
                                        .ToListAsync();
            return Ok(ratings);
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
        [Authorize]
        [HttpGet("favorites")]
        public async Task<ActionResult<List<Item>>> GetUserFavourites()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var userFavourites = await _userContext.UsersFull
                                               .Where(u => u.UserID == userId)
                                               .Select(u => u.Favourites)
                                               .FirstOrDefaultAsync();

            if (userFavourites == null)
            {
                return NotFound("Избранные пользователя не найдены");
            }

            var sneakerIds = userFavourites
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(id => int.Parse(id.Trim())) 
                .ToList();

            var sneakers = await _context.Items
                                         .Where(i => sneakerIds.Contains(i.Id))
                                         .ToListAsync();

            if (sneakers == null || !sneakers.Any())
            {
                return NotFound("Не найдены кроссовки с таким Id");
            }

            return Ok(sneakers);
        }
        [Authorize]
        [HttpPost("add-to-favorites")]
        public async Task<ActionResult> AddToUserFavourites(int sneakerId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var user = await _userContext.UsersFull
                                         .Where(u => u.UserID == userId)
                                         .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("Пользователь не найден");
            }

            var userFavourites = user.Favourites ?? string.Empty;
            var favouriteIds = userFavourites
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(id => int.Parse(id.Trim()))
                .ToList();

            if (!favouriteIds.Contains(sneakerId))
            {
                favouriteIds.Add(sneakerId);
                user.Favourites = string.Join(",", favouriteIds);
                await _userContext.SaveChangesAsync();
                return Ok("Товар добавлен в избранные");
            }
            else
            {
                return BadRequest("Товар уже находится в избранных");
            }
        }
        [Authorize]
        [HttpDelete("remove-from-favorites/{sneakerId}")]
        public async Task<ActionResult> RemoveFromUserFavourites(int sneakerId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var user = await _userContext.UsersFull
                                         .Where(u => u.UserID == userId)
                                         .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("Пользователь не найден");
            }

            var userFavourites = user.Favourites ?? string.Empty;
            var favouriteIds = userFavourites
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(id => int.Parse(id.Trim()))
                .ToList();

            if (favouriteIds.Contains(sneakerId))
            {
                favouriteIds.Remove(sneakerId);
                user.Favourites = string.Join(",", favouriteIds);
                await _userContext.SaveChangesAsync();
                return Ok("Товар удален из избранных");
            }
            else
            {
                return NotFound("Товар не найден в избранных");
            }
        }

    }

}
