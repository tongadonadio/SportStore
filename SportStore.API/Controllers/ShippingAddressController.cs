using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

using SportStore.BusinessLogic;
using SportStore.Model;
using SportStore.Repository;

namespace SportStore.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ShippingAddressController : SportStoreController
    {
        public ShippingAddressController(ISportStoreBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        // GET: api/ShippingAddress
        public IEnumerable<ShippingAddress> Get()
        {
            return this.businessLogic.ShippingAddress.All();
        }

        // GET: api/ShippingAddress/5
        public IHttpActionResult Get(Guid id)
        {
            var shippingAddress = this.businessLogic.ShippingAddress.GetById(id);

            if (shippingAddress != null)
            {
                return Ok(shippingAddress);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/ShippingAddress
        public IHttpActionResult Post([FromBody]ShippingAddress shippingAddress)
        {
            try
            {
                var id = this.businessLogic.ShippingAddress.Create(shippingAddress);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        // PUT: api/ShippingAddress/5
        public IHttpActionResult Put(Guid id, [FromBody]ShippingAddress shippingAddress)
        {
            try
            {
                shippingAddress.Id = id;

                this.businessLogic.ShippingAddress.Update(shippingAddress);

                return Ok(shippingAddress.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ShippingAddress/5
        public IHttpActionResult Delete(Guid id)
        {
            try
            {
                this.businessLogic.ShippingAddress.DeleteById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
