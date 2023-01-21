using EmployeeMicroservice.Services.Interfaces;
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
        [Route("all")]
        public IActionResult GetEmployees()
        {
            var x = _employeeService.GetEmployees();
            return Ok(x);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetEmployees([FromRoute]int id)
        {
            var x = _employeeService.GetEmployee(id);
            return Ok(x);
        }

        [HttpPost]
        public IActionResult AddEmployee(AddEmployeeView data)
        {
            var x = _employeeService.AddEmployee(data);
            return Ok(x);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteEmployee([FromRoute] int id)
        {
            var x = _employeeService.DeleteEmployee(id);
            return Ok(x);
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult EditEmployee([FromRoute] int id, EditEmployeeData data)
        {
            var x = _employeeService.EditEmployee(id, data);
            return Ok(x);
        }

        [HttpGet]
        [Route("types")]
        public IActionResult EmployeesTypes()
        {
            var x = _employeeService.GetEmployeeTypes();
            return Ok(x);
        }
    }
}
