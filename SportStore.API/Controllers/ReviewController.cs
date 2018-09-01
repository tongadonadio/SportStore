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
    public class ReviewController : SportStoreController
    {
        public ReviewController(ISportStoreBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        // GET: api/Review
        public IEnumerable<Review> Get()
        {
            AuthenticateWithTokenInHeaders();

            return this.businessLogic.Review.All();
        }

        // GET: api/Review/5
        public IHttpActionResult Get(Guid id)
        {
            AuthenticateWithTokenInHeaders();

            var review = this.businessLogic.Review.GetById(id);

            if (review != null)
            {
                return Ok(review);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Review
        public IHttpActionResult Post([FromBody]Review review)
        {
            try
            {
                Guid reviewName = this.businessLogic.Review.Create(review);

                return Ok(reviewName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Review/5
        public IHttpActionResult Put(Guid id, [FromBody]Review review)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                review.Id = id;

                this.businessLogic.Review.Update(review);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Review/5
        public IHttpActionResult Delete(Guid id)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                this.businessLogic.Review.DeleteById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
