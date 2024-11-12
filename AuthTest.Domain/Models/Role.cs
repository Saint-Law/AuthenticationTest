namespace AuthTest.Domain.Models;

public class Role : BaseEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? RoleName { get; set; }
}
