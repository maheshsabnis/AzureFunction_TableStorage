namespace AzFuncTableOperation.Models
{
    public class ResponseObject<T>
    {
        public IEnumerable<T>? Records { get; set; }
        public T? Record { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; } = string.Empty;
    }
}
