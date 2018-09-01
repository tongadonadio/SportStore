using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Repository
{
    public interface ISportStoreRepository
    {
        ICartRepository CartRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IConfigRepository ConfigRepository { get; }
        IManufacturerRepository ManufacturerRepository { get; }
        IPaymentMethodRepository PaymentRepository { get; }
        IProductRepository ProductRepository { get; }
        IPurchaseRepository PurchaseRepository { get; }
        IPurchasedProductRepository PurchasedProductRepository { get; }
        IReviewRepository ReviewRepository { get; }
        IRoleRepository RoleRepository { get; }
        ISessionRepository SessionRepository { get; }
        IShippingAddressRepository ShippingAddressRepository { get; }
        IUserRepository UserRepository { get; }
    }
}
