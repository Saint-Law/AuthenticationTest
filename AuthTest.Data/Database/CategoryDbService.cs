namespace AuthTest.Data.Database;

public class CategoryDbService
{
    private readonly IDbConnection _connection;
    private readonly static Logger _logger = NLog.LogManager.GetCurrentClassLogger();
    public CategoryDbService(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<int> LogCategory(Category request)
    {
        try
        {
            var query = @"[InsertInto_Category]";
            var param = new
            {
                CategoryName = request.CategoryName,
                Description = request.Description

            };
            return await _connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
            return 0;
        }
    }

    public async Task<Category> SingleCategory(int Id)
    {
        Category category = new Category();
        try
        {
            var query = @"[GetCategory]";
            var param = new { Id = Id };
            category = await _connection.QueryFirstOrDefaultAsync<Category>(query, param, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {

            _logger.Error(ex);
            if (ex.Message.Equals("Sequence contains no elements"))
            {
                return category;
            }
        }
        return category;
    }

    public async Task<ApiListResponse3<Category>> GetCategories(int pageNumber, int pageSize)
    {
        var response = new ApiListResponse3<Category>();
        try
        {
            var query = @"[GetAllCategories]";
            var param = new { pageNumber = pageNumber, pageSize = pageSize };
            var result = await _connection.QueryAsync<Category>(query, param, commandType: CommandType.StoredProcedure);
            response.Data = PagedList<Category>.ToPagedList(result, pageNumber, pageSize);

        }
        catch (Exception ex)
        {
            _logger.Error(ex);
            if (ex.Message.Equals("Seqence contains no elements"))
            {
                response.Code = "01";
            }
        }
        return response;
    }

    public async Task<int> UpdateDiscount(Category request)
    {
        try
        {
            var query = @"[Update_Category]";
            var param = new
            {
                CategoryName = request.CategoryName,
                Description = request.Description,
                Id = request.Id
            };
            return await _connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {

            _logger.Error(ex);
            return 0;
        }
    }
}
