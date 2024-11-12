namespace AuthTest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class AccountController : BaseApiController
{
    private readonly IAccount _repo;
    private readonly IValidator<LoginDto> _loginValidator;

    public AccountController(IAccount repo, IValidator<LoginDto> loginValidator)
    {
        _repo = repo;
        _loginValidator = loginValidator;
    }


    [HttpPost]
    [Route("Onboard")]
    public async Task<ActionResult> CreateRegistration([FromBody] RegistrationDto request)
    {
        var res = await _repo.CreateRegistraton(request);
        if (res.Code.Equals("00"))
        {
            return Ok(res);
        }
        else if (res.Code.Equals("06"))
        {
            return Ok(res);
        }
        else
        {
            return StatusCode(500, new ErrorResponse() { Code = res.Code, description = res.Description });
        }
    }


    [HttpPost]
    [Route("Authenticate")]
    [AllowAnonymous]
    public async Task<ActionResult> Login([FromBody] LoginDto request)
    {

        FluentValidation.Results.ValidationResult validationResult = await _loginValidator.ValidateAsync(request);
        if (!validationResult.IsValid)
            return BadRequest(GetValidationErrors(validationResult.Errors));

        var res = await _repo.Login(request);
        if (res.Code.Equals("00"))
        {
            return Ok(res);
        }
        else if (res.Code.Equals("06"))
        {
            return BadRequest(res);
        }
        else
        {
            return StatusCode(500, new ErrorResponse() { Code = res.Code, description = res.Description });
        }
    }
}
