namespace AuthTest.Logic.Repository;

public class ProductRepo : IProduct
{
    private readonly IDbConnection _connection;
    private readonly ProductDbService service;
    private readonly IMapper _mapper;

    public ProductRepo(IDbConnection connection, IMapper mapper)
    {
        _connection = connection;
        _mapper = mapper;
        service = new ProductDbService(connection);
    }

    public async Task<ApiResponse<CreateProductDto>> CreateProduct(CreateProductDto request)
    {
        var response = new ApiResponse<CreateProductDto>();
        var model = _mapper.Map<Product>(request);
        var result = await service.LogProduct(model);
        if (result == 1)
        {
            response.Code = "00";
            response.Description = "Successful";
            response.Data = request;
        }
        else if (result == -1)
        {
            response.Code = "06";
            response.Description = "Record Already Exist";
        }
        else
        {
            response.Code = "99";
            response.Description = "An error occured. Please try again later";
        }
        return response;
    }

    public async Task<ApiListResponse3<Product>> GetProducts(int pageNumber, int pageSize)
    {
        var response = new ApiListResponse3<Product>();
        var result = await service.GetProducts(pageNumber, pageSize);
        if (result != null)
        {
            if (result.Data.Count() > 0)
            {
                var metadata = new
                {
                    result.Data.TotalCount,
                    result.Data.PageSize,
                    result.Data.CurrentPage,
                    result.Data.TotalPages,
                    result.Data.HasNext,
                    result.Data.HasPrevious
                };
                response.PageInfo = JsonConvert.SerializeObject(metadata);
                response.Code = "00";
                response.Description = "Successful";
                response.Data = result.Data;
            }
            else
            {
                response.Code = "01";
                response.Description = "No record found";
            }
        }
        else
        {
            response.Code = "99";
            response.Description = "An error occurred. Please try again later";
        }
        return response;
    }

    public async Task<ApiResponse<Product>> GetSingleProduct(int Id)
    {
        var response = new ApiResponse<Product>();
        var result = await service.SingleProduct(Id);
        if (result != null)
        {
            if (result.Id == 0)
            {
                response.Code = "01";
                response.Description = "No Record found";
            }
            else
            {
                response.Code = "00";
                response.Description = "Successfull";
                response.Data = result;
            }
        }
        else
        {
            response.Code = "01";
            response.Description = "No Record found";
        }
        return response;
    }

    public async Task<ApiResponse<UpdateProductDto>> UpdateProduct(UpdateProductDto request)
    {
        var response = new ApiResponse<UpdateProductDto>();
        var model = _mapper.Map<Product>(request);
        var result = await service.UpdateProduct(model);
        if (result == 1)
        {
            response.Code = "00";
            response.Description = "Successful";
            response.Data = request;
        }
        else
        {
            response.Code = "99";
            response.Description = "An error occurred. Please try again later";
        }
        return response;
    }
}
