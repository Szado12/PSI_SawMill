using CSharpFunctionalExtensions;
using StoreMicroService.ViewModels.Product;

namespace StoreMicroService.Services.Interfaces
{
  public interface IProductService
  {
    Result<int> AddProduct(AddProductViewModel product);
    Result<int> RemoveProduct(int productId);
    Result<int> UpdateProduct(UpdateProductViewModel product);
    Result<List<GetProductViewModel>> GetAllProducts();
    Result<GetProductViewModel> GetProductById(int productId);
    Result<string> ReserveProductInStore(List<ProductIdAndAmount> reserveItemList);
    Result<string> AddToStore(List<ProductIdAndAmount> addItemList);
  }
}
