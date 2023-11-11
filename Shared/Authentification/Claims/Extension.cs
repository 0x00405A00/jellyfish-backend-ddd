using cl = System.Security.Claims;

namespace Shared.Authentification.Claims
{
    public static class Extension
    {
        public static List<cl.Claim> GetClaims(this IEnumerable<cl.Claim> claims, Func<cl.Claim, bool> condition)
        {
            List<cl.Claim> claim = new List<cl.Claim>();
            if (claims.Any())
            {
                claim = claims.ToList()
                    .Where(condition)
                    .ToList();
            }
            return claim;
        }
    }
}
