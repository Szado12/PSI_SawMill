using CSharpFunctionalExtensions;
using EmployeeMicroservice.ViewModels;

namespace EmployeeMicroservice.Services.Interfaces
{
    public interface IEmployeeService
    {
        Result<List<EmployeeView>> GetEmployees();
        Result<EmployeeView> GetEmployee(int id);
        Result<bool> EditEmployee(int id, EditEmployeeData employeeData);
        Result<bool> DeleteEmployee(int id);
        Result<EmployeeView> AddEmployee(AddEmployeeView employeeData);
        Result<List<EmployeeTypeView>> GetEmployeeTypes();
    }
}
