namespace Koncar.Interview.Server.Infrastructure.Db.Postgres;

using Koncar.Interview.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public sealed class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) 
        : base(options)
    { 
    }

    public DbSet<Character> Characters { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }
}
