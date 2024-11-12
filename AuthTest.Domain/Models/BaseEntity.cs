namespace AuthTest.Domain.Models;

public class BaseEntity
{
    public DateTime? DateCreated { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? DateModified { get; set; }
    public string? ModifiedBy { get; set; }
    public bool IsDeleted { get; set; } 
}
