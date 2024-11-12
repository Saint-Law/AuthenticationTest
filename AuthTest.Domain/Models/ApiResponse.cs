namespace AuthTest.Domain.Models;

public class ApiResponse<T> where T : class
{
    public string Description { get; set; }
    public string Code { get; set; }
    public T Data { get; set; }
}

public class ApiResponse2<T> where T : class
{
    public string Description { get; set; }
    public string Code { get; set; }
    public string Data { get; set; }
}
