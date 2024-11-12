namespace AuthTest.Logic.Repository;

public class AccountRepo : IAccount
{
    private readonly IDbConnection _connection;
    private readonly AccountDbService service;
    private readonly IMapper _mapper;
    private AppSettings _appSettings;
    private readonly TokenSettings _jwtTokenConfig;

    private readonly byte[] _secret;
    private readonly static Logger _logger = NLog.LogManager.GetCurrentClassLogger();

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly string loggedUser;


    public AccountRepo(IDbConnection connection, IMapper mapper, IOptions<AppSettings> appSettings, IOptions<TokenSettings> jwtTokenConfig, IHttpContextAccessor httpContextAccessor)
    {
        _connection = connection;
        _mapper = mapper;
        service = new AccountDbService(connection);
        _appSettings = appSettings.Value;
        _jwtTokenConfig = jwtTokenConfig.Value;

        _secret = Encoding.ASCII.GetBytes(_jwtTokenConfig.Secret);

        _httpContextAccessor = httpContextAccessor;
        loggedUser = String.Empty;
        var identity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            loggedUser = identity?.Name;
        }
    }


    public async Task<ApiResponse<UserDto>> CreateRegistraton(RegistrationDto request)
    {
        var response = new ApiResponse<UserDto>();

        var validUser = await service.GetUser(request.UserName);
        if (validUser != null)
        {
            response.Code = "06";
            response.Description = "Record Already Exist";
            return response;
        }

        var result = await service.OnboardUser(request, loggedUser);
        if (result > 0)
        {
            response.Code = "00";
            response.Description = "Successful";
        }
        else
        {
            response.Code = "99";
            response.Description = "An error occured. Please try again later";
        }
        return response;
    }


    public async Task<ApiResponse<TokenDto>> Login(LoginDto request)
    {
        var response = new ApiResponse<TokenDto>();
        var tokenResult = new TokenDto();

        try
        {
            var user = await service.Authenticate(request.UserName);
            if (user == null)
            {
                response.Code = "06";
                response.Description = "User Record not Found";
                return response;
            }

            if (user.PasswordTrial >= _appSettings.MaximumPasswordTrial)
            {
                await service.UpdateFailedLogin(request.UserName, user.PasswordTrial, true);
                response.Code = "06";
                response.Description = "You have exceeded your password trial, kindly contact admin";
                return response;
            }

            //var pasw = BCrypt.Net.BCrypt.HashPassword(request.Password.Trim());

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                user.PasswordTrial++;
                await service.UpdateFailedLogin(request.UserName, user.PasswordTrial, false);

                int passwordTrialLeft = _appSettings.MaximumPasswordTrial - user.PasswordTrial;
                string attemptMsg = passwordTrialLeft <= 1 ? "attempt" : "attempts";

                response.Code = "06";
                response.Description = $"Invalid Login Details. You have {passwordTrialLeft} more {attemptMsg} ";
                return response;
            }

            if (user.PasswordTrial > 0)
                await service.UpdateFailedLogin(request.UserName, 0, false);


            var claims = GenerateClaims(user, "");
            var tokenObj = await GenerateToken(user, claims, DateTime.Now);
            var userViewModel = _mapper.Map<UserDto>(user);

            tokenResult.AccessToken = tokenObj.AccessToken;
            tokenResult.RefreshToken = tokenObj.RefreshToken.TokenString;
            tokenResult.ExpiresIn = tokenObj.ExpirationTime;
            tokenResult.TokenType = "Bearer";
            tokenResult.UserDetails = userViewModel;


            response.Code = "00";
            response.Description = "Successful";
            response.Data = tokenResult;

        }
        catch (Exception ex)
        {
            response.Code = "99";
            response.Description = "Ooops! Something went wrong, please try again later";
            _logger.Error(ex);
        }

        return response;
    }

    private async Task<JwtAuthResult> GenerateToken(User user, Claim[] claims, DateTime now)
    {
        var shouldAddAudienceClaim = string.IsNullOrEmpty(claims?
            .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Aud)?.Value);

        var jwtToken = new JwtSecurityToken(_jwtTokenConfig.Issuer,
            shouldAddAudienceClaim ? _jwtTokenConfig.Audience : string.Empty,
            claims,
            expires: now.AddMinutes(_jwtTokenConfig.AccessExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(_secret),
            SecurityAlgorithms.HmacSha256Signature));

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        var refreshedToken = GenerateRefreshTokenString();

        var refreshToken = new RefreshToken
        {
            Username = user.UserName,
            TokenString = refreshedToken,
            ExpiredAt = now.AddMinutes(_jwtTokenConfig.RefreshExpiration)
        };

        return new JwtAuthResult
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpirationTime = now.AddMinutes(_jwtTokenConfig.AccessExpiration)
        };

    }

    private static string GenerateRefreshTokenString()
    {
        var randomNumber = new byte[32];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private Claim[] GenerateClaims(User user, string roleId)
    {
        var claim = new[]
        {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, string.IsNullOrWhiteSpace(user.Email) ? user.UserName : user.Email),
                        new Claim("IssueDate", user.LastLoginDate.ToString()),
                        new Claim("RoleId", roleId),
                        new Claim("RequestingUserId", user.Id.ToString())
        };

        return claim;
    }
}
