using EmployeeMicroservice.Services.Interfaces;
using EmployeeMicroservice.Utils;
using EmployeeMicroservice.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMicroservice.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            return _employeeService.GetEmployees().ToActionResult();
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetEmployee([FromRoute]int id)
        {
            return _employeeService.GetEmployee(id).ToActionResult();
        }

        [HttpGet]
        [Route("type-{id}")]
        public IActionResult GetEmployeeByEmployeeType([FromRoute] int id)
        {
            return _employeeService.GetActiveEmployeesByEmployeeType(id).ToActionResult();
        }

        [HttpGet]
        [Route("operators")]
        public IActionResult GetOperators()
        {
            return _employeeService.GetActiveEmployeesByEmployeeType((int)EmployeeTypesEnum.MachineOperator).ToActionResult();
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeView data)
        {
            return _employeeService.AddEmployee(data).ToActionResult();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteEmployee([FromRoute] int id)
        {
            return _employeeService.DeleteEmployee(id).ToActionResult();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult EditEmployee([FromRoute] int id, EditEmployeeData data)
        {
            return _employeeService.EditEmployee(id, data).ToActionResult();
        }

        [HttpGet]
        [Route("types")]
        public IActionResult EmployeesTypes()
        {
            return _employeeService.GetEmployeeTypes().ToActionResult();
        }
    }
}
