using Autorisation.Services;
using Autorisation.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Autorisation.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            app.MapPost("login", Login);
            app.MapGet("favourites", GetFavourites)
               .RequireAuthorization();
            return app;
        }

        private static async Task<IResult> Register(
            RegisterUserRequest request,
            UsersService usersService)
        {
            try
            {
                await usersService.Register(request.UserName, request.Email, request.Password);
                return Results.Ok(new { Message = "Регистрация успешно выполнена!" });
            }
            catch (Exception ex)
            {
                return Results.BadRequest(new { Error = ex.Message });
            }
        }

        private static async Task<IResult> Login(
            LoginUserRequest request,
            UsersService usersService,
            HttpContext context)
        {
            try
            {
                var token = await usersService.Login(request.Email, request.Password);

                var response = new { Token = token };
                return Results.Json(response);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status401Unauthorized);
            }
        }

        private static async Task<IResult> GetFavourites(
        HttpContext context,
        UsersService usersService)
        {
            try
            {
                var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null)
                {
                    return Results.Problem("User is not authenticated.", statusCode: StatusCodes.Status401Unauthorized);
                }

                if (!Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    return Results.Problem("Invalid user identifier.", statusCode: StatusCodes.Status400BadRequest);
                }

                var sneakers = await usersService.GetFavouriteSneakers(userId);
                return Results.Json(sneakers);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }

    }
}
