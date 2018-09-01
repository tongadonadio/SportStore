using System;
using System.Collections.Generic;
using System.Linq;

using SportStore.BusinessLogic.V1.Auth;
using SportStore.Model;
using SportStore.Repository;

namespace SportStore.BusinessLogic.V1
{
    public class CartBusinessLogic : ICartBusinessLogic
    {
        private IAuthBusinessLogic authBusinessLogic;
        private IConfigBusinessLogic configBusinessLogic;
        private ISportStoreRepository repository;
        
        public CartBusinessLogic(IAuthBusinessLogic authBusinessLogic, IConfigBusinessLogic configBusinessLogic, ISportStoreRepository repository)
        {
            this.authBusinessLogic = authBusinessLogic;
            this.configBusinessLogic = configBusinessLogic;
            this.repository = repository;
        }

        [AuthRequired]
        public Cart GetForCurrentSession()
        {
            return this.repository.CartRepository.GetById(this.authBusinessLogic.CurrentSession.User.Cart.Id);
        }

        [AuthRequired]
        public Cart AddProduct(Product product, int quantity)
        {
            var cart = this.GetForCurrentSession();

            try
            {
                var productInCart = TryToFindProductInCart(cart, product);
                productInCart.Quantity += quantity;
            }
            catch // TODO: Only catch the exception thrown by TryToFindProductInCart
            {
                cart.Products.Add(new ProductInCart(product, quantity));
            }

            this.repository.CartRepository.Update(cart);

            return cart;
        }

        [AuthRequired]
        public Cart RemoveProduct(Product product, int quantity)
        {
            var cart = this.GetForCurrentSession();
            var productInCart = TryToFindProductInCart(cart, product);

            productInCart.Quantity = Math.Max(0, productInCart.Quantity - quantity);
            
            this.repository.CartRepository.Update(cart);

            return cart;
        }

        private ProductInCart TryToFindProductInCart(Cart cart, Product product)
        {
            try
            {
                return cart.Products.Single(pb => pb.Product.Code == product.Code); 
            }
            catch
            {
                throw new Exception("Product is not in the cart"); // TODO: Throw custom exception
            }
        }

        [AuthRequired]
        public Purchase CheckOut(PaymentMethod paymentMethod, ShippingAddress shippingAddress)
        {
            var cart = this.GetForCurrentSession();

            if (cart.Products.Count > 0)
            {
                var purchase = CreatePurchaseFromCartForCurrentSession(cart, paymentMethod, shippingAddress);

                ResetCartForCurrentSession(cart);

                return purchase;
            }
            else
            {
                throw new Exception("Cannot checkout an empty cart");
            }
        }

        private Purchase CreatePurchaseFromCartForCurrentSession(Cart cart, PaymentMethod paymentMethod, ShippingAddress shippingAddress)
        {
            var purchasedProductsList = CreatePurchasedProductsListFromCartForCurrentSession(cart);

            Purchase purchase = new Purchase()
            {
                Id = Guid.NewGuid(),
                DateTime = DateTime.Now,
                Products = purchasedProductsList,
                PaymentMethod = paymentMethod,
                ShippingAddress = shippingAddress,
                User = this.authBusinessLogic.CurrentSession.User
            };

            this.repository.PurchaseRepository.Add(purchase);

            PurchaseDotsForUserInCurrentSession(purchase);

            return purchase;
        }

        private List<PurchasedProduct> CreatePurchasedProductsListFromCartForCurrentSession(Cart cart)
        {
            var purchasedProductsList = new List<PurchasedProduct>();

            foreach (var productInCart in cart.Products)
            {
                var productInRepository = this.repository.ProductRepository.GetById(productInCart.Product.Code);

                productInRepository.Stock -= productInCart.Quantity;
                
                if (productInRepository.Stock >= 0)
                {
                    productInCart.Product.Stock = productInRepository.Stock;

                    purchasedProductsList.Add(new PurchasedProduct(productInCart));
                }
                else
                {
                    throw new Exception("There is not enough product's stock: " + productInRepository.Code + " - " + productInRepository.Name);
                }
            }

            foreach (var productInCart in cart.Products)
            {
                this.repository.ProductRepository.Update(productInCart.Product);
            }

            return purchasedProductsList;
        }

        private void PurchaseDotsForUserInCurrentSession(Purchase purchase)
        {
            var currentSessionUser = this.authBusinessLogic.CurrentSession.User;
            var productsNotInDotBlackListTotal = PurchaseProductsNotInBlackListTotal(purchase);

            currentSessionUser.Dots += (int)Math.Floor(productsNotInDotBlackListTotal / this.configBusinessLogic.GetDotPrice());

            this.repository.UserRepository.Update(currentSessionUser);
        }

        private float PurchaseProductsNotInBlackListTotal(Purchase purchase)
        {
            var dotBlackList = this.configBusinessLogic.GetDotBlackList();
            var productsNotInDotBlackListTotal = 0f;

            foreach (var purchasedProduct in purchase.Products)
            {
                if (!dotBlackList.Contains(purchasedProduct.Product.Code))
                {
                    productsNotInDotBlackListTotal += purchasedProduct.Price * purchasedProduct.Quantity;
                }
            }

            return productsNotInDotBlackListTotal;
        }

        private void ResetCartForCurrentSession(Cart cart)
        {
            cart.Products.Clear();

            this.repository.CartRepository.Update(cart);
        }
    }
}

