using CSharpFunctionalExtensions;
using EmployeeMicroservice.ViewModels;
using Newtonsoft.Json;
using ProductionMicroService.Models;
using ProductionMicroService.ViewModels;
using StoreMicroService.ViewModels.Product;

namespace ProductionMicroService.Services
{
    public class HttpService
    {
        static readonly HttpClient client = new HttpClient();

        public static async Task<Result<List<GetProductViewModel>>> GetProducts()
        {
          var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"http://store-microservice:5001/api/Product/"));
          var response = await client.SendAsync(request);
          var content = await response.Content.ReadAsStringAsync();
          if (!response.IsSuccessStatusCode)
          {
            return Result.Failure<List<GetProductViewModel>>(content);
          }
          
          return Result.Success(JsonConvert.DeserializeObject<List<GetProductViewModel>>(content));
        }

        public static async Task<Result<List<EmployeeView>>> GetMachineWorkers()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri($"http://employee-microservice:5004/api/employees/operators"));
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
              return Result.Failure<List<EmployeeView>>(content);
            }

            return Result.Success(JsonConvert.DeserializeObject<List<EmployeeView>>(content));
        }

        public static async Task<Result<bool>> ReserveWood(List<ProductIdAndAmount> reservedWoodList)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri($"http://store-microservice:5001/api/Product/RemoveFromStore"));
            request.Content = JsonContent.Create(reservedWoodList);
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
              return Result.Failure<bool>(content);
            }

            return Result.Success(true);
        }

        public static async Task<Result<bool>> AddWood(List<ProductIdAndAmount> reservedWoodList)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri($"store-microservice/api/Product/AddToStore"));
            request.Content = new StringContent(JsonConvert.SerializeObject(reservedWoodList));
            var response = await client.SendAsync(request);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
              return Result.Failure<bool>(content);
            }

            return Result.Success(true);
        }
    }
}
