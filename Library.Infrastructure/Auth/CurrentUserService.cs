using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Library.Infrastructure.Auth
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public string? GetEmail()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.FindFirst(ClaimTypes.Email)?.Value
                   ?? user?.FindFirst("email")?.Value;
        }

        public string? GetUserId() =>
            _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public bool IsAdmin() => Convert.ToBoolean(_httpContextAccessor.HttpContext?.User.FindFirst("is_admin")?.Value);
    }
}