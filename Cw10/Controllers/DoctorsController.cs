using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw10.DTOs.Request;
using Cw10.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cw10.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    public class DoctorsController : ControllerBase
    {
        private readonly IDbService dbService;

        public DoctorsController(IDbService dbService)
        {
            this.dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetDoctors()
        {
            var res = dbService.GetDoctors();
            if (res == null)
                return NotFound("no data to display or an error ocured");
            else
                return Ok(res);
        }

        [HttpDelete("deleteDoctor/{IdDoctor}")]
        public IActionResult DeleteDoctor(int IdDoctor) {
            if (dbService.DeleteDoctor(IdDoctor))
                return Ok("Doctor was deleted");
            else
                return BadRequest("Wrong data was passed");
        }

        [HttpPost("addDoctor")]
        public IActionResult AddDoctor(AddDoctorRequest request) {
            if (dbService.AddDoctor(request))
                return Ok("Doctor was added");
            else
                return BadRequest("Wrong data was passed");
        }

        [HttpPut("updateDoctor")]
        public IActionResult UpdateDoctor(UpdateDoctorRequest request) {
            if (dbService.UpdateDoctor(request))
                return Ok("Doctor data was updated");
            else
                return BadRequest("Wrong data was passed");
        }
    }
}
