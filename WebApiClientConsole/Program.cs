// See https://aka.ms/new-console-template for more information
using WebApiClientConsole;

Console.WriteLine("Hello, World!");
Console.WriteLine("API CLIENT!");
EmployeeAPIClient.CallGetAllEmployee().Wait();
EmployeeAPIClient.GetAllEmployeeJson().Wait();
//EmployeeAPIClient.AddnewEmployee().Wait();
EmployeeViewModel updatedEmployee = new EmployeeViewModel()
{
    EmpId = 23,
    FirstName = "Sangeetha",
    LastName = "N",
    City = "Chennai",
    BirthDate = new DateTime(1980, 01, 01),
    HireDate = new DateTime(2000, 01, 01),
    Title = "Manager"
};
EmployeeAPIClient.UpdateEmployee(updatedEmployee).Wait();
//EmployeeAPIClient.DeleteEmployee(22).Wait();
Console.ReadLine();