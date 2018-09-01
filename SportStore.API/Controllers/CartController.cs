using System;
using System.Web.Http;
using System.Web.Http.Cors;

using SportStore.BusinessLogic;
using SportStore.Model;

namespace SportStore.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CartController : SportStoreController
    {
        public CartController(ISportStoreBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        // GET: api/Cart
        public IHttpActionResult Get()
        {
            AuthenticateWithTokenInHeaders();

            var cart = this.businessLogic.Cart.GetForCurrentSession();

            return Ok(cart);
        }

        // PUT: api/Cart
        public IHttpActionResult Put([FromBody]ProductInCart productInCart)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                this.businessLogic.Cart.AddProduct(productInCart.Product, productInCart.Quantity);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Cart
        [HttpDelete]
        public IHttpActionResult Delete(Guid id, int quantity)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                this.businessLogic.Cart.RemoveProduct(this.businessLogic.Product.GetById(id), quantity);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Cart
        [HttpPost]
        [Route("api/Cart/CheckOut")]
        public IHttpActionResult CheckOut([FromBody]CheckOutArguments args)
        {
            AuthenticateWithTokenInHeaders();

            try
            {
                var purchase = this.businessLogic.Cart.CheckOut(args.PaymentMethod, args.ShippingAddress);

                return Ok(purchase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #region Classes to model request arguments

        public class CheckOutArguments
        {
            public PaymentMethod PaymentMethod { get; set; }
            public ShippingAddress ShippingAddress { get; set; }

            public CheckOutArguments(PaymentMethod paymentMethod, ShippingAddress shippingAddress)
            {
                this.PaymentMethod = paymentMethod;
                this.ShippingAddress = shippingAddress;
            }
        }

        #endregion
    }
}
