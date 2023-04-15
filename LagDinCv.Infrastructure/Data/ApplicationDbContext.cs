using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LagDinCv.Infrastructure.Data;

internal class ApplicationDbContext : IdentityDbContext
{
    internal ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
}