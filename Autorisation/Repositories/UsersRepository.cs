using Autorisation.Data;
using Autorisation.Interfaces;
using Autorisation.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using qalqasneakershop.Data;

namespace Autorisation.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationUserDbContext _context;

        public UsersRepository(ApplicationUserDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Add(User user)
        {
            var userEntity = _mapper.Map<UserEntity>(user);

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            return userEntity != null ? _mapper.Map<User>(userEntity) : null;
        }

        public async Task<bool> EmailExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
