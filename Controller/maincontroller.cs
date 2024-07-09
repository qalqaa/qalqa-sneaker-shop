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
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
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
    [Route("[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("CORS is working!");
        }
    }
}
