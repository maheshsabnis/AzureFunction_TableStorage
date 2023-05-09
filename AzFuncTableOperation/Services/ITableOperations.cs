using AzFuncTableOperation.Models;

namespace AzFuncTableOperation.Services
{
    public interface ITableOperations
    {
        Task<ResponseObject<ProductEntity>> GetAsync();
        Task<ResponseObject<ProductEntity>> GetAsync(string partitionKey, string rowKey);
        Task<ResponseObject<ProductEntity>> CreateAsync(ProductEntity entity);
        Task<ResponseObject<ProductEntity>> UpdateAsync(ProductEntity entity);
        Task<ResponseObject<ProductEntity>> DeleteAsync(string partitionKey, string rowKey);
    }
}
