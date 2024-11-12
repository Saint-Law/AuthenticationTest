namespace AuthTest.Domain.Models;

public class Category : BaseEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string CategoryName { get; set; }
    public string Description { get; set; }

}
