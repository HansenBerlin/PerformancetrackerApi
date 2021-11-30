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
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)   
        {
            _dashboardRepository = dashboardRepository;
        }
        
        [HttpGet("{matNr}", Name = "Dashboardvalues von Student")]
        public async Task<IActionResult> GetDashboardValuesForStudent([FromHeader(Name="ApiKey")][Required] string apiKey, int matNr)
        {
            var dashboard = await _dashboardRepository.GetDashboardForStudent(matNr);
            if (dashboard == null)
                return BadRequest();
            return Ok(dashboard);
        }
    }
}