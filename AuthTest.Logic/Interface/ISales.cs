namespace AuthTest.Logic.Interface;

public interface ISales
{
    Task<ApiListResponse3<Sales>> GetSales(int pageNumber, int pageSize);
    Task<ApiResponse<Sales>> GetSingleSale(int Id);
    Task<ApiResponse<CreateSalesDto>> CreateSale(CreateSalesDto request);
    Task<ApiResponse<UpdateSalesDto>> UpdateSale(UpdateSalesDto request);
}
