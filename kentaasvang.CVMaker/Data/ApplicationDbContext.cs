using Microsoft.EntityFrameworkCore;

namespace kentaasvang.CVMaker.Data; 

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // public DbSet<ResumeEntity> Resumes => Set<ResumeEntity>();
}