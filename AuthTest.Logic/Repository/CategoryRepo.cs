namespace AuthTest.Logic.Repository;

public class CategoryRepo : ICategory
{
    private readonly IDbConnection _connection;
    private readonly CategoryDbService service;
    private readonly IMapper _mapper;

    public CategoryRepo(IDbConnection connection, IMapper mapper)
    {
        _connection = connection;
        _mapper = mapper;
        service = new CategoryDbService(connection);
    }

    public async Task<ApiResponse<CreateCategoryDto>> CreateCategory(CreateCategoryDto request)
    {
        var response = new ApiResponse<CreateCategoryDto>();
        var model = _mapper.Map<Category>(request);
        var result = await service.LogCategory(model);
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

    public async Task<ApiListResponse3<Category>> GetCategories(int pageNumber, int pageSize)
    {
        var response = new ApiListResponse3<Category>();
        var result = await service.GetCategories(pageNumber, pageSize);
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

    public async Task<ApiResponse<Category>> GetSingleCategory(int Id)
    {
        var response = new ApiResponse<Category>();
        var result = await service.SingleCategory(Id);
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

    public async Task<ApiResponse<UpdateCategoryDto>> UpdateCategory(UpdateCategoryDto request)
    {
        var response = new ApiResponse<UpdateCategoryDto>();
        var model = _mapper.Map<Category>(request);
        var result = await service.UpdateDiscount(model);
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
