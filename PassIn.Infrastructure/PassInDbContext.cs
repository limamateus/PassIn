using Microsoft.EntityFrameworkCore;
using PassIn.Infrastructure.Entities;

namespace PassIn.Infrastructure;

public class PassInDbContext : DbContext
{
    public DbSet<Event> Events {get; set; }

    public DbSet<Attendee> Attendees { get; set; }

    public DbSet<CheckIn> CheckIns { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=D:\Alura\C# .net\Nlw\nlw-unite-c-sharp-6f784e5b85df2462e1cb76c9682181d89b91db1e\PassIn\PassInDb.db");
    }
}
