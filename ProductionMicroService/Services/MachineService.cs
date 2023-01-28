using Azure;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using ProductionMicroService.Models;
using ProductionMicroService.Services.Interfaces;
using ProductionMicroService.ViewModels.Machine;
using ProductionMicroService.ViewModels.Operation;
using System.Reflection.PortableExecutable;
using Machine = ProductionMicroService.Models.Machine;
using Operation = ProductionMicroService.Models.Operation;

namespace ProductionMicroService.Services
{
  public class MachineService : DefaultService, IMachineService
  {
    private IOperationService _operationService;
    public MachineService(ProductionContext productionContext, IOperationService operationService) : base(productionContext)
    {
      _operationService = operationService;
    }

    public Result<int> AddMachine(AddMachineViewModel addMachine)
    {
      try
      {
        foreach (var operationId in addMachine.AvailableOperationIds)
        {
          var result = _operationService.GetOperationById(operationId);
          if (result.IsFailure)
            return Result.Failure<int>(result.Error);
        }

        var machineToBeAdded = new Machine() {Name = addMachine.Name};
        ProductionContext.Machines.Add(machineToBeAdded);
        ProductionContext.SaveChanges();

        foreach (var operationId in addMachine.AvailableOperationIds)
        {
          ProductionContext.OperationsToMachines.Add(new OperationsToMachine()
            {MachineId = machineToBeAdded.MachineId, OperationId = operationId});
        }
        ProductionContext.SaveChanges();
        return Result.Success(machineToBeAdded.MachineId);
      }
      catch (Exception e)
      {
        return Result.Failure<int>(e.Message);
      }
    }


    public Result<int> DeleteMachine(int machineId)
    {
      try
      {
        var deleteOperation = ProductionContext.Machines
        .Include(o => o.ProductionDetails)
          .FirstOrDefault(o => o.MachineId == machineId);
        if (deleteOperation == null)
          return Result.Failure<int>($"Machine with id:{deleteOperation.MachineId} doesn't exist");
        if (deleteOperation.ProductionDetails.Count(x => !x.IsArchived) > 0)
        {
          return Result.Failure<int>($"Machine with id:{deleteOperation.MachineId} has active production plans");
        }

        deleteOperation.IsArchived = true;
        ProductionContext.SaveChanges();
        return Result.Success(machineId);
      }
      catch (Exception e)
      {
        return Result.Failure<int>(e.Message);
      }
    }

    public Result<int> UpdateMachine(UpdateMachineViewModel updateMachine)
    {
      try
      {
        foreach (var operationId in updateMachine.AvailableOperationIds)
        {
          var result = _operationService.GetOperationById(operationId);
          if (result.IsFailure)
            return Result.Failure<int>(result.Error);
        }


        var machineToBeUpdated = ProductionContext.Machines.FirstOrDefault(x => x.MachineId == updateMachine.MachineId);
        if (machineToBeUpdated == null)
          return Result.Failure<int>($"Machine with id:{updateMachine.MachineId} doesn't exist");

        machineToBeUpdated.Name = updateMachine.Name;
        machineToBeUpdated.IsBroken = updateMachine.IsBroken;
        ProductionContext.SaveChanges();

        ProductionContext.OperationsToMachines.RemoveRange(ProductionContext.OperationsToMachines.Where(otm => otm.MachineId == updateMachine.MachineId));

        foreach (var operationId in updateMachine.AvailableOperationIds)
        {
          ProductionContext.OperationsToMachines.Add(new OperationsToMachine
            { MachineId = machineToBeUpdated.MachineId, OperationId = operationId });
        }
        ProductionContext.SaveChanges();
        return Result.Success(machineToBeUpdated.MachineId);
      }
      catch (Exception e)
      {
        return Result.Failure<int>(e.Message);
      }
    }

    public Result<GetMachineViewModel> GetMachineById(int machineId)
    {
      try
      {
        return Result.Success(
          Mapper.Map<Machine, GetMachineViewModel>(
            ProductionContext.Machines
              .Include(x => x.OperationsToMachines)
              .ThenInclude(x => x.Operation)
              .First(x => x.MachineId == machineId && !x.IsArchived)));
      }
      catch (Exception e)
      {
        return Result.Failure<GetMachineViewModel>($"Machine with id {machineId} doesn't exist");
      }
    }

    public Result<List<GetMachineViewModel>> GetAllMachines()
    {
      try
      {
        return Result.Success(Mapper.Map<List<Machine>, List<GetMachineViewModel>>(
          ProductionContext.Machines
            .Where(x => !x.IsArchived)
            .Include(x => x.OperationsToMachines)
            .ThenInclude(x => x.Operation)
            .ToList()));
      }
      catch (Exception e)
      {
        return Result.Failure<List<GetMachineViewModel>>(e.Message);
      }
    }

    public Result<List<GetMachineViewModel>> GetAllMachinesByState()
    {
      throw new NotImplementedException();
    }

    public Result<List<GetMachineViewModel>> GetAllMachinesByOperation(int operationId)
    {
      try
      {
        return Mapper.Map<List<Machine>, List<GetMachineViewModel>>(
          ProductionContext.OperationsToMachines
            .Where(x => x.OperationId == operationId)
            .Include(mto => mto.Machine)
            .Select(x => x.Machine).ToList());
      }
      catch (Exception e)
      {
        return Result.Failure<List<GetMachineViewModel>>(e.Message);
      }
    }
  }
}
