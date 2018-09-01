using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Repository.Temporary
{
    public class TemporarySportStoreRepository : ISportStoreRepository
    {
        private ICartRepository cartRepository = new TemporaryCartRepository();
        private IProductRepository productRepository = new TemporaryProductRepository();
        private IRoleRepository roleRepository = new TemporaryRoleRepository();
        private IShippingAddressRepository shippingAddressRepository = new TemporaryShippingAddressRepository();
        private IUserRepository userRepository = new TemporaryUserRepository();

        public ICartRepository CartRepository
        {
            get
            {
                return cartRepository;
            }
        }

        public IProductRepository ProductRepository
        {
            get
            {
                return productRepository;
            }
        }

        public IRoleRepository RoleRepository
        {
            get
            {
                return roleRepository;
            }
        }

        public IShippingAddressRepository ShippingAddressRepository
        {
            get
            {
                return shippingAddressRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                return userRepository;
            }
        }
    }
}
