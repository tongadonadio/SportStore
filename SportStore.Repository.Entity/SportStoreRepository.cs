namespace SportStore.Repository.Entity
{
    public class SportStoreRepository : ISportStoreRepository
    {
        private SportStoreDbContext dbContext;

        private ICartRepository cartRepository;
        private ICategoryRepository categoryRepository;
        private IConfigRepository configRepository;
        private IManufacturerRepository manufacturerRepository;
        private IPaymentMethodRepository paymentRepository;
        private IProductRepository productRepository;
        private IPurchaseRepository purchaseRepository;
        private IPurchasedProductRepository purchasedProductRepository;
        private IReviewRepository reviewRepository;
        private IRoleRepository roleRepository;
        private ISessionRepository sessionRepository;
        private IShippingAddressRepository shippingAddressRepository;
        private IUserRepository userRepository;

        public ICartRepository CartRepository { get { return cartRepository; } }
        public ICategoryRepository CategoryRepository { get { return categoryRepository; } }
        public IConfigRepository ConfigRepository { get { return configRepository; } }
        public IManufacturerRepository ManufacturerRepository { get { return manufacturerRepository; } }
        public IPaymentMethodRepository PaymentRepository { get { return paymentRepository; } }
        public IProductRepository ProductRepository { get { return productRepository; } }
        public IPurchaseRepository PurchaseRepository { get { return purchaseRepository; } }
        public IPurchasedProductRepository PurchasedProductRepository { get { return purchasedProductRepository; } }
        public IReviewRepository ReviewRepository { get { return reviewRepository; } }
        public IRoleRepository RoleRepository { get { return roleRepository; } }
        public ISessionRepository SessionRepository { get { return sessionRepository; } }
        public IShippingAddressRepository ShippingAddressRepository { get { return shippingAddressRepository; } }
        public IUserRepository UserRepository { get { return userRepository; } }

        public SportStoreRepository()
        {
            this.dbContext = new SportStoreDbContext();

            this.cartRepository = new CartRepository();
            this.categoryRepository = new CategoryRepository();
            this.configRepository = new ConfigRepository();
            this.manufacturerRepository = new ManufacturerRepository();
            this.paymentRepository = new PaymentMethodRepository();
            this.productRepository = new ProductRepository();
            this.purchaseRepository = new PurchaseRepository();
            this.purchasedProductRepository = new PurchasedProductRepository();
            this.reviewRepository = new ReviewRepository();
            this.roleRepository = new RoleRepository();
            this.sessionRepository = new SessionRepository();
            this.shippingAddressRepository = new ShippingAddressRepository();
            this.userRepository = new UserRepository();
        }
    }
}
