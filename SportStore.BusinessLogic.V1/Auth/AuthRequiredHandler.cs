using Microsoft.Practices.Unity.InterceptionExtension;
using System;

using SportStore.BusinessLogic;

namespace SportStore.BusinessLogic.V1
{
    public class AuthRequiredHandler : ICallHandler
    {
        private IAuthBusinessLogic authBusinessLogic;
        private string requiredRole;

        public int Order { get; set; }

        public AuthRequiredHandler(IAuthBusinessLogic authBusinessLogic, string requiredRole)
        {
            this.authBusinessLogic = authBusinessLogic;
            this.requiredRole = requiredRole;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            ThrowIfThereIsNoUserAuthenticated();
            ThrowIfAuthenticatedUserDoesntHaveTheRequiredRole();

            return getNext().Invoke(input, getNext);
        }

        private void ThrowIfThereIsNoUserAuthenticated()
        {
            if (this.authBusinessLogic.CurrentSession == null)
            {
                throw new Exception("Unauthorized");
            }
        }

        private void ThrowIfAuthenticatedUserDoesntHaveTheRequiredRole()
        {
            if (this.requiredRole != null)
            {
                if (this.authBusinessLogic.CurrentSession.User.Role == null || this.authBusinessLogic.CurrentSession.User.Role.Name != this.requiredRole)
                {
                    throw new Exception("Unauthorized");
                }
            }
        }
    }
}
