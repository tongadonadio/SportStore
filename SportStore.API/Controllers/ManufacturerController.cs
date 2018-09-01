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
    public class ManufacturerController : SportStoreController
    {
        public ManufacturerController(ISportStoreBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        // GET: api/Manufacturer
        public IEnumerable<Manufacturer> Get()
        {
            AuthenticateWithTokenInHeaders();

            return this.businessLogic.Manufacturer.All();
        }

        // GET: api/Manufacturer/5
        public IHttpActionResult Get(Guid id)
        {
            AuthenticateWithTokenInHeaders();

            var manufacturer = this.businessLogic.Manufacturer.GetById(id);

            if (manufacturer != null)
            {
                return Ok(manufacturer);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Manufacturer
        public IHttpActionResult Post([FromBody]Manufacturer manufacturer)
        {
            try
            {
                Guid manufacturerName = this.businessLogic.Manufacturer.Create(manufacturer);

                return Ok(manufacturerName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Manufacturer/5
        public IHttpActionResult Put(Guid id, [FromBody]Manufacturer manufacturer)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                manufacturer.Id = id;

                this.businessLogic.Manufacturer.Update(manufacturer);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Manufacturer/5
        public IHttpActionResult Delete(Guid id)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                this.businessLogic.Manufacturer.DeleteById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
