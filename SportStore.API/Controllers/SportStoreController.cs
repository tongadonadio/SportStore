using System;
using System.Linq;
using System.Web.Http;

using SportStore.BusinessLogic;

namespace SportStore.API.Controllers
{
    public class SportStoreController : ApiController
    {
        protected ISportStoreBusinessLogic businessLogic;

        protected void AuthenticateWithTokenInHeaders()
        {
            try
            {
                var headerValues = Request.Headers.GetValues("X-SPORTSTORE-AUTH-TOKEN");
                var authToken = headerValues.FirstOrDefault();

                if (authToken != null)
                {
                    this.businessLogic.Auth.Login(authToken);
                }
            }
            catch
            {
            }
        }
    }
}