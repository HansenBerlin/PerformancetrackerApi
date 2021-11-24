using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerformancetrackerApi.Attributes;
using PerformancetrackerApi.Interfaces;

namespace PerformancetrackerApi.Controllers
{
    [ApiKey]
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;

        public StudentsController(IStudentRepository studentRepo)
        {
            _studentRepo = studentRepo;
        }

        /// <summary>
        /// Gets a list of all students in a specific course.
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///
        ///     [
        ///         {
        ///             "matNr": 323334,
        ///             "mail": "stud.rocky.balboa@hwr.de",
        ///             "firstName": "Rocky",
        ///             "lastName": "Balboa"
        ///         },
        ///         {
        ///             "matNr": 353637,
        ///             "mail": "stud.vito.corleone@hwr.de",
        ///             "firstName": "Vito",
        ///             "lastName": "Corleone"
        ///         }
        ///     ]
        ///
        /// </remarks>
        /// <param name="kursId">Course ID to search in</param>
        /// <returns>a list of students</returns>
        
        [HttpGet("{kursId}")]
        public async Task<ActionResult> GetStudents([FromHeader(Name="ApiKey")][Required] string requiredHeader, int kursId)
        {
            var students = await _studentRepo.GetStudentsInCourse(kursId);
            if (students == null)
                return BadRequest();
            return Ok(students);
        }
    }
}