using System;
using System.Data.Entity;
using System.Reflection;

using SportStore.BusinessLogic;
using SportStore.BusinessLogic.V1;
using SportStore.Model;
using SportStore.Repository.Entity;

namespace SportStore.BusinessLogic.V1.Tests
{
    public abstract class SportStoreIntegrationTest
    {
        protected ISportStoreBusinessLogic businessLogic;

        protected virtual bool LoginUserOnSetUp { get { return true; } }
        protected virtual bool LoggedUserShouldBeAdministrator { get { return false; } }

        public SportStoreIntegrationTest()
        {
            this.businessLogic = new SportStoreBusinessLogic<SportStoreRepository>();
        }

        public virtual void SetUp()
        {
            this.ResetBusinessLogicRepositories();

            this.businessLogic.SetUpInitialData();

            if (this.LoginUserOnSetUp)
            {
                if (this.LoggedUserShouldBeAdministrator)
                {
                    this.businessLogic.Auth.Login("admin", "****");
                }
                else
                {
                    var user = new User()
                    {
                        UserName = "test",
                        Password = "***",
                        FirstName = "SportStore",
                        LastName = "Test",
                        Role = new Role() { Name = RoleName.Default }
                    };

                    this.businessLogic.Auth.Login("admin", "****");
                    this.businessLogic.User.Create(user);
                    this.businessLogic.Auth.Logout();
                    this.businessLogic.Auth.Login("test", "***");
                }
            }
        }

        private void ResetBusinessLogicRepositories()
        {
            var businessLogic = this.businessLogic as SportStoreBusinessLogic<SportStoreRepository>;
            var repository = ReadValueOfPrivateField(businessLogic, typeof(SportStoreBusinessLogic<SportStoreRepository>), "repository") as SportStoreRepository;
            var dbContext = ReadValueOfPrivateField(repository, typeof(SportStoreRepository), "dbContext") as DbContext;

            dbContext.Database.Delete();
        }

        private object ReadValueOfPrivateField(object instance, Type type, string field)
        {
            FieldInfo fieldInfo = type.GetField(field, BindingFlags.Instance | BindingFlags.NonPublic);
            return fieldInfo.GetValue(instance);
        }
    }
}
