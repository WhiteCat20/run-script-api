using Microsoft.AspNetCore.Identity;

namespace run_script.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> Roles);
    }
}
