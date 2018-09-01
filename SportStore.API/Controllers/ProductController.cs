using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

using SportStore.API.Controllers.RequestParams;
using SportStore.BusinessLogic;
using SportStore.Model;

namespace SportStore.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProductController : SportStoreController
    {
        public ProductController(ISportStoreBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        // GET: api/Product
        public IEnumerable<Product> Get()
        {
            return this.businessLogic.Product.All();
        }

        // GET: api/Product/5
        //public IEnumerable<Product> Get([FromUri]ProductParams args)
        //{
        //    return this.businessLogic.Product.Find(args.GetPredicate());
        //}

        // GET: api/Product/5
        public IHttpActionResult Get(Guid id)
        {
            var product = this.businessLogic.Product.GetById(id);

            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Product
        public IHttpActionResult Post([FromBody]Product product)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                var id = this.businessLogic.Product.Create(product);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Product/5
        public IHttpActionResult Put(Guid id, [FromBody]Product product)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                ThrowIfPutArgumentsAreWrong(id, product);

                this.businessLogic.Product.Update(product);

                return Ok(product.Code);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void ThrowIfPutArgumentsAreWrong(Guid code, Product product)
        {
            if (!product.Code.Equals(code))
            {
                throw new ArgumentException("`product.Code` should be equal than `code`");
            }
        }

        // DELETE: api/Product/5
        public IHttpActionResult Delete(Guid id)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                this.businessLogic.Product.DeleteById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
