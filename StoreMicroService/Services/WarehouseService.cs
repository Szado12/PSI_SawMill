using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using StoreMicroService.Models;
using StoreMicroService.Services.Interfaces;
using StoreMicroService.ViewModels.Address;
using StoreMicroService.ViewModels.Warehouse;

namespace StoreMicroService.Services
{
  public class WarehouseService : DefaultService,IWarehouseService
  {
    public Result<int> AddWarehouse(AddWarehouseViewModel addWarehouse)
    {
      try
      {
        Address newAddress = Mapper.Map<AddressViewModel, Address>(addWarehouse.Address);
        StoreContext.Addresses.Add(newAddress);
        StoreContext.SaveChanges();

        Warehouse newWarehouse = Mapper.Map<AddWarehouseViewModel, Warehouse>(addWarehouse);
        newWarehouse.AddressId = newAddress.AddressId;

        StoreContext.Warehouses.Add(newWarehouse);
        StoreContext.SaveChanges();
        return Result.Success(newWarehouse.WarehouseId);
      }
      catch (Exception e)
      {
        return Result.Failure<int>(e.Message);
      }
    }

    public Result<int> UpdateWarehouse(UpdateWarehouseViewModel updateWarehouse)
    {
      try
      {
        Warehouse? warehouseToBeUpdated = StoreContext.Warehouses
          .Include(x=> x.Address)
          .Include(x=> x.WarehousesToProducts)
          .FirstOrDefault(x=>x.WarehouseId == updateWarehouse.WarehouseId);

        if (warehouseToBeUpdated == null)
          throw new Exception($"Warehouse with id:{updateWarehouse.WarehouseId} doesn't exist");

        Address? addressToBeUpdated = warehouseToBeUpdated.Address;
        if (addressToBeUpdated == null)
          throw new Exception($"Address for warehouse with id:{updateWarehouse.WarehouseId} doesn't exist");

        if(updateWarehouse.StoredProducts.Sum(x=>x.Amount) > updateWarehouse.Capacity)
          throw new Exception($"Warehouse has maximum capacity of {updateWarehouse.Capacity}");

        addressToBeUpdated.City = updateWarehouse.Address.City;
        addressToBeUpdated.Street = updateWarehouse.Address.Street;
        addressToBeUpdated.PostalCode = updateWarehouse.Address.PostalCode;

        warehouseToBeUpdated.Name = updateWarehouse.Name;
        warehouseToBeUpdated.Capacity = updateWarehouse.Capacity;

        foreach (var storedProduct in updateWarehouse.StoredProducts)
        {
          var storedProductToBeUpdated =
            warehouseToBeUpdated.WarehousesToProducts.FirstOrDefault(x => x.ProductId == storedProduct.ProductId);

          if (storedProductToBeUpdated == null)
          {
            warehouseToBeUpdated.WarehousesToProducts.Add(new WarehousesToProduct()
            {
              Amount = storedProduct.Amount,
              ProductId = storedProduct.ProductId,
              WarehouseId = updateWarehouse.WarehouseId
            });
          }

          else
          {
            storedProductToBeUpdated.Amount = storedProduct.Amount;
            storedProductToBeUpdated.ProductId = storedProduct.ProductId;
            storedProductToBeUpdated.WarehouseId = updateWarehouse.WarehouseId;
          }
        }

        StoreContext.SaveChanges();
        return Result.Success(updateWarehouse.WarehouseId);

      }
      catch (Exception e)
      {
        return Result.Failure<int>(e.Message);
      }
    }

    public Result<List<WarehouseViewModel>> GetWarehouses()
    {
      try
      {
        return Result.Success(Mapper.Map<List<Warehouse>, List<WarehouseViewModel>>(StoreContext.Warehouses
          .Include(x => x.Address)
          .Include(x => x.WarehousesToProducts)
          .ToList()));
      }
      catch (Exception e)
      {
        return Result.Failure<List<WarehouseViewModel>>(e.Message);
      }
    }

    public Result<WarehouseDetailsViewModel> GetWarehouseDetails(int warehouseId)
    {
      try
      {
        var warehouse = StoreContext.Warehouses
          .Include(x => x.Address)
          .Include(x=>x.WarehousesToProducts)
          .ThenInclude(x=>x.Product)
          .ThenInclude(x=>x.WoodType)
          .Include(x => x.WarehousesToProducts)
          .ThenInclude(x => x.Product)
          .ThenInclude(x => x.ProductType)
          .FirstOrDefault(x => x.WarehouseId == warehouseId);
        if (warehouse == null)
          throw new Exception($"Warehouse with id:{warehouseId} doesn't exist");

        return Result.Success(Mapper.Map<Warehouse, WarehouseDetailsViewModel>(warehouse));
      }
      catch (Exception e)
      {
        return Result.Failure<WarehouseDetailsViewModel>(e.Message);
      }
    }

    public WarehouseService(StoreContext storeContext) : base(storeContext)
    {
    }
  }
}
