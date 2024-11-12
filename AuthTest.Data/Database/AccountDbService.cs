


namespace AuthTest.Data.Database;

public class AccountDbService
{
    private readonly IDbConnection _connection;
    private readonly static Logger _logger = NLog.LogManager.GetCurrentClassLogger();


    public AccountDbService(IDbConnection connection)
    {
        _connection = connection;
    }


    public async Task<int> OnboardUser(RegistrationDto request, string initiator)
    {
        try
        {
            var query = @"[InsertInto_UserRegistration]";
            var param = new
            {
                UserName = request.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password.Trim()),
                FullName = request.FullName,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                RoleId = request.Role,
                CreatedBy = initiator
            };
            return await _connection.ExecuteAsync(query, param, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
            return 0;
        }
    }

    public async Task UpdateFailedLogin(string username, int failedCount, bool isLockedOut)
    {
        try
        {
            var query = @"[UpdateFailedLogin]";
            var param = new
            {
                UserName = username,
                FailedCount = failedCount,
                IsLockedout = isLockedOut
            };

            await _connection.QueryFirstAsync<int>(query, param, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
        }
    }


    public async Task<User> GetUser(string username)
    {
        try
        {
            var query = @"[GetUser]";
            var param = new
            {
                UserName = username

            };
            return await _connection.QueryFirstOrDefaultAsync<User>(query, param, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
            return null;
        }
    }


    public async Task<User> Authenticate(string username)
    {
        try
        {
            var query = @"[LoginUsers]";
            var param = new
            {
                UserName = username

            };
            return await _connection.QueryFirstOrDefaultAsync<User>(query, param, commandType: CommandType.StoredProcedure);
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
            return null;
        }
    }
}
