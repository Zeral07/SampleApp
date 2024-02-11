namespace SampleApp.ViewModels
{
    public class PaginationFilter<T> where T : class
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public T? Parameter { get; set; }
        public List<T> Result { get; set; } = [];
        public int Total { get; set; } = 0;
        public List<int> PageSizes { get; set; } = new List<int> { 10, 20, 30, 50 };
    }
}
