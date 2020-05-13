using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw9.DTOs.Requests;
using Cw9.DTOs.Responses;
using Cw9.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cw9.Controllers
{
    [ApiController]
    [Route("api/")]
    public class EnrollmentsController : ControllerBase
    {
        IStudentsDbService dbService;

        public EnrollmentsController(IStudentsDbService dbService)
        {
            this.dbService = dbService;
        }
        [HttpGet("students")]
        public IActionResult GetStudentsList()
        {
            return Ok(dbService.GetStudents());
        }
        [HttpGet("{num}")]
        public IActionResult GetStudentsList(String num)
        {
            return Ok(dbService.IsStudentExists(num));
        }

        [HttpPost("enrollments/promotions")]
        public IActionResult PromoteStudents(PromoteStudentRequest request)
        {
            PromoteStudentResponse response = dbService.PromoteStudent(request);
            if (response == null)
                return NotFound("Wrong data was passed");
            else
                return StatusCode(201,response);
        }
        [HttpPost("enrollments")]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            EnrollStudentResponse response=dbService.EnrollStudent(request);
            if (response == null)
                return NotFound("Wrong data was passed");
            else
                return StatusCode(201,response);
        }
    }
}