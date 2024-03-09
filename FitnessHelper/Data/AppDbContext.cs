using FitnessHelper.Domain;
using Microsoft.EntityFrameworkCore;

namespace FitnessHelper.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<FoodsClass>? Foods { get; set; }
}
