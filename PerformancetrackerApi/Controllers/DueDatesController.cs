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
    [Route("api/duedates")]
    [ApiController]
    
    public class DueDatesController : ControllerBase
    {
        private readonly IDueDatesRepository _dueDatesRepo;

        public DueDatesController(IDueDatesRepository dueDatesRepo)
        {
            _dueDatesRepo = dueDatesRepo;
        }
        
        [HttpGet("{kursNr:int}/{matNr:int}", Name = "Übrige Latedays für Kategorien")]
        public async Task<IActionResult> GetStudentsGradesInCourse([FromHeader(Name="ApiKey")][Required] string apiKey, int kursNr, int matNr)
        {
            var leftDays = await _dueDatesRepo.GetLeftLateDaysForAllWorkCategories(kursNr, matNr);
            if (leftDays == null)
                return BadRequest();
            return Ok(leftDays);
        }
    }
}