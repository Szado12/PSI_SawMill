using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using ProductionMicroService.Models;
using ProductionMicroService.Services.Interfaces;
using ProductionMicroService.ViewModels.Operation;

namespace ProductionMicroService.Services
{
  public class OperationService : DefaultService, IOperationService
  {
    public OperationService(ProductionContext productionContext) : base(productionContext)
    {
    }

    public Result<int> AddOperation(AddOperationViewModel addOperationViewModel)
    {
      try
      {
        if(addOperationViewModel.SourceProductTypeId == addOperationViewModel.OutputProductTypeId)
          return Result.Failure<int>("Source product type and output product the are the same");

        if (addOperationViewModel.SourceOutputRatio <= 0)
          return Result.Failure<int>("Source to output ratio can not be 0 or less");

        if (addOperationViewModel.Duration <= 0)
          return Result.Failure<int>("Duration can not be 0 or less");

        var addOperation = Mapper.Map<AddOperationViewModel, Operation>(addOperationViewModel);
        addOperation.IsArchived = false;
        ProductionContext.Operations.Add(addOperation);
        ProductionContext.SaveChanges();
        return Result.Success(addOperation.OperationId);
      }
      catch (Exception e)
      {
        return Result.Failure<int>(e.Message);
      }
    }

    public Result<int> UpdateOperation(UpdateOperationViewModel operationViewModel)
    {
      try
      {
        var updateOperation = ProductionContext.Operations.FirstOrDefault(o => o.OperationId == operationViewModel.OperationId);
        if (updateOperation == null)
          return Result.Failure<int>($"Operation with id:{operationViewModel.OperationId} doesn't exist");

        var updatedOperation = Mapper.Map<UpdateOperationViewModel, Operation>(operationViewModel);
        ProductionContext.Entry(updatedOperation).CurrentValues.SetValues(updatedOperation);
        ProductionContext.SaveChanges();
        return Result.Success(updateOperation.OperationId);
      }
      catch (Exception e)
      {
        return Result.Failure<int>(e.Message);
      }
    }

    public Result<int> DeleteOperation(int operationId)
    {
      try
      {
        var deleteOperation = ProductionContext.Operations
          .Include(o=>o.ProductionDetails)
          .FirstOrDefault(o => o.OperationId == operationId);
        if (deleteOperation == null)
          return Result.Failure<int>($"Operation with id:{deleteOperation.OperationId} doesn't exist");
        if (deleteOperation.ProductionDetails.Count(x => !x.IsArchived) > 0)
        {
          return Result.Failure<int>($"Operation with id:{deleteOperation.OperationId} has active production plans");
        }

        deleteOperation.IsArchived = true;
        ProductionContext.SaveChanges();
        return Result.Success(operationId);
      }
      catch (Exception e)
      {
        return Result.Failure<int>(e.Message);
      }
    }

    public Result<List<GetOperationViewModel>> GetAllOperations()
    {
      try
      {
        return Result.Success(Mapper.Map<List<Operation>, List<GetOperationViewModel>>(ProductionContext.Operations.Where(x => !x.IsArchived).ToList()));
      }
      catch (Exception e)
      {
        return Result.Failure<List<GetOperationViewModel>>(e.Message);
      }
    }

    public Result<GetDetailsOperationViewModel> GetOperationById(int operationId)
    {
      try
      {
        return Mapper.Map<Operation, GetDetailsOperationViewModel> (
          ProductionContext.Operations
            .Where(x => !x.IsArchived && x.OperationId == operationId)
            .Include(x=>x.SourceProductType)
            .Include(x=>x.OutputProductType)
            .First()
        );
      }
      catch (Exception e)
      {
        return Result.Failure<GetDetailsOperationViewModel>(e.Message);
      }
    }

    public Result<List<GetOperationViewModel>> GetAllOperationsByMachine(int machineId)
    {
      try
      {
        return Mapper.Map<List<Operation>, List<GetOperationViewModel>>(
          ProductionContext.OperationsToMachines
            .Where(x => x.MachineId == machineId)
            .Include(mto => mto.Operation)
            .Select(x => x.Operation)
            .ToList());
      }
      catch (Exception e)
      {
        return Result.Failure<List<GetOperationViewModel>>(e.Message);
      }
    }
  }
}
