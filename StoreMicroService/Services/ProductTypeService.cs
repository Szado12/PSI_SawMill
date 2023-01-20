using CSharpFunctionalExtensions;
using StoreMicroService.Models;
using StoreMicroService.Services.Interfaces;

namespace StoreMicroService.Services
{
  public class ProductTypeService : BaseService,IProductTypeService
  {
    public Result<string> AddProductType(string productName)
    {
      bool exist = StoreContext.ProductTypes.Count(x => x.Name.ToLower() == productName.ToLower()) > 0;
      if (exist)
        return Result.Failure<string>("Product type with this name already exist");

      StoreContext.ProductTypes.Add(new ProductType() { Name = productName });
      StoreContext.SaveChanges();
      return Result.Success($"Product type {productName} added");
    }

    public Result<string> RemoveProductType(int productTypeId)
    {
      throw new NotImplementedException();
    }

    public Result<string> UpdateProductType(int productTypeId, string updatedName)
    {
      var productTypeToChange = StoreContext.ProductTypes.FirstOrDefault(x => x.ProductTypeId == productTypeId);
      if (productTypeToChange == null)
        return Result.Failure<string>($"Product type with id:{productTypeId} doesn't exist");

      productTypeToChange.Name = updatedName;
      StoreContext.SaveChanges();

      return Result.Success("Product type updated");
    }

    public Result<List<ProductType>> GetProductTypes()
    {
      return Result.Success(StoreContext.ProductTypes.ToList());
    }
  }
}
