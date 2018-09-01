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
    public class PaymentMethodController : SportStoreController
    {
        public PaymentMethodController(ISportStoreBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        // GET: api/PaymentMethod
        public IEnumerable<PaymentMethod> Get()
        {
            AuthenticateWithTokenInHeaders();

            return this.businessLogic.PaymentMethod.All();
        }

        // GET: api/PaymentMethod/5
        public IHttpActionResult Get(Guid id)
        {
            AuthenticateWithTokenInHeaders();

            var paymentMethod = this.businessLogic.PaymentMethod.GetById(id);

            if (paymentMethod != null)
            {
                return Ok(paymentMethod);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/PaymentMethod
        public IHttpActionResult Post([FromBody]PaymentMethod paymentMethod)
        {
            try
            {
                Guid paymentMethodName = this.businessLogic.PaymentMethod.Create(paymentMethod);

                return Ok(paymentMethodName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/PaymentMethod/5
        public IHttpActionResult Put(Guid id, [FromBody]PaymentMethod paymentMethod)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                paymentMethod.Id = id;

                this.businessLogic.PaymentMethod.Update(paymentMethod);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/PaymentMethod/5
        public IHttpActionResult Delete(Guid id)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                this.businessLogic.PaymentMethod.DeleteById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
