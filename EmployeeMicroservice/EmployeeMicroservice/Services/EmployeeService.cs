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

        public EmployeeView AddEmployee(AddEmployeeView employeeData)
        {
            var newEmployee = Mapper.Map<AddEmployeeView, Employee>(employeeData);
            EmployeeContext.Employees.Add(newEmployee);
            EmployeeContext.SaveChanges();

            employeeData.EmployeeId = newEmployee.EmployeeId;

            if (employeeData.EmployeeId != null)
            {
                var newEmployeeLoginData = Mapper.Map<AddEmployeeView, LoginData>(employeeData);
                EmployeeContext.LoginData.Add(newEmployeeLoginData);
                EmployeeContext.SaveChanges();
            }

            return GetEmployee(employeeData.EmployeeId ?? 0);
        }

        public bool DeleteEmployee(int id)
        {
            var employeeLoginDataToDelete = EmployeeContext.LoginData.Where(x => x.EmployeeId == id).FirstOrDefault();
            if (employeeLoginDataToDelete != null)
                EmployeeContext.LoginData.Remove(employeeLoginDataToDelete);
            EmployeeContext.SaveChanges();

            var employeeToDelete = EmployeeContext.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            if(employeeToDelete != null)
                EmployeeContext.Employees.Remove(employeeToDelete);
            return EmployeeContext.SaveChanges() > 0;

        }

        public bool EditEmployee(int id, EditEmployeeData employeeData)
        {
            var employeeToEdit = EmployeeContext.Employees.Where(x => x.EmployeeId == id).FirstOrDefault();
            if (employeeToEdit != null) {
                employeeToEdit.EmployeeTypeId = employeeData.EmployeeTypeId;
                employeeToEdit.FirstName = _encryptionService.EncryptData(employeeData.FirstName);
                employeeToEdit.LastName = _encryptionService.EncryptData(employeeData.LastName);
                employeeToEdit.IsBlocked = employeeData.IsBlocked;
            }
            return EmployeeContext.SaveChanges() > 0;
        }

        public EmployeeView GetEmployee(int id)
        {
            return EmployeeContext.Employees.Include(x => x.EmployeeType).Where(x=>x.EmployeeId == id).Select(Mapper.Map<Employee, EmployeeView>).FirstOrDefault();
        }

        public List<EmployeeView> GetEmployees()
        {
            return EmployeeContext.Employees.Include(x=> x.EmployeeType).Select(Mapper.Map<Employee, EmployeeView>).ToList();
        }

        public List<EmployeeTypeView> GetEmployeeTypes()
        {
            return EmployeeContext.EmployeeTypes.Select(Mapper.Map<EmployeeType, EmployeeTypeView>).ToList();
        }
    }
}
