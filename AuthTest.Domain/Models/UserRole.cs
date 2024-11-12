namespace AuthTest.Domain.Models;

public class UserRole : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }
}
