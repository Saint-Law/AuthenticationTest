namespace AuthTest.Domain.Models;

    public class ApiListResponse<T> where T : class
    {
        public string Description { get; set; } 
        public string Code { get; set; }
        public IEnumerable<T> Data { get; set; }
    }

    public class ApiListResponse2<T> where T : class
    {
        public string Description { get; set; }
        public string Code { get; set; }
        public string Data { get; set; }
    }

    public class ApiListResponse3<T> where T : class
    {
        public string Description { get; set; }
        public string Code { get; set; }
        public string PageInfo { get; set; }
        public PagedList<T> Data { get; set; }
    }
