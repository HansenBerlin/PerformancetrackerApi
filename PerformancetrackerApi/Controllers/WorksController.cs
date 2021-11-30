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
        private readonly IWorksRepository _worksRepository;
        
        public WorksController(IDueDatesRepository dueDatesRepository, IWorksRepository worksRepository)
        {
            _dueDatesRepository = dueDatesRepository;
            _worksRepository = worksRepository;
        }
        
       
        [HttpGet("commited/{matNr}/{kursId}", Name = "Eingereichte Leistungen Student/Kurs")]
        public async Task<IActionResult> GetStudentCommitedWorks([
            FromHeader(Name="ApiKey")][Required] string requiredHeader, int matNr, int kursId)
        {
            var works = await _worksRepository.GetCommitedWorksForStudentInCourse(matNr, kursId);
            if (works == null)
                return BadRequest();
            return Ok(works);
        }
        
        [HttpGet("uncommited/{matNr}/{kursId}", Name = "Nicht eingereichte Leistungen Student/Kurs")]
        public async Task<IActionResult> GetStudentUncommitedWorks([
            FromHeader(Name="ApiKey")][Required] string requiredHeader, int matNr, int kursId)
        {
            var works = await _worksRepository.GetUncommitedWorksForStudentInCourse(matNr, kursId);
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