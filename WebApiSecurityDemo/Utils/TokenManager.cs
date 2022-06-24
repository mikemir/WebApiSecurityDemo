using Microsoft.AspNetCore.Http;
using System.Linq;

namespace WebApiSecurityDemo.Utils
{
    public class TokenManager : ITokenManager
    {
        private IHttpContextAccessor _accessor;

        public TokenManager(IHttpContextAccessor httpContextAccessor)
        {
            _accessor = httpContextAccessor;
        }

        public int GetIdJwt()
        {
            var claimId = _accessor.HttpContext.User.Claims.SingleOrDefault(item => item.Type == "id");
            return int.Parse(claimId.Value);
        }
    }
}