using Autorisation.Services;
using Autorisation.Users;

namespace Autorisation.Endpoints
{
    public static class UsersEndpoints
    {
        public static IEndpointRouteBuilder MapUsersEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("register", Register);
            app.MapPost("login", Login);
            return app;
        }

        private static async Task<IResult> Register(
            RegisterUserRequest request,
            UsersService usersService)
        {
            try
            {
                await usersService.Register(request.UserName, request.Email, request.Password);
                return Results.Ok(new { Message = "Registration successful" });
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
                context.Response.Cookies.Append("tasty-cookies", token);

                var response = new { Token = token };
                return Results.Json(response);
            }
            catch (Exception ex)
            {
                return Results.Problem(detail: ex.Message, statusCode: StatusCodes.Status401Unauthorized);
            }
        }
    }
}
