using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PerformancetrackerApi.Attributes;
using PerformancetrackerApi.Entities;
using PerformancetrackerApi.Interfaces;

namespace PerformancetrackerApi.Controllers
{
    [ApiKey]
    [Route("api/grades")]
    [ApiController]

    public class GradesController : Controller
    {
        private readonly IGradesRepository _gradesRepo;

        public GradesController(IGradesRepository gradesRepo)
        {
            _gradesRepo = gradesRepo;
        }

        /// <summary>
        /// Gets a list of all grades of a specific student in a specific course.
        /// </summary>
        /// <remarks>
        /// Sample response:
        ///
        ///     [
        ///       {
        ///         "value": 1,
        ///         "dueDate": "2021-10-01T00:00:00",
        ///         "commitDate": "2021-10-02T11:11:00",
        ///         "fixed": false,
        ///         "type": "Klausur"
        ///       },
        ///       {
        ///         "value": 2.3,
        ///         "dueDate": "2021-11-01T00:00:00",
        ///         "commitDate": "2021-11-01T23:59:00",
        ///         "fixed": false,
        ///         "type": "Arbeitsblatt"
        ///       }
        ///     ]
        /// A header containing a valid API Key payload must be included.
        /// </remarks>
        /// <param name="matNr">Student Matrikelnummer (ID)</param>
        /// <param name="kursNr">Kurs ID</param>
        /// <returns>a list of grades</returns>
        [HttpGet("{matNr}/{kursNr}", Name = "Noten für Student in Kurs")]
        public async Task<IActionResult> GetStudentsGradesInCourse([FromHeader(Name="ApiKey")][Required] string apiKey, int matNr, int kursNr)
        {
            var grades = await _gradesRepo.GetGradesPerStudentInCourse(matNr, kursNr);
            if (grades == null)
                return BadRequest();
            return Ok(grades);
        }
        
        //[Consumes(MediaTypeNames.Application.Json)]
        [HttpPut("update/{gradeId:int}/{gradeValue:double}", Name = "UpdateGradeForStudent")]
        public async Task<IActionResult> Put([FromHeader(Name="ApiKey")][Required] string apiKey, int gradeId, double gradeValue)
        {
            if (gradeValue is < 1 or > 5)
            {
                return BadRequest();
            }
            var success = await _gradesRepo.UpdateGrade(gradeId, gradeValue);
            if (success)
                return Ok();
            return BadRequest();
        }
    }
}