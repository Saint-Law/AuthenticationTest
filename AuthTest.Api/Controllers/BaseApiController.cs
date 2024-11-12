namespace AuthTest.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
    protected string GetValidationErrors(List<FluentValidation.Results.ValidationFailure> failures)
    {
        string errors = string.Empty;
        errors = string.Join(",", failures.Select(x => x.ErrorMessage));

        return errors;
    }
}