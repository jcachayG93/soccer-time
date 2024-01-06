using System.Reflection;
using Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Application.Persistence;

public class AppDbContext 
: DbContext
{
    public DbSet<SoccerField> SoccerFields { get; set; } = null!;
    public AppDbContext(
        DbContextOptions<AppDbContext> options)
        : base(options)

    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}