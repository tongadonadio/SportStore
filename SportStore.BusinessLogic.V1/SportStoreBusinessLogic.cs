using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;

using SportStore.BusinessLogic.V1.Auth;
using SportStore.BusinessLogic.V1.Management;
using SportStore.Log;
using SportStore.Log.PlainText;
using SportStore.Model;
using SportStore.Repository;

namespace SportStore.BusinessLogic.V1
{
    public class SportStoreBusinessLogic<T> : ISportStoreBusinessLogic where T : ISportStoreRepository, new()
    {
        private T repository;
        private UnityContainer unityContainer;

        public IAuthBusinessLogic Auth { get; }
        public ICartBusinessLogic Cart { get; }
        public ICategoryBusinessLogic Category { get; }
        public IManagementBusinessLogic Management { get; }
        public IManufacturerBusinessLogic Manufacturer { get; }
        public IPaymentMethodBusinessLogic PaymentMethod { get; }
        public IProductBusinessLogic Product { get; }
        public IPurchaseBusinessLogic Purchase { get; }
        public IReviewBusinessLogic Review { get; }
        public IRoleBusinessLogic Role { get; }
        public IShippingAddressBusinessLogic ShippingAddress { get; }
        public IUserBusinessLogic User { get; }

        public IConfigBusinessLogic Config { get; }
        public IPluginBusinessLogic Plugin { get; }

        public SportStoreBusinessLogic()
        {
            this.repository = new T();

            this.unityContainer = new UnityContainer();

            InitializeInterceptors();
            InitializeDependencyInjectionInstances();

            LoadManagementBusinessLogic();

            this.Auth = unityContainer.Resolve<IAuthBusinessLogic>();
            this.Cart = unityContainer.Resolve<ICartBusinessLogic>();
            this.Category = unityContainer.Resolve<ICategoryBusinessLogic>();
            this.Config = unityContainer.Resolve<IConfigBusinessLogic>();
            this.Manufacturer = unityContainer.Resolve<IManufacturerBusinessLogic>();
            this.Management = unityContainer.Resolve<IManagementBusinessLogic>();
            this.PaymentMethod = unityContainer.Resolve<IPaymentMethodBusinessLogic>();
            this.Product = unityContainer.Resolve<IProductBusinessLogic>();
            this.Purchase = unityContainer.Resolve<IPurchaseBusinessLogic>();
            this.Review = unityContainer.Resolve<IReviewBusinessLogic>();
            this.Role = unityContainer.Resolve<IRoleBusinessLogic>();
            this.ShippingAddress = unityContainer.Resolve<IShippingAddressBusinessLogic>();
            this.User = unityContainer.Resolve<IUserBusinessLogic>();

            if (this.Config.GetPluginsEnabled())
            {
                this.Plugin = new PluginBusinessLogic("Plugin");
            }
        }

        private void InitializeInterceptors()
        {
            this.unityContainer.AddNewExtension<Interception>();

            this.unityContainer.Configure<Interception>().SetInterceptorFor<ICartBusinessLogic>(new InterfaceInterceptor());
            this.unityContainer.Configure<Interception>().SetInterceptorFor<ICategoryBusinessLogic>(new InterfaceInterceptor());
            this.unityContainer.Configure<Interception>().SetInterceptorFor<IConfigBusinessLogic>(new InterfaceInterceptor());
            this.unityContainer.Configure<Interception>().SetInterceptorFor<IManufacturerBusinessLogic>(new InterfaceInterceptor());
            this.unityContainer.Configure<Interception>().SetInterceptorFor<IPaymentMethodBusinessLogic>(new InterfaceInterceptor());
            this.unityContainer.Configure<Interception>().SetInterceptorFor<IProductBusinessLogic>(new InterfaceInterceptor());
            this.unityContainer.Configure<Interception>().SetInterceptorFor<IPurchaseBusinessLogic>(new InterfaceInterceptor());
            this.unityContainer.Configure<Interception>().SetInterceptorFor<IReviewBusinessLogic>(new InterfaceInterceptor());
            this.unityContainer.Configure<Interception>().SetInterceptorFor<IRoleBusinessLogic>(new InterfaceInterceptor());
            this.unityContainer.Configure<Interception>().SetInterceptorFor<IShippingAddressBusinessLogic>(new InterfaceInterceptor());
            this.unityContainer.Configure<Interception>().SetInterceptorFor<IUserBusinessLogic>(new InterfaceInterceptor());
        }

        private void InitializeDependencyInjectionInstances()
        {
            this.unityContainer.RegisterInstance<IAuthBusinessLogic>(new AuthBusinessLogic(this.repository));
            this.unityContainer.RegisterInstance<IConfigBusinessLogic>(new ConfigBusinessLogic(this.repository));

            this.unityContainer.RegisterInstance<ICartBusinessLogic>(new CartBusinessLogic(unityContainer.Resolve<IAuthBusinessLogic>(), unityContainer.Resolve<IConfigBusinessLogic>(), this.repository));
            this.unityContainer.RegisterInstance<ICategoryBusinessLogic>(new CategoryBusinessLogic(this.repository));
            this.unityContainer.RegisterInstance<IManufacturerBusinessLogic>(new ManufacturerBusinessLogic(this.repository));
            this.unityContainer.RegisterInstance<IPaymentMethodBusinessLogic>(new PaymentMethodBusinessLogic(this.repository));
            this.unityContainer.RegisterInstance<IProductBusinessLogic>(new ProductBusinessLogic(this.repository));
            this.unityContainer.RegisterInstance<IPurchaseBusinessLogic>(new PurchaseBusinessLogic(this.repository));
            this.unityContainer.RegisterInstance<IRoleBusinessLogic>(new RoleBusinessLogic(this.repository));
            this.unityContainer.RegisterInstance<IReviewBusinessLogic>(new ReviewBusinessLogic(unityContainer.Resolve<IAuthBusinessLogic>(), this.repository));
            this.unityContainer.RegisterInstance<IShippingAddressBusinessLogic>(new ShippingAddressBusinessLogic(this.repository));
            this.unityContainer.RegisterInstance<IUserBusinessLogic>(new UserBusinessLogic(this.repository));
        }

        private void LoadManagementBusinessLogic()
        {
            // TODO: Check if Management Assembly exists and load the class dinamically
            var managementBusinessLogic = new ManagementBusinessLogic(this.repository);

            this.unityContainer.RegisterInstance<IManagementBusinessLogic>(managementBusinessLogic);
        }

        public void SetUpInitialData()
        {
            SetUpConfig();

            SetUpCategory();
            SetUpManufacturers();
            SetUpRole();

            SetUpUser();
        }

        private void SetUpConfig()
        {
            this.repository.ConfigRepository.Set("DotPrice", "150");
            this.repository.ConfigRepository.Set("PluginsEnabled", "true");
        }

        private void SetUpCategory()
        {
            if (this.repository.CategoryRepository.Find(c => c.Name == "Default").Count() == 0)
            {
                this.repository.CategoryRepository.Add(new Category()
                {
                    Name = "Default",
                    Description = "Default category"
                });
            }
        }

        private void SetUpManufacturers()
        {
            if (this.repository.ManufacturerRepository.Find(c => c.Name == "Unspecified").Count() == 0)
            {
                this.repository.ManufacturerRepository.Add(new Manufacturer()
                {
                    Name = "Unspecified"
                });
            }
        }

        private void SetUpRole()
        {
            if (this.repository.RoleRepository.Find(r => r.Name == RoleName.Administrator).Count() == 0)
            {
                this.repository.RoleRepository.Add(new Role()
                {
                    Name = RoleName.Administrator,
                    Description = "Default administrator user"
                });

                this.repository.RoleRepository.Add(new Role()
                {
                    Name = RoleName.Default,
                    Description = "Default non-administrator user"
                });
            }
        }

        private void SetUpUser()
        {
            if (this.repository.UserRepository.Find(u => u.UserName == "admin").Count() == 0)
            {
                var cartGuid = Guid.NewGuid();

                this.repository.CartRepository.Add(new Cart()
                {
                    Id = cartGuid,
                    Products = new List<ProductInCart>()
                });
                this.repository.UserRepository.Add(new User()
                {
                    UserName = "admin",
                    Password = "****",
                    FirstName = "Sport",
                    LastName = "Store",
                    Email = "admin@sportstore.com",
                    Address = "ABC 1234",
                    Role = new Role() { Name = RoleName.Administrator },
                    Cart = new Cart() { Id = cartGuid }
                });
            }
        }
    }
}
