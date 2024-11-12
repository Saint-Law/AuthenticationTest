

namespace AuthTest.Data;

public class AuthDbContext : DbContext, IAuthDbContext
{
    public AuthDbContext()
    {
        
    }

    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
        
    }

    public static AuthDbContext Create()
    {
        return new AuthDbContext();
    }


    public DbSet<Product> Products { get; set; }
    public DbSet<Sales> Sales { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Role> Roles { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile($"appsettings.{envName}.json", optional: true)
            .Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

    }
}
