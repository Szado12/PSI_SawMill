using CSharpFunctionalExtensions;
using EmployeeMicroservice.ViewModels;
using Microsoft.EntityFrameworkCore;
using ProductionMicroService.Models;
using ProductionMicroService.Services.Interfaces;
using ProductionMicroService.ViewModels;
using ProductionMicroService.ViewModels.ProductionPlan;
using StoreMicroService.ViewModels.Product;

namespace ProductionMicroService.Services
{
  public class ProductionPlanService : DefaultService,IProductionPlanService
  {
    private IMachineService _machineService;
    private IOperationService _operationService;
    public ProductionPlanService(ProductionContext productionContext, IMachineService machineService, IOperationService operationService) : base(productionContext)
    {
      _machineService = machineService;
      _operationService = operationService;
    }

    public async Task<Result<int>> AddProductionPlan(AddProductionPlan addProductionPlan)
    {
      Result<List<GetProductViewModel>> products = await HttpService.GetProducts();

      if (products.IsFailure)
      {
        return Result.Failure<int>(products.Error);
      }

      if (products.Value.Count(x => x.ProductId == addProductionPlan.ProductId) == 0)
        return Result.Failure<int>($"Product with id:{addProductionPlan.ProductId} doesn't exist");

      GetProductViewModel product = products.Value.First(x => x.ProductId == addProductionPlan.ProductId);

      if (product.AvailableAmount < addProductionPlan.MaterialAmount)
      {
        return Result.Failure<int>($"Insufficient product amount in stores");
      }

      var operation = _operationService.GetOperationById(addProductionPlan.OperationId);
      if (operation.IsFailure)
      {
        return Result.Failure<int>(operation.Error);
      }
      
      if (products.Value.Count(x => x.WoodTypeId == product.WoodTypeId && x.ProductTypeId == operation.Value.OutputProductTypeId) == 0)
        return Result.Failure<int>($"Output product doesn't exist");
      
      var machine = _machineService.GetMachineById(addProductionPlan.MachineId);
      if (machine.IsFailure)
      {
        return Result.Failure<int>(machine.Error);
      }

      if (machine.Value.AllowedOperations.Count(x => x.OperationId == operation.Value.OperationId) <= 0)
        return Result.Failure<int>(
          $"Machine with id:{addProductionPlan.MachineId} is not compatible with operation {addProductionPlan.OperationId}");


      var productionPlansForMachine =
        ProductionContext.ProductionDetails
          .Where(x => x.MachineId == addProductionPlan.MachineId && !x.IsArchived)
          .Include(x => x.Operation);

      var isFree = true;
      ProductionDetail productionDetail = null;
      DateTime startTime = DateTime.Now;
      DateTime endTime = DateTime.Now;

      foreach (var productionPlan in productionPlansForMachine)
      {
        startTime = productionPlan.StartDate;
        endTime =
          productionPlan.StartDate.AddHours(productionPlan.MaterialAmount * productionPlan.Operation.Duration);
        if (startTime <= addProductionPlan.StartDate
            && endTime > addProductionPlan.StartDate)
        {
          isFree = false;
          productionDetail = productionPlan;
          break;
        }
      }

      if (!isFree)
      {
        return Result.Failure<int>(
          $"Machine is engaged by operation:{productionDetail.ProductionDetailId} in time {startTime} - {endTime}");
      }

      var productionPlansForEmployee =
        ProductionContext.ProductionDetails
          .Where(x => x.EmployeeId == addProductionPlan.EmployeeId && !x.IsArchived)
          .Include(x=>x.Operation);

      foreach (var productionPlan in productionPlansForEmployee)
      {
        startTime = productionPlan.StartDate;
        endTime =
          productionPlan.StartDate.AddHours(productionPlan.MaterialAmount * productionPlan.Operation.Duration);
        if (startTime <= addProductionPlan.StartDate
            && endTime > addProductionPlan.StartDate)
        {
          isFree = false;
          productionDetail = productionPlan;
          break;
        }
      }

      if (!isFree)
      {
        return Result.Failure<int>(
          $"Employee is engaged by operation:{productionDetail.ProductionDetailId} in time {startTime} - {endTime}");
      }

      Result<List<EmployeeView>> machineWorkers = await HttpService.GetMachineWorkers();

      if (machineWorkers.IsFailure)
        return Result.Failure<int>(machineWorkers.Error);
      
      if(machineWorkers.Value.Count(x=>x.EmployeeId == addProductionPlan.EmployeeId) == 0)
        return Result.Failure<int>($"Employe with id:{addProductionPlan.EmployeeId} is not a machine operator");

      List<ProductIdAndAmount> reservedWoodList = new List<ProductIdAndAmount>
      {
        new() {Amount = addProductionPlan.MaterialAmount, ProductId = addProductionPlan.ProductId}
      };
      Result<bool> reservedWoodResult = await HttpService.ReserveWood(reservedWoodList);

      if (reservedWoodResult.IsFailure)
        return Result.Failure<int>(reservedWoodResult.Error);

      var productionPlanToBeAdded = Mapper.Map<AddProductionPlan, ProductionDetail>(addProductionPlan);
      productionPlanToBeAdded.Status = (int) ProductionPlanStatus.Created;
      ProductionContext.ProductionDetails.Add(productionPlanToBeAdded);
      await ProductionContext.SaveChangesAsync();
      return Result.Success(productionPlanToBeAdded.ProductionDetailId);
    }
    public Result<int> DeleteProductionPlan(int productionPlanId)
    {
      var productionPlanToBeArchived =
        ProductionContext.ProductionDetails.FirstOrDefault(x => x.OperationId == productionPlanId);
      if(productionPlanToBeArchived == null)
        return Result.Failure<int>($"Production plan with id:{productionPlanId} doesn't exist");
      if((ProductionPlanStatus)productionPlanToBeArchived.Status == ProductionPlanStatus.Created || (ProductionPlanStatus)productionPlanToBeArchived.Status == ProductionPlanStatus.InProgress)
        return Result.Failure<int>($"Production plan with id:{productionPlanId} is in status: '{((ProductionPlanStatus)productionPlanToBeArchived.Status).EnumToString()}'");
      productionPlanToBeArchived.IsArchived = true;
      return Result.Success(productionPlanId);
    }

    public Result<List<GetProductionPlanViewModel>> GetProductionPlans()
    {
      try
      {
        return Result.Success(
          Mapper.Map<List<ProductionDetail>, List<GetProductionPlanViewModel>>(
            ProductionContext.ProductionDetails
              .Where(x => !x.IsArchived)
              .ToList()));
      }
      catch (Exception e)
      {
        return Result.Failure<List<GetProductionPlanViewModel>>(e.Message);
      }
    }

    public Result<GetProductionPlanViewModel> GetProductionPlanById(int productionPlanId)
    {
      var productionPlanToBeArchived =
        ProductionContext.ProductionDetails.FirstOrDefault(x => x.OperationId == productionPlanId && !x.IsArchived);
      if (productionPlanToBeArchived == null)
        return Result.Failure<GetProductionPlanViewModel>($"Production plan with id:{productionPlanId} doesn't exist");
      return Result.Success(Mapper.Map<ProductionDetail, GetProductionPlanViewModel>(productionPlanToBeArchived));
    }

    public Result<List<GetProductionPlanViewModel>> GetProductionPlanByEmployee(int employeeId)
    {
      try
      {
        return Result.Success(
          Mapper.Map<List<ProductionDetail>, List<GetProductionPlanViewModel>>(
            ProductionContext.ProductionDetails
              .Where(x => !x.IsArchived && x.EmployeeId == employeeId)
              .ToList()));
      }
      catch (Exception e)
      {
        return Result.Failure<List<GetProductionPlanViewModel>>(e.Message);
      }
    }

    public Result<List<GetProductionPlanViewModel>> GetProductionPlanByMachine(int machineId)
    {
      try
      {
        return Result.Success(
          Mapper.Map<List<ProductionDetail>, List<GetProductionPlanViewModel>>(
            ProductionContext.ProductionDetails
              .Where(x => !x.IsArchived && x.MachineId == machineId)
              .ToList()));
      }
      catch (Exception e)
      {
        return Result.Failure<List<GetProductionPlanViewModel>>(e.Message);
      }
    }

    public Result<List<GetProductionPlanViewModel>> GetProductionPlanByOperation(int operationId)
    {
      try
      {
        return Result.Success(
          Mapper.Map<List<ProductionDetail>, List<GetProductionPlanViewModel>>(
            ProductionContext.ProductionDetails
              .Where(x => !x.IsArchived && x.OperationId == operationId)
              .ToList()));
      }
      catch (Exception e)
      {
        return Result.Failure<List<GetProductionPlanViewModel>>(e.Message);
      }
    }

    public Result<List<GetProductionPlanViewModel>> GetProductionPlanByState(int stateId)
    {
      try
      {
        return Result.Success(
          Mapper.Map<List<ProductionDetail>, List<GetProductionPlanViewModel>>(
            ProductionContext.ProductionDetails
              .Where(x => !x.IsArchived && x.Status == stateId)
              .ToList()));
      }
      catch (Exception e)
      {
        return Result.Failure<List<GetProductionPlanViewModel>>(e.Message);
      }
    }

    public async Task<Result<int>> UpdateState(int productionPlanId)
    {
      var productionPlan =
        ProductionContext.ProductionDetails.FirstOrDefault(x => x.OperationId == productionPlanId);
      if (productionPlan == null)
        return Result.Failure<int>($"Production plan with id:{productionPlanId} doesn't exist");
      if ((ProductionPlanStatus) productionPlan.Status == ProductionPlanStatus.Completed)
        return Result.Failure<int>($"Production plan with id is completed");
      productionPlan.Status++;
      if ((ProductionPlanStatus) productionPlan.Status == ProductionPlanStatus.Completed)
      {
        Result<List<GetProductViewModel>> products = await HttpService.GetProducts();
        if (products.IsFailure)
        {
          return Result.Failure<int>(products.Error);
        }
        
        var operation = _operationService.GetOperationById(productionPlan.OperationId);
        if (operation.IsFailure)
        {
          return Result.Failure<int>(operation.Error);
        }

        var product = products.Value.First(x => x.ProductId == productionPlan.ProductId);

        if (products.Value.Count(x => x.WoodTypeId == product.WoodTypeId && x.ProductTypeId == operation.Value.OutputProductTypeId) == 0)
          return Result.Failure<int>($"Output product doesn't exist");
        
        var output = products.Value.First(x =>
          x.WoodTypeId == product.WoodTypeId && x.ProductTypeId == operation.Value.OutputProductTypeId);

        List<ProductIdAndAmount> reservedWoodList = new List<ProductIdAndAmount>
        {
          new() {Amount = productionPlan.MaterialAmount, ProductId = output.ProductId}
        };
        Result<bool> reservedWoodResult = await HttpService.AddWood(reservedWoodList);

        if (reservedWoodResult.IsFailure)
          return Result.Failure<int>(reservedWoodResult.Error);
      } 

      await ProductionContext.SaveChangesAsync();
      return Result.Success(productionPlanId);
    }

    public Result<int> UpdateStateError(int productionPlanId)
    {
      var productionPlan =
        ProductionContext.ProductionDetails.FirstOrDefault(x => x.OperationId == productionPlanId);
      if (productionPlan == null)
        return Result.Failure<int>($"Production plan with id:{productionPlanId} doesn't exist");
      if ((ProductionPlanStatus)productionPlan.Status == ProductionPlanStatus.Completed)
        return Result.Failure<int>($"Production plan with id is completed");
      productionPlan.Status = (int)ProductionPlanStatus.Failed;
      ProductionContext.SaveChanges();
      return Result.Success(productionPlanId);
    }

    public Result<List<bool>> CheckAvailability()
    {
      throw new NotImplementedException();
    }
  }
}
