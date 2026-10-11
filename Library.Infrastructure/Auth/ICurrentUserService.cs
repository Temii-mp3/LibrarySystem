namespace Library.Infrastructure.Auth
{
    public interface ICurrentUserService
    {
        string? GetEmail();
        string? GetUserId();
        bool IsAdmin();
    }
}