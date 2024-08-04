using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autorisation.Interfaces;
using Autorisation.Models;
using qalqasneakershop.Models;

namespace Autorisation.Services
{
    public class UsersService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUsersRepository _usersRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly ISneakersRepository _sneakersRepository;

        public UsersService(
            IUsersRepository usersRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider jwtProvider,
            ISneakersRepository sneakersRepository)
        {
            _passwordHasher = passwordHasher;
            _usersRepository = usersRepository;
            _jwtProvider = jwtProvider;
            _sneakersRepository = sneakersRepository;
        }

        public async Task Register(string userName, string email, string password)
        {
            if (await _usersRepository.EmailExists(email))
            {
                throw new Exception("Данный пользователь уже существует");
            }

            var hashedPassword = _passwordHasher.Generate(password);
            var user = User.Create(Guid.NewGuid(), userName, hashedPassword, email, string.Empty);
            await _usersRepository.Add(user);
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email)
                       ?? throw new Exception("Пользователь не найден!");

            var result = _passwordHasher.Verify(password, user.Password);

            if (!result)
            {
                throw new Exception("Неверный адрес электронной почты или пароль");
            }

            var token = _jwtProvider.GenerateToken(user);
            return token;
        }

        public async Task<IEnumerable<Item>> GetFavouriteSneakers(Guid userId)
        {
            var user = await _usersRepository.GetById(userId)
                       ?? throw new Exception("Пользователь не найден!");

            var favouriteIds = user.GetFavouritesAsArray();
            var sneakers = await _sneakersRepository.GetSneakersByIds(favouriteIds);
            return sneakers;
        }
    }
}
