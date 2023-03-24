using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Models;
using SalesSystem.Models.Request;
using SalesSystem.Models.Response;

namespace SalesSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
                    var list = db.Clients.OrderByDescending(c => c.Id).ToList();
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
                    var newClient = db.Clients.Add(oClient).Entity;
                    db.SaveChanges();

                    oResponse.Data = newClient;
                    oResponse.Sucess = true;
                }


            }
            catch (Exception ex)
            {
                oResponse.Message = ex.Message;
            }

            return Ok(oResponse);
        }

        [HttpPut]
        public IActionResult Edit(ClientReq oModel)
        {

            Response oResponse = new Response();
            try
            {
                using (SalesSystemContext db = new())
                {
                    Client? oClient = db.Clients.Find(oModel.id);
                    oClient.Name = oModel.Name;
                    db.Entry(oClient).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                    db.SaveChanges();

                    oResponse.Data = oClient;
                    oResponse.Sucess = true;
                }


            }
            catch (Exception ex)
            {
                oResponse.Message = ex.Message;
            }

            return Ok(oResponse);
        }



        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {

            Response oResponse = new Response();
            try
            {
                using (SalesSystemContext db = new())
                {
                    Client? oClient = db.Clients.Find(Id);
                    db.Remove(oClient);
                    db.SaveChanges();
                    oResponse.Data = oClient;
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
