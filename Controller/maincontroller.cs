using Autorisation.Services;
using Autorisation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using qalqasneakershop.Data;
using qalqasneakershop.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Autorisation.Data;

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
            }).ToList();
            return Ok(result);
        }

        [HttpGet("info")]
        public async Task<ActionResult<List<ItemDescription>>> GetDescriptionItems()
        {
            var items = await _context.Items.ToListAsync();
            var result = items.Select(item => new
            {
                item.Id,
                item.Title,
                item.Price,
                item.ImageUrl,
                item.Description,
                item.Rating,
                item.Reviews,
            }).ToList();
            return Ok(result);
        }

        [HttpGet("info/{id}")]
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
        [HttpGet("test")]
        public async Task<ActionResult<List<UserEntity>>> GetUsers()
        {
            try
            {
                var users = await _userContext.UsersFull.ToListAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }
        [HttpGet("datatest")]
        [Authorize]
        public IActionResult GetData()
        {
            return Ok(new { Message = "Authorized access" });
        }
    }
}
