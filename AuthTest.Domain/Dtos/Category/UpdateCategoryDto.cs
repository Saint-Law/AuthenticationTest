
namespace AuthTest.Domain.Dtos.Category;

public class UpdateCategoryDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public DateTime? DateModified { get; set; }
}
