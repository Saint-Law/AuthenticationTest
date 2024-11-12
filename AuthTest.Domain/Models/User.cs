namespace AuthTest.Domain.Models;

public class User : BaseEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    [Required]
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int RoleId { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public bool LockoutEnabled { get; set; }
    public int PasswordTrial { get; set; }
}
