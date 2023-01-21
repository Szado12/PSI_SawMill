using CSharpFunctionalExtensions;
using StoreMicroService.Models;
using StoreMicroService.ViewModels.ProductType;

namespace StoreMicroService.Services.Interfaces
{
  public interface IProductTypeService
  {
    Result<string> AddProductType(string productName);
    Result<string> RemoveProductType(int productTypeId);
    Result<string> UpdateProductType(ProductTypeModel productType);
    Result<List<ProductTypeModel>> GetProductTypes();
  }
}
