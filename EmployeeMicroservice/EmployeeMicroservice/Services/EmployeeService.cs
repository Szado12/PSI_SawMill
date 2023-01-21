using CSharpFunctionalExtensions;
using EmployeeMicroservice.Models;
using EmployeeMicroservice.Services.Interfaces;
using EmployeeMicroservice.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace EmployeeMicroservice.Services
{
    public class EmployeeService : DefaultService, IEmployeeService
    {
        private readonly IEncryptionService _encryptionService;

        public EmployeeService(IEncryptionService encryptionService)
        {
            _encryptionService = encryptionService;
        }

        public Result<EmployeeView> AddEmployee(AddEmployeeView employeeData)
        {
            var newEmployee = Mapper.Map<AddEmployeeView, Employee>(employeeData);
            EmployeeContext.Employees.Add(newEmployee);
            EmployeeContext.SaveChanges();

            employeeData.EmployeeId = newEmployee.EmployeeId;

            if(employeeData.EmployeeId == null)
                return Result.Failure<EmployeeView>($"Adding employee failed.");

            var newEmployeeLoginData = Mapper.Map<AddEmployeeView, LoginData>(employeeData);
            EmployeeContext.LoginData.Add(newEmployeeLoginData);
            EmployeeContext.SaveChanges();

            return GetEmployee(employeeData.EmployeeId ?? 0);
        }

        public Result<bool> DeleteEmployee(int id)
        {
            var employeeLoginDataToDelete = EmployeeContext.LoginData.Where(x => x.EmployeeId == id).FirstOrDefault();
            if (employeeLoginDataToDelete == null)
                return Result.Failure<bool>($"Deleting user with id {id} failed");
            EmployeeContext.LoginData.Remove(employeeLoginDataToDelete);
            EmployeeContext.SaveChanges();

            var employeeToDelete = EmployeeContext.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            if (employeeToDelete == null)
                return Result.Failure<bool>($"Deleting user with id {id} failed");
            EmployeeContext.Employees.Remove(employeeToDelete);
            var result = EmployeeContext.SaveChanges() > 0;

            if (result) 
                return Result.Success(result);
            else 
                return Result.Failure<bool>($"Deleting user with id {id} failed");

        }

        public Result<bool> EditEmployee(int id, EditEmployeeData employeeData)
        {
            var employeeToEdit = EmployeeContext.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            if (employeeToEdit == null)
                return Result.Failure<bool>($"Editing user with id {id} failed");
            employeeToEdit.EmployeeTypeId = employeeData.EmployeeTypeId;
            employeeToEdit.FirstName = _encryptionService.EncryptData(employeeData.FirstName);
            employeeToEdit.LastName = _encryptionService.EncryptData(employeeData.LastName);
            employeeToEdit.IsBlocked = employeeData.IsBlocked;

            var result = EmployeeContext.SaveChanges() > 0;

            if (result)
                return Result.Success(result);
            else
                return Result.Failure<bool>($"Editing user with id {id} failed");
        }

        public Result<EmployeeView> GetEmployee(int id)
        {
            var result = EmployeeContext.Employees.Include(x => x.EmployeeType).Where(x=>x.EmployeeId == id).Select(Mapper.Map<Employee, EmployeeView>).FirstOrDefault();
            if (result != null)
                return Result.Success(result);
            else
                return Result.Failure<EmployeeView>($"Getting user with id {id} failed");
        }

        public Result<List<EmployeeView>> GetEmployees()
        {
            var result = EmployeeContext.Employees.Include(x=> x.EmployeeType).Select(Mapper.Map<Employee, EmployeeView>).ToList();
            if (result != null)
                return Result.Success(result);
            else
                return Result.Failure<List<EmployeeView>>($"Getting users failed");
        }

        public Result<List<EmployeeTypeView>> GetEmployeeTypes()
        {
            var result = EmployeeContext.EmployeeTypes.Select(Mapper.Map<EmployeeType, EmployeeTypeView>).ToList();
            if (result != null)
                return Result.Success(result);
            else
                return Result.Failure<List<EmployeeTypeView>>($"Getting employee types failed");
        }
    }
}
