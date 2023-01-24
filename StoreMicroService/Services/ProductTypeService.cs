using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreMicroService.Models;
using StoreMicroService.Services.Interfaces;
using StoreMicroService.ViewModels.Product;
using StoreMicroService.ViewModels.ProductType;
using StoreMicroService.ViewModels.WoodType;

namespace StoreMicroService.Services
{
  public class ProductTypeService : DefaultService,IProductTypeService
  {
    public ProductTypeService(StoreContext storeContext) : base(storeContext)
    {
    }

    public Result<int> AddProductType(string productName)
    {
      bool exist = StoreContext.ProductTypes.Count(x => x.Name.ToLower() == productName.ToLower()) > 0;
      if (exist)
        return Result.Failure<int>("Product type with this name already exist");

      var productType = new ProductType() {Name = productName};
      StoreContext.ProductTypes.Add(productType);
      StoreContext.SaveChanges();
      return Result.Success(productType.ProductTypeId);
    }

    public Result<int> RemoveProductType(int productTypeId)
    {
      throw new NotImplementedException();
    }

    public Result<int> UpdateProductType(ProductTypeModel productType)
    {
      var productTypeToChange = StoreContext.ProductTypes.FirstOrDefault(x => x.ProductTypeId == productType.ProductTypeId);
      if (productTypeToChange == null)
        return Result.Failure<int>($"Product type with id:{productType.ProductTypeId} doesn't exist");

      productTypeToChange.Name = productType.Name;
      StoreContext.SaveChanges();

      return Result.Success(productType.ProductTypeId);
    }

    public Result<List<ProductTypeModel>> GetProductTypes()
    {
      return Result.Success(Mapper.Map<List<ProductType>, List<ProductTypeModel>>(StoreContext.ProductTypes.ToList()));
    }
  }
}
