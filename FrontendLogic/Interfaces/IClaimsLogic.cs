using System.Collections.Generic;
using System.Security.Claims;

namespace FrontendLogic.Interfaces
{
    public interface IClaimsLogic
    {
        string GetUserIdFromClaims(IEnumerable<Claim> claims);
    }
}