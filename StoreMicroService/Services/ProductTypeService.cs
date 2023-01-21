using CSharpFunctionalExtensions;
using StoreMicroService.Models;
using StoreMicroService.Services.Interfaces;
using StoreMicroService.ViewModels.ProductType;
using StoreMicroService.ViewModels.WoodType;

namespace StoreMicroService.Services
{
  public class ProductTypeService : DefaultService,IProductTypeService
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

    public Result<string> UpdateProductType(ProductTypeModel productType)
    {
      var productTypeToChange = StoreContext.ProductTypes.FirstOrDefault(x => x.ProductTypeId == productType.ProductTypeId);
      if (productTypeToChange == null)
        return Result.Failure<string>($"Product type with id:{productType.ProductTypeId} doesn't exist");

      productTypeToChange.Name = productType.Name;
      StoreContext.SaveChanges();

      return Result.Success("Product type updated");
    }

    public Result<List<ProductTypeModel>> GetProductTypes()
    {
      return Result.Success(Mapper.Map<List<ProductType>, List<ProductTypeModel>>(StoreContext.ProductTypes.ToList()));
    }

    public ProductTypeService(StoreContext storeContext) : base(storeContext)
    {
    }
  }
}
