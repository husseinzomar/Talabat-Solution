using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Talabat.API.Dtos;
using Talabat.DAL.Entities.Identity;

namespace Talabat.API.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email);
            return await userManager.Users.Include(U => U.Address).SingleOrDefaultAsync(U => U.Email == email);
        }
    }
}
