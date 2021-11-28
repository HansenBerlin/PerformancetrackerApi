using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerformancetrackerApi.Attributes;
using PerformancetrackerApi.Interfaces;

namespace PerformancetrackerApi.Controllers
{
    [ApiKey]
    [Route("api/course")]
    [ApiController]
    
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepo;

        public CourseController(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }
        
        [HttpGet("{matNr}", Name = "Kurse von Student")]
        public async Task<IActionResult> GetStudentsGradesInCourse([FromHeader(Name="ApiKey")][Required] string apiKey, int matNr)
        {
            var courses = await _courseRepo.GetAvailableCoursesForStudent(matNr);
            if (courses == null)
                return BadRequest();
            return Ok(courses);
        }
    }
}