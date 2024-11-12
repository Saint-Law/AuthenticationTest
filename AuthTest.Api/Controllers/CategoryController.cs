namespace AuthTest.Api.Controllers;

public class CategoryController : ControllerBase
{
    private readonly ICategory _repo;

    public CategoryController(ICategory repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<object> GetCategories(int pageNumber, int pageSize)
    {
        var res = await _repo.GetCategories(pageNumber, pageSize);
        if (res.Code.Equals("00"))
        {
            return Ok(res);
        }
        else if (res.Code.Equals("01"))
        {
            return NotFound(res);
        }
        else
        {
            return StatusCode(500, new ErrorResponse() { Code = res.Code, description = res.Description });
        }
    }


    [HttpGet]
    public async Task<object> GetSingleCategory(int Id)
    {
        var res = await _repo.GetSingleCategory(Id);
        if (res.Code.Equals("00"))
        {
            return Ok(res);
        }
        else if (res.Code.Equals("01"))
        {
            return NotFound(res);
        }
        else
        {
            return StatusCode(500, new ErrorResponse() { Code = res.Code, description = res.Description });
        }
    }


    [HttpPost]
    public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDto request)
    {
        var res = await _repo.CreateCategory(request);
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

    [HttpPut]
    public async Task<ActionResult> UpdateCategory([FromBody] UpdateCategoryDto request)
    {
        var res = await _repo.UpdateCategory(request);
        if (res.Code.Equals("00"))
        {
            return Ok(res);
        }
        else
        {
            return StatusCode(500, new ErrorResponse() { Code = res.Code, description = res.Description });
        }
    }
}