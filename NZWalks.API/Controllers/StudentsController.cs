using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]

    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] students_array = new string[] { "Cristian", "Diana", "Dariana", "Guillermo" };
            return Ok(students_array);
        }
    }
}
