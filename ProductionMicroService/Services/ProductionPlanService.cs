using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using ProductionMicroService.Models;
using ProductionMicroService.Services.Interfaces;
using ProductionMicroService.ViewModels.ProductionPlan;

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

    public Result<int> AddProductionPlan(AddProductionPlan addProductionPlan)
    {
      //check if Product exist
      //check if product amount is sufficient

      var operation = _operationService.GetOperationById(addProductionPlan.OperationId);
      if (operation.IsFailure)
      {
        return Result.Failure<int>(operation.Error);
      }
      
      var machine = _machineService.GetMachineById(addProductionPlan.MachineId);
      if (machine.IsFailure)
      {
        return Result.Failure<int>(machine.Error);
      }

      if (machine.Value.AllowedOperations.Count(x => x.OperationId == operation.Value.OperationId) <= 0)
        return Result.Failure<int>(
          $"Machine with id:{addProductionPlan.MachineId} is not compatible with operation {addProductionPlan.OperationId}");


      //check if employee is machineWorker

      //reserve Product amount

      var productionPlanToBeAdded = Mapper.Map<AddProductionPlan, ProductionDetail>(addProductionPlan);
      productionPlanToBeAdded.Status = (int) ProductionPlanStatus.Created;
      ProductionContext.ProductionDetails.Add(productionPlanToBeAdded);
      ProductionContext.SaveChanges();
      return Result.Success(productionPlanToBeAdded.ProductionDetailId);
    }

    public Result<int> UpdateProductionPlan(UpdateProductionPlanViewModel productionPlanToBeUpdated)
    {
      throw new NotImplementedException();
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
            ProductionContext.ProductionDetails.Where(x => x.IsArchived == false).ToList()));
      }
      catch (Exception e)
      {
        return Result.Failure<List<GetProductionPlanViewModel>>(e.Message);
      }
    }

    public Result<GetProductionPlanViewModel> GetProductionPlanById(int productionPlanId)
    {
      var productionPlanToBeArchived =
        ProductionContext.ProductionDetails.FirstOrDefault(x => x.OperationId == productionPlanId && x.IsArchived == false);
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
            ProductionContext.ProductionDetails.Where(x => x.IsArchived == false && x.EmployeeId == employeeId).ToList()));
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
            ProductionContext.ProductionDetails.Where(x => x.IsArchived == false && x.MachineId == machineId).ToList()));
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
            ProductionContext.ProductionDetails.Where(x => x.IsArchived == false && x.OperationId == operationId).ToList()));
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
            ProductionContext.ProductionDetails.Where(x => x.IsArchived == false && x.Status == stateId).ToList()));
      }
      catch (Exception e)
      {
        return Result.Failure<List<GetProductionPlanViewModel>>(e.Message);
      }
    }

    public Result<int> UpdateState(int productionPlanId)
    {
      var productionPlan =
        ProductionContext.ProductionDetails.FirstOrDefault(x => x.OperationId == productionPlanId);
      if (productionPlan == null)
        return Result.Failure<int>($"Production plan with id:{productionPlanId} doesn't exist");
      if ((ProductionPlanStatus) productionPlan.Status == ProductionPlanStatus.Completed)
        return Result.Failure<int>($"Production plan with id is completed");
      productionPlan.Status++;
      if ((ProductionPlanStatus) productionPlan.Status == ProductionPlanStatus.Completed)
        //call woodStore To Add outputProduct;
      ProductionContext.SaveChanges();
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
