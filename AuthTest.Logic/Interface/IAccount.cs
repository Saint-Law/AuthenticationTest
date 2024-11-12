namespace AuthTest.Logic.Interface;

public interface IAccount
{
    Task<ApiResponse<TokenDto>> Login(LoginDto request);
    Task<ApiResponse<UserDto>> CreateRegistraton(RegistrationDto request);
}
