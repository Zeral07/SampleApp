namespace SampleApp.ViewModels
{
    public class ResultResponse<T> where T : class
    {
        public List<T> Result { get; set; } = [];
        public int Total { get; set; } = 0;
    }
}
