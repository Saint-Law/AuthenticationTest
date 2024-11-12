

namespace AuthTest.Domain.Dtos.Account;

public class TokenDto
{
    /// <summary>
    /// Gets or sets the access token.
    /// </summary>
    /// <value>The access token.</value>
    public string AccessToken { get; set; }
    /// <summary>
    /// Gets or sets the type of the token.
    /// </summary>
    /// <value>The type of the token.</value>
    public string TokenType { get; set; }
    /// <summary>
    /// Gets or sets the expires in.
    /// </summary>
    /// <value>The expires in.</value>
    public DateTime ExpiresIn { get; set; }
    /// <summary>
    /// Gets or sets the refresh token.
    /// </summary>
    /// <value>The refresh token.</value>
    public string RefreshToken { get; set; }
    /// <summary>
    /// Gets or sets the change password token
    /// </summary>
    /// <value>The Change password token</value>
    public UserDto UserDetails { get; set; }
}
