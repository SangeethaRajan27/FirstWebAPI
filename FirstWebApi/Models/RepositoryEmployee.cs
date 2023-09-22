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
        public Employee AddEmployee(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            _context.SaveChanges(); 

            return newEmployee;
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
    }
}
