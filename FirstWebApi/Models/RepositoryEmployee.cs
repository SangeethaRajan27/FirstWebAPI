using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace FirstWebApi.Models
{
    public class RepositoryEmployee
    {
        private NorthWindContext _context;
        public RepositoryEmployee(NorthWindContext context)
        {
            _context = context;
        }
        public List<Employee> AllEmployees()
        {
            return _context.Employees.ToList();
        }
        public Employee FindEmployeeById(int id)
        {
            Employee employee = _context.Employees.Find(id);
            return employee;
        }
        public Employee UpdateEmployee(Employee updatedEmployee)
        {
            _context.Employees.Update(updatedEmployee);
            // Console.WriteLine(_context.Entry(updatedEmployee).State); //
            _context.SaveChanges();
            return updatedEmployee;
        }
        public int UpdateEmployee1(Employee updatedEmployee)
        {
            EntityState es = _context.Entry(updatedEmployee).State;
            Console.WriteLine($"EntityState before Update:{es.GetDisplayName()}");
            _context.Employees.Update(updatedEmployee);
            // Console.WriteLine(_context.Entry(updatedEmployee).State); //
            es = _context.Entry(updatedEmployee).State;
            Console.WriteLine($"EntityState After Update:{es.GetDisplayName()}");
            int result = _context.SaveChanges();
            es = _context.Entry(updatedEmployee).State;
            Console.WriteLine($"EntityState After Save Changes:{es.GetDisplayName()}");
            return result;
        }
        public int UpdateEmployee2(Employee updatedEmployee)
        {
            try
            {
                EntityState es = _context.Entry(updatedEmployee).State;
                Console.WriteLine($"EntityState before Update: {es.GetDisplayName()}");
                _context.Employees.Update(updatedEmployee);
                es = _context.Entry(updatedEmployee).State;
                Console.WriteLine($"EntityState After Update: {es.GetDisplayName()}");
                int result = _context.SaveChanges();
                es = _context.Entry(updatedEmployee).State;
                Console.WriteLine($"EntityState After Save Changes: {es.GetDisplayName()}");
                return result;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"DbUpdateException: {ex.Message}");

                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }

        public Employee AddEmployee1(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            _context.SaveChanges();

            return newEmployee;
        }
        public int AddEmployee(Employee newEmployee)
        {
            Employee? foundEmp = _context.Employees.Find(newEmployee.EmployeeId);
            if(foundEmp != null)
            {
                throw new Exception("Failed to add employee. duplicate Id");
            }
            EntityState es = _context.Entry(newEmployee).State;
            Console.WriteLine($"EntityState before ADD:{es.GetDisplayName()}");
            _context.Employees.Add(newEmployee);//dbcontext.entity."add" used to attach
            es = _context.Entry(newEmployee).State;
            Console.WriteLine($"EntityState After ADD:{es.GetDisplayName()}");
            int result =_context.SaveChanges();
            es = _context.Entry(newEmployee).State;
            Console.WriteLine($"EntityState After Save changes:{es.GetDisplayName()}");
            return result;
        }
        public void DeleteEmployee(int employeeId)
        {
            var employeeToDelete = _context.Employees.Find(employeeId);

            if (employeeToDelete != null)
            {
                _context.Employees.Remove(employeeToDelete);
                _context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Employee not exists");
            }
        }
        public int DeleteEmployee1(int id)
        {
            Employee empdelete = _context.Employees.Find(id);
            EntityState es = EntityState.Detached;
            int result = 0;
            if (empdelete != null)
            {
                es = _context.Entry(empdelete).State;
                Console.WriteLine($"EntityState before Delete:{es.GetDisplayName()}");
                _context.Employees.Remove(empdelete);//dbcontext.entity."add" used to attach
                es = _context.Entry(empdelete).State;
                Console.WriteLine($"EntityState After Delete:{es.GetDisplayName()}");
                result = _context.SaveChanges();
                es = _context.Entry(empdelete).State;
                Console.WriteLine($"EntityState After Save changes:{es.GetDisplayName()}");
            }
            return result;
        }
        }
}
