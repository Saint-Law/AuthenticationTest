namespace AuthTest.Logic.Interface;

public interface IProduct
{
    Task<ApiListResponse3<Product>> GetProducts(int pageNumber, int pageSize);
    Task<ApiResponse<Product>> GetSingleProduct(int Id);
    Task<ApiResponse<CreateProductDto>> CreateProduct(CreateProductDto request);
    Task<ApiResponse<UpdateProductDto>> UpdateProduct(UpdateProductDto request);
}
