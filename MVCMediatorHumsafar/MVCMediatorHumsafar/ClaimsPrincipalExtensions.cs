using System.Security.Claims;

namespace MVC_Humsafar_Mubarak
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {

            return user.FindFirst(ClaimTypes.NameIdentifier).Value;

        }
    }
}
