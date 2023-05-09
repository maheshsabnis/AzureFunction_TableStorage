using AzFuncTableOperation.Models;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;

namespace AzFuncTableOperation.Services
{
    public class TableOperations : ITableOperations
    {
        TableServiceClient serviceClient;
        string tableName = "Products";
        TableItem table;
        TableClient tableClient;

        ResponseObject<ProductEntity> response = new ResponseObject<ProductEntity>();
        
        public TableOperations()
        {
            serviceClient = new TableServiceClient("CONNECTION-STRING");

            #region Create Table If Not Exist and get the table client
              table = serviceClient.CreateTableIfNotExists(tableName);
            tableClient = serviceClient.GetTableClient(tableName);
            #endregion
        }

        async Task<ResponseObject<ProductEntity>> ITableOperations.CreateAsync(ProductEntity entity)
        {
            try
            {
                var result = await tableClient.AddEntityAsync<ProductEntity>(entity);
                response.Record = entity;
               
                response.StatusMessage = "Entiry is created successfully";
            }
            catch (Exception ex)
            {
                response.StatusMessage = ex.Message;
            }
            return response;
        }

        async Task<ResponseObject<ProductEntity>> ITableOperations.DeleteAsync(string partitionKey, string rowKey)
        {
            try
            {
                var result = await tableClient.DeleteEntityAsync(partitionKey, rowKey);
                response.StatusMessage = "Entiry is deleted successfully";
            }
            catch (Exception ex)
            {
                response.StatusMessage = ex.Message;
            }
            return response;
        }

        Task<ResponseObject<ProductEntity>> ITableOperations.GetAsync()
        {
            try
            {
                
                var queryResult = tableClient.Query<ProductEntity>();
                response.Records = queryResult;
                response.StatusMessage = "Products Data is Read Successfully";
                
            }
            catch (Exception ex)
            {
                response.StatusMessage = ex.Message;
            }
            return Task.FromResult(response);
        }

        Task<ResponseObject<ProductEntity>> ITableOperations.GetAsync(string partitionKey, string rowKey)
        {
            try
            {
                var queryResults = tableClient.GetEntity<ProductEntity>(partitionKey,rowKey);
                response.Record = queryResults.Value;
                response.StatusMessage = "Product Data is Read Successfully";
            }
            catch (Exception ex)
            {
                response.StatusMessage = ex.Message;
            }
            return Task.FromResult(response);
        }

       async Task<ResponseObject<ProductEntity>> ITableOperations.UpdateAsync(ProductEntity entity)
        {
            try
            {
                entity.PartitionKey = entity.Manufacturer;
                entity.RowKey = entity.ProductId.ToString();
                // get record for update based on PartitionKey and RowKey
                var queryResults = tableClient.GetEntity<ProductEntity>(entity.PartitionKey, entity.RowKey);
                var result = await tableClient.UpdateEntityAsync(entity, queryResults.Value.ETag);
               
                response.StatusMessage = "Product Entity is updated successfully";
            }
            catch (Exception ex)
            {
                response.StatusMessage = ex.Message;
            }
            return response;
        }
    }
}
