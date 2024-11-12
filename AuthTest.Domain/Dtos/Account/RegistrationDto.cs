

namespace AuthTest.Domain.Dtos.Account;

public class RegistrationDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int Role { get; set; }
}
