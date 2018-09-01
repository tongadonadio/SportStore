using System;
using System.Collections.Generic;
using System.Linq;

using SportStore.BusinessLogic.V1.Log;
using SportStore.Log.Events;
using SportStore.Model;
using SportStore.Model.Notifications;
using SportStore.Repository;

namespace SportStore.BusinessLogic.V1.Auth
{
    public class AuthBusinessLogic : IAuthBusinessLogic
    {
        private ISportStoreRepository repository;

        public Session CurrentSession { get; private set; }

        public AuthBusinessLogic(ISportStoreRepository repository)
        {
            this.repository = repository;
        }

        public void SignUp(User user)
        {
            this.repository.UserRepository.Add(user);
        }

        public void Login(string authToken)
        {
            var session = this.repository.SessionRepository.GetByToken(authToken);

            if (session == null) throw new Exception("Invalid token");

            this.CurrentSession = session;
        }

        public void Login(string userName, string password)
        {
            var users = this.repository.UserRepository.Find(u => u.UserName == userName && u.Password == password);

            if (users.Count() == 0) throw new Exception("UserName or Password are wrong");

            this.CurrentSession = CreateSession(users.First());

            SportStoreLog.Instance.WriteEvent(new LoginEvent(this.CurrentSession.User));
        }

        private Session CreateSession(User user)
        {
            var session = new Session(user);

            session.Notifications = CheckNotificationsForSession(session);

            this.repository.SessionRepository.Add(session);

            return session;
        }

        private List<Notification> CheckNotificationsForSession(Session session)
        {
            List<Notification> notifications = new List<Notification>();

            CheckForUnfinishedPurchaseNotifications(session, notifications);
            CheckForUnreviewedPurchasedProductsNotification(session, notifications);

            return notifications;
        }

        private void CheckForUnfinishedPurchaseNotifications(Session session, List<Notification> notifications)
        {
            if (session.User.Cart.Products.Count() > 0)
            {
                notifications.Add(new UnfinishedPurchaseNotification());
            }
        }

        private void CheckForUnreviewedPurchasedProductsNotification(Session session, List<Notification> notifications)
        {
            var purchases = this.repository.PurchaseRepository.Find(p => p.User.UserName == session.User.UserName);
            var unreviewedPurchasedProducts = new List<PurchasedProduct>();

            foreach (var purchase in purchases)
            {
                foreach (var purchasedProduct in purchase.Products)
                {
                    var reviewForPurchasedProduct = this.repository.ReviewRepository.GetByPurchasedProductId(purchasedProduct.Id);

                    if (reviewForPurchasedProduct == null)
                    {
                        unreviewedPurchasedProducts.Add(purchasedProduct);
                    }
                }
            }

            if (unreviewedPurchasedProducts.Count() > 0)
            {
                notifications.Add(new UnreviewedPurchasedProductsNotification(unreviewedPurchasedProducts));
            }
        }

        public void Logout()
        {
            this.CurrentSession = null;
        }
    }
}
