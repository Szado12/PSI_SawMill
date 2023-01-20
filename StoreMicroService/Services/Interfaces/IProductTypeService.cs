using CSharpFunctionalExtensions;
using StoreMicroService.Models;

namespace StoreMicroService.Services.Interfaces
{
  public interface IProductTypeService
  {
    Result<string> AddProductType(string productName);
    Result<string> RemoveProductType(int productTypeId);
    Result<string> UpdateProductType(int productTypeId, string updatedName);
    Result<List<ProductType>> GetProductTypes();
  }
}
