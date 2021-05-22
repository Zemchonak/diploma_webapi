using FitnessCenterManagement.Api.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessCenterManagement.Api.Contexts
{
    public class AuthApiDbContext : IdentityDbContext<User>
    {
        public AuthApiDbContext(DbContextOptions<AuthApiDbContext> options)
            : base(options)
        {
        }
    }
}
