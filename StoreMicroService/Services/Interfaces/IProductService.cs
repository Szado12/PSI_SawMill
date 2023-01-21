using CSharpFunctionalExtensions;
using StoreMicroService.ViewModels.Product;

namespace StoreMicroService.Services.Interfaces
{
  public interface IProductService
  {
    Result<string> AddProduct(AddProductViewModel product);
    Result<string> RemoveProduct(int productId);
    Result<string> UpdateProduct(UpdateProductViewModel product);
    Result<List<GetProductViewModel>> GetAllProducts();
  }
}
