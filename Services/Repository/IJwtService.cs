using System.Security.Claims;

namespace Services.Repository
{
    public interface IJwtService
    {
        string GenerateAccessToken(List<Claim> claims);
    }
}
