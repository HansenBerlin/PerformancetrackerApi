using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PerformancetrackerApi.Attributes;
using PerformancetrackerApi.Interfaces;

namespace PerformancetrackerApi.Controllers
{
    [ApiKey]
    [Route("api/works")]
    [ApiController]
    
    public class WorksController : ControllerBase
    {
        private readonly IDueDatesRepository _dueDatesRepository;
        
        public WorksController(IDueDatesRepository dueDatesRepository)
        {
            _dueDatesRepository = dueDatesRepository;
        }
        
       
        [HttpGet("notcommited/{matNr}/{kursId}", Name = "Noch nicht eingereichte Leistungen Student/Kurs")]
        public async Task<IActionResult> GetStudentsGrades([
            FromHeader(Name="ApiKey")][Required] string requiredHeader, int matNr, int kursId)
        {
            var works = await _dueDatesRepository.GetDueDatesForStudentInCourse(matNr, kursId);
            if (works == null)
                return BadRequest();
            return Ok(works);
        }
        
        [HttpGet("participation/{matNr}/{kursId}", Name = "Aktive Mitarbeit")]
        public async Task<IActionResult> GetParticipationWorks([
            FromHeader(Name="ApiKey")][Required] string requiredHeader, int matNr, int kursId)
        {
            var works = await _dueDatesRepository.GetParticipationWorks(matNr, kursId);
            if (works == null)
                return BadRequest();
            return Ok(works);
        }
    }
}