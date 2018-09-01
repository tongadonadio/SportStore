using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

using SportStore.BusinessLogic;

namespace SportStore.BusinessLogic.V1.Auth
{
    public class AuthRequiredAttribute : HandlerAttribute
    {
        public string RequiredRole { get; set; }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return new AuthRequiredHandler(container.Resolve<IAuthBusinessLogic>(), this.RequiredRole);
        }
    }
}
