using PurchasingSystem.Domain.User.Entities;

namespace PurchasingSystem.Application.Services.Abstractions
{
    public interface IJwtTokenProvider
    {
        string GenerateToken(User user);
    }
}
