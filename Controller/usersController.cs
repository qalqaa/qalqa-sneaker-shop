using Autorisation.Data;
using Autorisation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using qalqasneakershop.Data;
using qalqasneakershop.Models;
using Microsoft.EntityFrameworkCore;
using Azure.Core;
using Autorisation.Interfaces;
using Autorisation;

namespace qalqasneakershop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UsersService _usersService;
        private readonly ApplicationUserDbContext _userContext;
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UsersController(UsersService usersService, ApplicationUserDbContext userContext, IPasswordHasher passwordHasher, IUsersRepository usersRepository)
        {
            _usersService = usersService;
            _userContext = userContext;
            _passwordHasher = passwordHasher;
            _usersRepository = usersRepository;
        }

        [Authorize]
        [HttpGet("info")]
        public async Task<ActionResult<List<UserEntity>>> GetUserInfo()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var userInfo = await _userContext.UsersFull
                                             .Where(u => u.UserID == userId)
                                             .Select(u => new
                                             {
                                                 u.Username,
                                                 u.Email,
                                                 u.FIO,
                                                 u.PhoneNumber,
                                                 u.Sex
                                             })
                                             .FirstOrDefaultAsync();
            return Ok(userInfo);
        }
        [Authorize]
        [HttpPost("updateInfo")]
        public async Task<ActionResult> UpdateUserInfo([FromBody] UserEntity request)
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

            user.FIO = string.IsNullOrWhiteSpace(request.FIO) ? user.FIO : request.FIO;
            user.PhoneNumber = string.IsNullOrWhiteSpace(request.PhoneNumber) ? user.PhoneNumber : request.PhoneNumber;
            user.Sex = string.IsNullOrWhiteSpace(request.Sex) ? user.Sex : request.Sex;
            user.Email = string.IsNullOrWhiteSpace(request.Email) ? user.Email : request.Email;

            await _userContext.SaveChangesAsync();

            return Ok("Информация о пользователе обновлена");

        }
        [Authorize]
        [HttpPost("passwordChange")]
        public async Task<ActionResult> PasswordChange([FromBody] ChangePasswordRequest request)
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

            if (!_passwordHasher.Verify(request.OldPassword, user.Password))
            {
                return BadRequest("Неверный старый пароль");
            }

            if (request.NewPassword != request.ConfirmPassword)
            {
                return BadRequest("Новый пароль и подтверждение пароля не совпадают");
            }

            user.Password = _passwordHasher.Generate(request.NewPassword);
            await _userContext.SaveChangesAsync();

            return Ok("Пароль успешно изменен");
        }
        [Authorize]
        [HttpPost("createOrders")]
        public async Task<ActionResult> CreateOrder([FromBody] Order request)
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

            if (string.IsNullOrWhiteSpace(request.SneakerID))
            {
                return BadRequest("Корзина пуста");
            }

            var order = new Order
            {
                UserID = userId,
                SneakerID = request.SneakerID
            };

            _userContext.Orders.Add(order);
            await _userContext.SaveChangesAsync();

            return Ok("Заказ успешно создан");
        }

        [Authorize]
        [HttpGet("getOrders")]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim.Value);

            var orders = await _userContext.Orders
                                   .Where(o => o.UserID == userId)
                                   .ToListAsync();

            if (orders == null || !orders.Any())
            {
                return NotFound("Заказы для данного пользователя не найдены");
            }

            return Ok(orders);
        }
    }
}