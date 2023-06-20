using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

static void Main(string[] args)
{
    var _context = new AppDbContext();
    var data = _context.Users.ToList();
    Console.WriteLine(data);
}

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        for (int i = 1; i <= 200; i++)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = i,
                Name = $"Test={i}",
                Surname = $"Test={i}",
                Age = i,
            });
        }
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users { get; set; }
}

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }     
    public int Age { get; set; }
}