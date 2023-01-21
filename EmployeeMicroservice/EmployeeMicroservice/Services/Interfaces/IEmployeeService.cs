using EmployeeMicroservice.ViewModels;

namespace EmployeeMicroservice.Services.Interfaces
{
    public interface IEmployeeService
    {
        List<EmployeeView> GetEmployees();
        EmployeeView GetEmployee(int id);
        bool EditEmployee(int id, EditEmployeeData employeeData);
        bool DeleteEmployee(int id);
        EmployeeView AddEmployee(AddEmployeeView employeeData);
        List<EmployeeTypeView> GetEmployeeTypes();
    }
}
