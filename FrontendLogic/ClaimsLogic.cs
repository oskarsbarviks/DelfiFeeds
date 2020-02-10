using FrontendLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FrontendLogic
{
    /// <summary>
    /// Class responsible for user claims reading and validation. 
    /// </summary>
    public class ClaimsLogic : IClaimsLogic
    {
        public string GetUserIdFromClaims(IEnumerable<Claim> claims)
        {
            var userIDClaim = claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);

            if (userIDClaim == null)
            {
                throw new UnauthorizedAccessException("User ID claim is not found");
            }
            else
            {
                return userIDClaim.Value;
            }
        }
    }
}
