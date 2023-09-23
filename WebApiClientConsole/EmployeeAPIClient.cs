using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net;

namespace WebApiClientConsole
{
    internal class EmployeeAPIClient
    {
        static Uri uri = new Uri("http://localhost:5295/");
        public static async Task CallGetAllEmployee()
        {
            //async->not wait for the response
            using(var client=new HttpClient()) 
            {
                client.BaseAddress = uri;
                //HTTPGET:
                HttpResponseMessage response = await client.GetAsync("getAllEmployees");//call the function asynchronously
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    String x =await response.Content.ReadAsStringAsync();
                    await Console.Out.WriteLineAsync(x);
                }
            }
        }
        public static async Task GetAllEmployeeJson()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                List<EmployeeViewModel> employees = new List<EmployeeViewModel>();
                client.DefaultRequestHeaders
                    .Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //HTTPGET:
                HttpResponseMessage response = await client.GetAsync("getAllEmployees");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    String json = await response.Content.ReadAsStringAsync();
                    employees = JsonConvert.DeserializeObject<List<EmployeeViewModel>>(json);
                    foreach (EmployeeViewModel emp in employees)
                    {
                        await Console.Out.WriteLineAsync($"{emp.EmpId},{emp.FirstName}:{emp.LastName}");
                    }
                }

            }
        }
        public static async Task AddnewEmployee()
        {
            using (var client=new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                EmployeeViewModel employee = new EmployeeViewModel()
                {
                    FirstName = "Willam",
                    LastName = "John",
                    City = "Nyc",
                    BirthDate = new DateTime(1980, 01, 01),
                    HireDate = new DateTime(2000, 01, 01),
                    Title = "Manager"
                };
                /*IMPORTANT
                 empid = serialization
                 objet(employee)-we serialize-byte array-post method-result
                 */
                var myContent =JsonConvert.SerializeObject(employee);
                var buffer=Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //Http POST
                HttpResponseMessage response = await client.PostAsync("AddNewEmployee", byteContent);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync(response.StatusCode.ToString());
                }
            }
        }
        /*public static async Task UpdateEmployee()
        {
            using (var client=new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept
                    .Add (new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }*/
        public static async Task UpdateEmployee(EmployeeViewModel employee)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var myContent = JsonConvert.SerializeObject(employee);
                var buffer = Encoding.UTF8.GetBytes(myContent); 
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                // HttpPut:
                HttpResponseMessage response = await client.PutAsync($"UpdateEmployee", byteContent);
                
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Employee updated successfully.");
                }
                else
                {
                    Console.WriteLine($"Failed to update the employee. Status Code: {response.StatusCode}. Reason: {response.ReasonPhrase}");
                }
            }
        }
        public static async Task DeleteEmployee(int employeeId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                string deleteUrl = $"DeleteEmployee/{employeeId}";
                HttpResponseMessage response = await client.DeleteAsync(deleteUrl);

                if (response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync("Employee deleted successfully.");
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    await Console.Out.WriteLineAsync("Employee not found.");
                }
                else
                {
                    await Console.Out.WriteLineAsync($"Error: {response.StatusCode}");
                }
            }
        }

    }
}
