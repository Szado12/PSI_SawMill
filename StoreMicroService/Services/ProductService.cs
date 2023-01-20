using CSharpFunctionalExtensions;
using StoreMicroService.Models;
using StoreMicroService.Services.Interfaces;
using StoreMicroService.ViewModels.Product;

namespace StoreMicroService.Services
{
  public class ProductService : BaseService, IProductService
  {
    public Result<string> AddProduct(AddProductViewModel product)
    {
      var exist = StoreContext.Products.FirstOrDefault(x => x.WoodTypeId == product.WoodTypeId && x.ProductTypeId == product.ProductTypeId && Math.Abs(x.Price - product.Price) < 0.001);
      if (exist != null)
        return Result.Failure<string>($"Product with this specification already exist id:{exist.ProductId}");

      StoreContext.Products.Add(new Product
      {
        WoodTypeId = product.WoodTypeId,
        ProductTypeId = product.ProductTypeId,
        Price = product.Price
      });
      StoreContext.SaveChanges();
      return Result.Success($"Product added");
    }

    public Result<string> RemoveProduct(int productId)
    {
      throw new NotImplementedException();
    }

    public Result<string> UpdateProduct(UpdateProductViewModel product)
    {
      var productToUpdate = StoreContext.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
      if (productToUpdate == null)
        return Result.Failure<string>($"Product with id:{product.ProductId} doesn't exist");

      productToUpdate.ProductTypeId = product.ProductTypeId;
      productToUpdate.WoodTypeId = product.WoodTypeId;
      productToUpdate.Price= product.Price;
      StoreContext.SaveChanges();
      return Result.Success($"Product id:{productToUpdate.ProductId} updated");
    }

    public Result<List<GetProductViewModel>> GetAllProducts()
    {
      throw new NotImplementedException();
    }
  }
}
