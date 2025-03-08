using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autorisation.Interfaces;
using Autorisation.Models;
using Microsoft.EntityFrameworkCore;
using qalqasneakershop.Data;
using qalqasneakershop.Models;

namespace Autorisation.Repositories
{
    public class SneakersRepository : ISneakersRepository
    {
        private readonly ApplicationDbContext _context;

        public SneakersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Item>> GetSneakersByIds(int[] ids)
        {
            return await _context.Items.Where(s => ids.Contains(s.Id)).ToListAsync();
        }
        
    }
}
