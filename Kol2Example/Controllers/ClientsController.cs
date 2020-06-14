using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kol2Example.DTO_s.Requests;
using Kol2Example.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Kol2Example.Controllers
{
    [ApiController]
    [Route("api")]
    public class ClientsController : ControllerBase
    {
        private readonly IDbService dbService;

        public ClientsController(IDbService dbService)
        {
            this.dbService = dbService;
        }

        // GET: /<controller>/
        [HttpGet("orders")]
        public IActionResult getOrdersByClient(string ClientSurname)
        {
            var res = dbService.getOrdersByClientSurname(ClientSurname);
            if (res == null)
                return BadRequest("Wrong Client Surname");
            else
                return Ok(res);
        }

        [HttpPost("clients/{index}/orders")]
        public IActionResult AddOrder(int index, ClientDataRequst clientRequest)
        {
            if (dbService.addOrder(index, clientRequest))
            {
                return Ok("Order was added");
            }
            return BadRequest("Wrong data was passed");
            //return Ok(dbService.test());
        }
    }
}
