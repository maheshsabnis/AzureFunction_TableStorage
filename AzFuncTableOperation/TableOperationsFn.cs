using AzFuncTableOperation.Models;
using AzFuncTableOperation.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Net;
using System.Text.Json;

namespace AzFuncTableOperation
{
    public class TableOperationsFn
    {
        ITableOperations tableOperations;
        HttpResponseData response;
        public TableOperationsFn(ITableOperations operations )
        {
            tableOperations = operations;
        }

        

        [Function("Get")]
        public async Task<HttpResponseData> Get([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Products")] HttpRequestData req)
        {

            try
            {

                  response = req.CreateResponse(HttpStatusCode.OK);
                //  response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
                var responseData = await tableOperations.GetAsync();
                await response.WriteAsJsonAsync<ResponseObject<ProductEntity>>(responseData);

                return response;
            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(ex.Message);
            }
            return response;
        }

        [Function("Post")]
        public async Task<HttpResponseData> Post([HttpTrigger(AuthorizationLevel.Function, "post", Route ="Products")] HttpRequestData req)
        {

            try
            {
                response = req.CreateResponse(HttpStatusCode.OK);
                ProductEntity? product = JsonSerializer.Deserialize<ProductEntity>( new StreamReader(req.Body).ReadToEnd());
                product.PartitionKey = product.Manufacturer;
                product.RowKey = product.ProductId.ToString();
                

                var responseData = await tableOperations.CreateAsync(product);
                await response.WriteAsJsonAsync<ResponseObject<ProductEntity>>(responseData);

                return response;
            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(ex.Message);
            }
            return response;
        }


        [Function("GetSingle")]
        public async Task<HttpResponseData> GetSingle([HttpTrigger(AuthorizationLevel.Function, "get", Route = "Products/{partitionkey}/{rowkey}")] HttpRequestData req, string? partitionkey, string? rowkey)
        {

            try
            {
                response = req.CreateResponse(HttpStatusCode.OK);
                
                var responseData = await tableOperations.GetAsync(partitionkey,rowkey);
                await response.WriteAsJsonAsync<ResponseObject<ProductEntity>>(responseData);

                return response;
            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(ex.Message);
            }
            return response;
        }

        [Function("Put")]
        public async Task<HttpResponseData> Put([HttpTrigger(AuthorizationLevel.Function, "put", Route = "Products")] HttpRequestData req)
        {

            try
            {
                response = req.CreateResponse(HttpStatusCode.OK);
                ProductEntity? product = JsonSerializer.Deserialize<ProductEntity>(new StreamReader(req.Body).ReadToEnd());

                var responseData = await tableOperations.UpdateAsync(product);
                await response.WriteAsJsonAsync<ResponseObject<ProductEntity>>(responseData);

                return response;
            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(ex.Message);
            }
            return response;
        }

        [Function("Delete")]
        public async Task<HttpResponseData> Delete([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "Products/{partitionkey}/{rowkey}")] HttpRequestData req, string? partitionkey, string? rowkey)
        {

            try
            {
                response = req.CreateResponse(HttpStatusCode.OK);

                var responseData = await tableOperations.DeleteAsync(partitionkey, rowkey);
                await response.WriteAsJsonAsync<ResponseObject<ProductEntity>>(responseData);

                return response;
            }
            catch (Exception ex)
            {
                await response.WriteAsJsonAsync(ex.Message);
            }
            return response;
        }
    }
}
