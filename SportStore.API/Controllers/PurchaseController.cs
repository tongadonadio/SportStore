using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

using SportStore.BusinessLogic;
using SportStore.Model;

namespace SportStore.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PurchaseController : SportStoreController
    {
        public PurchaseController(ISportStoreBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        // GET: api/Purchase
        //public IEnumerable<Purchase> Get()
        //{
        //    AuthenticateWithTokenInHeaders();

        //    return this.businessLogic.Purchase.All();
        //}

        // GET: api/Purchase/5
        public IHttpActionResult Get(Guid id)
        {
            AuthenticateWithTokenInHeaders();

            var manufacturer = this.businessLogic.Purchase.GetById(id);

            if (manufacturer != null)
            {
                return Ok(manufacturer);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
