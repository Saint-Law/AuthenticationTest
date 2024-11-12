

namespace AuthTest.Data;

public interface IAuthDbContext
{
    DbSet<Category> Categories { get; set; }
    DbSet<Product> Products { get; set; }
    DbSet<Sales> Sales { get; set; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

}
