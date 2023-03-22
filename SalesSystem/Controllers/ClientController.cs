using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Models;
using SalesSystem.Models.Request;
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
                    var list = db.Clients.ToList();
                    oResponse.Sucess = true;
                    oResponse.Data = list;
                }
            }
            catch (Exception ex)
            {
                oResponse.Message = ex.Message;
            }

            return Ok(oResponse);
        }


        [HttpPost]
        public IActionResult Add(ClientReq oModel)
        {

            Response oResponse = new Response();
            try
            {
                using (SalesSystemContext db = new())
                {
                    Client oClient = new() { Name = oModel.Name };
                    var dsada = db.Clients.Add(oClient);
                    db.SaveChanges();

                    oResponse.Data = dsada.Entity;
                    oResponse.Sucess = true;
                }


            }
            catch (Exception ex)
            {
                oResponse.Message = ex.Message;
            }

            return Ok(oResponse);
        }
    }
}
