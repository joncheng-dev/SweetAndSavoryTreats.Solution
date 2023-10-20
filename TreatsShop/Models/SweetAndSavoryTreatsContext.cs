using Microsoft.EntityFrameworkCore;

namespace SweetAndSavoryTreats.Models
{
  public class SweetAndSavoryTreatsContext : DbContext
  {
    public DbSet<Flavor> Flavors { get; set; }
    public DbSet<Treat> Treats { get; set; }
    public DbSet<FlavorTreat> FlavorTreats { get; set; }

    public SweetAndSavoryTreatsContext(DbContextOptions options) : base(options) { }
  }
}