namespace MovieList.Middleware
{
    using Microsoft.AspNetCore.Http;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using MovieList.Models;

    public class PrivacyMiddleware
    {
        private readonly RequestDelegate _next;

        public PrivacyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = await userManager.FindByIdAsync(userId);

                if (user != null && user.IsPrivate)
                {
                    context.Response.StatusCode = 403; // Forbidden
                    await context.Response.WriteAsync("This profile is private.");
                    return;
                }
            }

            await _next(context);
        }
    }
}
