using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        [HttpGet("/sum1")]
        //AmbiguousMatchException: The request matched multiple endpoints.
        public int Sum(int a, int b) //methods in c# should in uppercase
        {
            return a + b + 1000;
        }
        [HttpGet("demo/sum")]
        //we need to add controller name to be as unique indentifier
        //AmbiguousMatchException: The request matched multiple endpoints.
        public int Sum1(int a, int b) //methods in c# should in uppercase
        {
            return a + b + 1000;
        }
    }
}
