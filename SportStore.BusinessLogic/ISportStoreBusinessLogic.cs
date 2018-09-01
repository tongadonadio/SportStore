using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.BusinessLogic
{
    public interface ISportStoreBusinessLogic
    {
        IAuthBusinessLogic Auth { get; }
        ICartBusinessLogic Cart { get; }
        ICategoryBusinessLogic Category { get; }
        IManagementBusinessLogic Management { get; }
        IManufacturerBusinessLogic Manufacturer { get; }
        IPaymentMethodBusinessLogic PaymentMethod { get; }
        IProductBusinessLogic Product { get; }
        IPurchaseBusinessLogic Purchase { get; }
        IReviewBusinessLogic Review { get; }
        IRoleBusinessLogic Role { get; }
        IShippingAddressBusinessLogic ShippingAddress { get; }
        IUserBusinessLogic User { get; }

        IConfigBusinessLogic Config { get; }
        IPluginBusinessLogic Plugin { get; }

        void SetUpInitialData();
    }
}
