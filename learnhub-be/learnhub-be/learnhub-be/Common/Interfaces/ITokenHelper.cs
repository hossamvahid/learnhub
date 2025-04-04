using System.Security.Claims;

namespace learnhub_be.Common.Interfaces
{
    public interface ITokenHelper
    {
        public abstract static string GenerateJwtToken(Claim[] claim);

        public abstract static string GetUsername(IEnumerable<Claim> claims);

        public abstract static string GetRole(IEnumerable<Claim> claims);
        
    }
}
