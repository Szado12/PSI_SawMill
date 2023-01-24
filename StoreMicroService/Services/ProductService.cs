using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreMicroService.Models;
using StoreMicroService.Services.Interfaces;
using StoreMicroService.ViewModels.Product;
using StoreMicroService.ViewModels.ProductType;

namespace StoreMicroService.Services
{
  public class ProductService : DefaultService, IProductService
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
      try
      {
        return Result.Success(Mapper.Map<List<Product>, List<GetProductViewModel>>(StoreContext.Products
          .Include(x=>x.WoodType)
          .Include(x=>x.ProductType)
          .Include(x=>x.WarehousesToProducts)
          .ToList()));
      }
      catch (Exception e)
      {
        return Result.Failure<List<GetProductViewModel>>(e.Message);
      }
    }

    public Result<List<ProductTypeModel>> GetProductTypes()
    {
      return Result.Success(Mapper.Map<List<ProductType>, List<ProductTypeModel>>(StoreContext.ProductTypes.ToList()));
    }

    public Result<string> ReserveProductInStore(List<ProductIdAndAmount> reserveItemList)
    {
      List<(Product, double)> productsToChange = new List<(Product, double)>();
      foreach (var product in reserveItemList)
      {
        var itemToBeChanged = StoreContext.Products.Include(x => x.WarehousesToProducts).FirstOrDefault(x => x.ProductId == product.ProductId);

        if (itemToBeChanged == null)
        {
          return Result.Failure<string>($"Product with id:{product.ProductId} doesn't exist");
        }

        var sum = itemToBeChanged.WarehousesToProducts.Sum(x => x.Amount);
        if (sum < product.Amount)
          return Result.Failure<string>($"Insufficient product amount:{product.Amount}, available {sum}");

        productsToChange.Add((itemToBeChanged, product.Amount));
      }

      foreach (var storedProductToChange in productsToChange)
      {
        var amount = storedProductToChange.Item2;
        foreach (var storedProduct in storedProductToChange.Item1.WarehousesToProducts)
        {
          if (storedProduct.Amount > amount)
          {
            storedProduct.Amount -= amount;
            break;
          }
          amount -= storedProduct.Amount;
          storedProduct.Amount = 0;

        }
      }

      return Result.Success<string>("Reserved");
    }

    public Result<string> AddToStore(List<ProductIdAndAmount> addItemList)
    {
      List<(Warehouse, double)> warehousesToBeUpdated = new List<(Warehouse, double)>();
      foreach (var product in addItemList)
      {
        var itemToBeChanged = StoreContext.Products.FirstOrDefault(x => x.ProductId == product.ProductId);

        if (itemToBeChanged == null)
        {
          return Result.Failure<string>($"Product with id:{product.ProductId} doesn't exist");
        }
      }

      foreach (var warehouse in StoreContext.Warehouses.Include(x => x.WarehousesToProducts))
      {
        warehousesToBeUpdated.Add((warehouse, warehouse.Capacity - warehouse.WarehousesToProducts.Sum(y=>y.Amount)));
      }

      if(addItemList.Sum(x=>x.Amount) > warehousesToBeUpdated.Sum(y=>y.Item2))
        return Result.Failure<string>($"Not enough space");

      warehousesToBeUpdated = warehousesToBeUpdated.OrderByDescending(z => z.Item2).ToList();
      
      foreach (var warehouse in warehousesToBeUpdated)
      {
        var availableSpace = warehouse.Item2;
        foreach (var product in addItemList)
        {
          if(product.Amount <= 0)
            continue;
          
          if (availableSpace <= product.Amount)
          {
            product.Amount -= availableSpace;
            warehouse.Item1.WarehousesToProducts.Add(new WarehousesToProduct
            {
              Amount = availableSpace,
              ProductId = product.ProductId,
              WarehouseId = warehouse.Item1.WarehouseId
            });
            availableSpace = 0;
            break;
          }

          availableSpace -= product.Amount;
          warehouse.Item1.WarehousesToProducts.Add(new WarehousesToProduct
          {
            Amount = product.Amount, 
            ProductId = product.ProductId,
            WarehouseId = warehouse.Item1.WarehouseId
          });
          product.Amount = 0;
          
        }

        if (availableSpace > 0)
        {
          break;
        }
      }
      StoreContext.SaveChanges();
      return Result.Success<string>("Stored");
    }

    public ProductService(StoreContext storeContext) : base(storeContext)
    {
    }
  }
}
