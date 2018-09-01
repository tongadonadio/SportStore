using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

using SportStore.BusinessLogic;
using SportStore.Model;

namespace SportStore.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : SportStoreController
    {
        public UserController(ISportStoreBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        // GET: api/User
        public IEnumerable<User> Get()
        {
            AuthenticateWithTokenInHeaders();

            return this.businessLogic.User.All();
        }

        // GET: api/User/5
        public IHttpActionResult Get(string id)
        {
            AuthenticateWithTokenInHeaders();

            var user = this.businessLogic.User.GetById(id);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/User
        public IHttpActionResult Post([FromBody]User user)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                string userName = this.businessLogic.User.Create(user);

                return Ok(userName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/User/5
        public IHttpActionResult Put(string id, [FromBody]User user)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                ThrowIfPutArgumentsAreWrong(id, user);

                this.businessLogic.User.Update(user);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private void ThrowIfPutArgumentsAreWrong(string userName, User user)
        {
            if (!user.UserName.Equals(userName))
            {
                throw new ArgumentException("`user.UserName` should be equal than `userName`");
            }
        }

        // DELETE: api/User/5
        public IHttpActionResult Delete(string id)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                this.businessLogic.User.DeleteById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/User/SignUp
        [Route("api/User/SignUp")]
        public IHttpActionResult SignUp([FromBody]User user)
        {
            try
            {
                this.businessLogic.Auth.SignUp(user);
                this.businessLogic.Auth.Login(user.UserName, user.Password);

                return Ok(this.businessLogic.Auth.CurrentSession);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/User/Login
        [Route("api/User/Login")]
        public IHttpActionResult Login(string userName, string password)
        {
            try
            {
                this.businessLogic.Auth.Login(userName, password);

                return Ok(this.businessLogic.Auth.CurrentSession);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/User/Logout
        [Route("api/User/Logout")]
        public IHttpActionResult Logout()
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                this.businessLogic.Auth.Logout();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
