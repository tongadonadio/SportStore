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
    public class RoleController : SportStoreController
    {
        public RoleController(ISportStoreBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        // GET: api/Role
        public IEnumerable<Role> Get()
        {
            AuthenticateWithTokenInHeaders();

            return this.businessLogic.Role.All();
        }

        // GET: api/Role/5
        public IHttpActionResult Get(string id)
        {
            AuthenticateWithTokenInHeaders();

            var role = this.businessLogic.Role.GetById(id);

            if (role != null)
            {
                return Ok(role);
            }
            else
            {
                return NotFound();
            }
        }

        // POST: api/Role
        public IHttpActionResult Post([FromBody]Role role)
        {
            try
            {
                var roleName = this.businessLogic.Role.Create(role);

                return Ok(roleName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Role/5
        public IHttpActionResult Put(string id, [FromBody]Role role)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                role.Name = id;

                this.businessLogic.Role.Update(role);

                return Ok(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Role/5
        public IHttpActionResult Delete(string id)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                this.businessLogic.Role.DeleteById(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
