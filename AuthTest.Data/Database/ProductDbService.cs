namespace AuthTest.Data.Database;

public class ProductDbService
{
    private readonly IDbConnection _connection;
    private readonly static Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    public ProductDbService(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<int> LogProduct(Product request)
    {
        try
        {
            var query = @"[InsertInto_Products]";
            var param = new
            {
                ProductName = request.ProductName,
                Price = request.Price,
                Quantity = request.Quantity,
                CategoryId = request.CategoryId,
                Category = request.Category
            };
            return await _connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
            return 0;
        }
    }

    public async Task<Product> SingleProduct(int Id)
    {
        Product product = new Product();
        try
        {
            var query = @"[GetProduct]";
            var param = new { Id = Id };
            product = await _connection.QueryFirstOrDefaultAsync<Product>(query, param, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {

            _logger.Error(ex);
            if (ex.Message.Equals("Sequence contains no elements"))
            {
                return product;
            }
        }
        return product;
    }

    public async Task<ApiListResponse3<Product>> GetProducts(int pageNumber, int pageSize)
    {
        var response = new ApiListResponse3<Product>();
        try
        {
            var query = @"[GetAllProducts]";
            var param = new { pageNumber = pageNumber, pageSize = pageSize };
            var result = await _connection.QueryAsync<Product>(query, param, commandType: CommandType.StoredProcedure);
            response.Data = PagedList<Product>.ToPagedList(result, pageNumber, pageSize);

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

    public async Task<int> UpdateProduct(Product request)
    {
        try
        {
            var query = @"[Update_Product]";
            var param = new
            {
                ProductName = request.ProductName,
                Price = request.Price,
                Quantity = request.Quantity,
                CategoryId = request.CategoryId,
                Category = request.Category,
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
