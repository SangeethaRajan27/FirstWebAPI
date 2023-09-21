using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {//adding changes via github
        // api/Calculator/Add?a=10&b=5
        //"/" becomes a new route without adding the controller 
        [HttpGet("/add")]  
       /* http://localhost:5295/sum?a=10&b=0*/
        public int Add(int a, int b) //methods in c# should in uppercase
        {
            return a + b;
        }
        [HttpGet("/sum")]
        public int Sum(int a, int b) //methods in c# should in uppercase
        {
            return a + b+1000;
        }
        // api/calculator/subtract?a=10&b=5
        [HttpPost]
        public int Subtract(int a, int b) //methods in c# should in uppercase
        {
            return a - b;
        }
        // api/calculator/multiply?a=10&b=5
        [HttpPut]
        public int Multiply(int a, int b) //methods in c# should in uppercase
        {
            return a * b;
        }
        // api/calculator/divide?a=10&b=5
        [HttpDelete]
        public int Divide(int a, int b) //methods in c# should in uppercase
        {
            return a / b;
        }
    }
}
