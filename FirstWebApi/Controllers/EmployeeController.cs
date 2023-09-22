using FirstWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private RepositoryEmployee _repositoryEmployee;
        public EmployeeController(RepositoryEmployee repositoryEmployee)
        {
            _repositoryEmployee = repositoryEmployee;
        }
        /*[HttpGet]
        public List<Employee> AllEmployees() 
        {
            List<Employee> employees = _repositoryEmployee.AllEmployees();
            return employees;
        }*/
        [HttpGet("get/allemployess")]
        public IEnumerable<EmployeeViewModel> getAllEmployees()
        {
            List<Employee> employees = _repositoryEmployee.AllEmployees();
            var emplist=(from emp in employees
                select new EmployeeViewModel()
                {
                      EmpId=emp.EmployeeId,
                      FirstName=emp.FirstName,
                      LastName=emp.LastName,
                      BirthDate=emp.BirthDate,
                      HireDate = emp.HireDate,
                      Title =emp.Title,
                      City=emp.City,
                      ReportsTo=emp.ReportsTo
                }
                ).ToList();
            return emplist;
        }
        [HttpGet("search/employee")]
        public Employee EmployeeDetails(int id)
        {
            Employee employee = _repositoryEmployee.FindEmployeeById(id);
            return employee;
        }
        [HttpPut]
        public Employee EditEmployee(int id, [FromBody] Employee updatedEmployee)
        {
            updatedEmployee.EmployeeId = id;
            Employee savedEmployee = _repositoryEmployee.UpdateEmployee(updatedEmployee);
            return savedEmployee;
        }
        [HttpPost("create/employee")]
        public Employee CreateEmployee([FromBody] Employee newEmployee)
        {
            _repositoryEmployee.AddEmployee(newEmployee); 
            return newEmployee;
        }
        [HttpDelete("{id}")]
        public int DeleteEmployee(int id)
        {
            _repositoryEmployee.DeleteEmployee(id);
            return id;
        }
    }
}
