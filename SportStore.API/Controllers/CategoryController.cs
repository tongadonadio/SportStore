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
    public class CategoryController : SportStoreController
    {
        public CategoryController(ISportStoreBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        // GET: api/Category
        public IEnumerable<Category> Get()
        {
            AuthenticateWithTokenInHeaders();

            return this.businessLogic.Category.All();
        }

        // GET: api/Category/5
        public IHttpActionResult Get(Guid id)
        {
            AuthenticateWithTokenInHeaders();

            var category = this.businessLogic.Category.GetById(id);

            if (category != null)
            {
                return Ok(category);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Category
        public IHttpActionResult Post([FromBody]Category category)
        {
            try
            {
                Guid categoryId = this.businessLogic.Category.Create(category);

                return Ok(categoryId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Category/5
        public IHttpActionResult Put(Guid id, [FromBody]Category category)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                ThrowIfPutArgumentsAreWrong(id, category);

                this.businessLogic.Category.Update(category);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void ThrowIfPutArgumentsAreWrong(Guid id, Category category)
        {
            if (!category.Id.Equals(id))
            {
                throw new ArgumentException("`Category.CategoryName` should be equal than `CategoryName`");
            }
        }

        // DELETE: api/Category/5
        public IHttpActionResult Delete(Guid id)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                this.businessLogic.Category.DeleteById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
