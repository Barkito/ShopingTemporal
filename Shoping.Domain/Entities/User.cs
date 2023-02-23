using Shoping.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Shoping.Domain;
public class User : IdentityUser
{
    public ICollection<RefreshToken> AccessTokens { get; set; } =
        new HashSet<RefreshToken>();

}
