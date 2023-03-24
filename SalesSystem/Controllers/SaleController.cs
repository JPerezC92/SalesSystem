using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Models;
using SalesSystem.Models.Request;
using SalesSystem.Models.Response;

namespace SalesSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {

        [HttpPost]
        public IActionResult add(SaleReq req)
        {
            Response oResponse = new();

            try
            {
                using (SalesSystemContext db = new())
                {
                    Sale oSale = new();
                    oSale.Total = req.Total;
                    oSale.Date = DateTime.Now;
                    oSale.IdClient = req.IdClient;
                    db.Sales.Add(oSale);
                    db.SaveChanges();

                    foreach (var detail in req.DetailList)
                    {
                        SaleDetail oSaleDetail = new();
                        oSaleDetail.Quantity = detail.Quantity;                        
                        oSaleDetail.IdProduct = detail.IdProduct;
                        oSaleDetail.UnitPrice = detail.UnitPrice;                      
                        oSaleDetail.Total = detail.Total;
                        oSaleDetail.IdSale = oSale.Id;

                        db.SaleDetails.Add(oSaleDetail);
                        db.SaveChanges();

                    }

                    oResponse.Sucess= true;
                }


            }
            catch { }

            return Ok(oResponse);
        }

    }
}
