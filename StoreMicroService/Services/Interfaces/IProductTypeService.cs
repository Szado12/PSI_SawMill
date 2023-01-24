using CSharpFunctionalExtensions;
using StoreMicroService.Models;
using StoreMicroService.ViewModels.Product;
using StoreMicroService.ViewModels.ProductType;

namespace StoreMicroService.Services.Interfaces
{
  public interface IProductTypeService
  {
    Result<int> AddProductType(string productName);
    Result<int> RemoveProductType(int productTypeId);
    Result<int> UpdateProductType(ProductTypeModel productType);
    Result<List<ProductTypeModel>> GetProductTypes();
  }
}
