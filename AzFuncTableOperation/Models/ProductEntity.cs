using Azure;
using Azure.Data.Tables;

namespace AzFuncTableOperation.Models
{
    public class ProductEntity : ITableEntity
    {
        public string? PartitionKey { get ; set; }
        public string? RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Manufacturer { get; set; }
        public int Price { get; set; }
    }
}
