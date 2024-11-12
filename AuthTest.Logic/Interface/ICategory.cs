namespace AuthTest.Logic.Interface;

public interface ICategory
{
    Task<ApiListResponse3<Category>> GetCategories(int pageNumber, int pageSize);
    Task<ApiResponse<Category>> GetSingleCategory(int Id);
    Task<ApiResponse<CreateCategoryDto>> CreateCategory(CreateCategoryDto request);
    Task<ApiResponse<UpdateCategoryDto>> UpdateCategory(UpdateCategoryDto request); 
}
