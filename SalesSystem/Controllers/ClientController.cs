using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Models;
using SalesSystem.Models.Response;
using System.Collections.Generic;

namespace SalesSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Response oResponse = new Response();


            try
            {
                using (SalesSystemContext db = new SalesSystemContext())
                {
                    var list = db.Client.ToList();
                    oResponse.Sucess = true;
                    oResponse.Data = list;
                }
            }
            catch (Exception ex)
            {
                oResponse.Message = ex.Message;
                oResponse.Sucess = false;

            }


            return Ok(oResponse);
        }


    }
}
