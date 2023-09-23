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

        [HttpGet("/getAllEmployees")]
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

        /*[HttpPut("/UpdateEmployee/{id}")]
        public int EditEmployee1(int id,[FromBody] Employee updatedEmployee)
        {
            updatedEmployee.EmployeeId = id;
            int result=_repositoryEmployee.UpdateEmployee1(updatedEmployee);
            return result;
        }*/

        //By View Model
        [HttpPut("/UpdateEmployee")]
        public int EditEmployee1([FromBody] EmployeeViewModel updateEmployee)
        {
            //updateEmployee.EmployeeId = id;
            Employee employee = new Employee()
            {
                EmployeeId = updateEmployee.EmpId,
                FirstName = updateEmployee.FirstName,
                LastName = updateEmployee.LastName,
                BirthDate = updateEmployee.BirthDate,
                HireDate = updateEmployee.HireDate,
                City = updateEmployee.City,
                ReportsTo = updateEmployee.ReportsTo,
                Title = updateEmployee.Title
            };
            int result = _repositoryEmployee.UpdateEmployee1(employee);
            return result;
        }


        [HttpPost("create/emp")]
        public Employee CreateEmployee1([FromBody] Employee newEmployee)
        {
            _repositoryEmployee.AddEmployee1(newEmployee);
            return newEmployee;
        }


        [HttpPost("/AddNewEmployee")]
        public int CreateEmployee(EmployeeViewModel newEmployee)
        {
            /*_repositoryEmployee.AddEmployee(newEmployee); 
            return newEmployee;*/
            Employee emp = new Employee()
            {
                FirstName = newEmployee.FirstName,
                LastName = newEmployee.LastName,
                BirthDate = newEmployee.BirthDate,
                HireDate = newEmployee.HireDate,
                Title = newEmployee.Title,
                City = newEmployee.City,
                ReportsTo = newEmployee.ReportsTo > 0 ? newEmployee.ReportsTo : null
            };
            int result = _repositoryEmployee.AddEmployee(emp);
            return result;
        }


        [HttpDelete("{id}")]
        public int DeleteEmployee(int id)
        {
            _repositoryEmployee.DeleteEmployee(id);
            return id;
        }
        //By View Model
        [HttpDelete("/DeleteEmployee/{id}")]
        public int DeleteEmployee1(int id)
        {
            _repositoryEmployee.DeleteEmployee1(id);
            return id;
        }
    }
}
