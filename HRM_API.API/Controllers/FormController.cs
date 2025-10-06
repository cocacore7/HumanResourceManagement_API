using Microsoft.AspNetCore.Mvc;

namespace HRM_API.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormController : ControllerBase
    {   
        [HttpGet("GetHiWorld")]
        public string GetHiWorld()
        {
            return "Get Hi World";
        }
        [HttpGet("GetHiWorld2")]
        public string GetHiWorld2()
        {
            return "Get Hi World";
        }
        [HttpGet("GetHiWorld3")]
        public string GetHiWorld3()
        {
            return "Get Hi World";
        }
    }
}
