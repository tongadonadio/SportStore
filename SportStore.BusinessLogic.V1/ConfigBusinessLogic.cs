using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportStore.BusinessLogic.V1.Auth;
using SportStore.Model;
using SportStore.Repository;

namespace SportStore.BusinessLogic.V1
{
    public class ConfigBusinessLogic : IConfigBusinessLogic
    {
        private ISportStoreRepository repository;

        public ConfigBusinessLogic(ISportStoreRepository repository)
        {
            this.repository = repository;
        }

        public float GetDotPrice()
        {
            try
            {
                return float.Parse(this.repository.ConfigRepository.Get("DotPrice"));
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid config value", ex);
            }
        }

        [AuthRequired(RequiredRole = RoleName.Administrator)]
        public void SetDotPrice(float value)
        {
            this.repository.ConfigRepository.Set("DotPrice", value.ToString());
        }

        public List<Guid> GetDotBlackList()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Guid>>(this.repository.ConfigRepository.Get("DotBlackList"));
            }
            catch
            {
                return new List<Guid>();
            }
        }

        public void SetDotBlackList(List<Guid> value)
        {
            this.repository.ConfigRepository.Set("DotBlackList", JsonConvert.SerializeObject(value));
        }

        public void AddProductToDotBlackList(Guid productCode)
        {
            var dotBlackList = GetDotBlackList();

            if (!dotBlackList.Any(guid => guid == productCode))
            {
                dotBlackList.Add(productCode);

                SetDotBlackList(dotBlackList);
            }
            else
            {
                throw new Exception("Product is already black listed");
            }
        }

        public void RemoveProductFromDotBlackList(Guid productCode)
        {
            var dotBlackList = GetDotBlackList();

            if (dotBlackList.Any(guid => guid == productCode))
            {
                dotBlackList.Remove(productCode);

                SetDotBlackList(dotBlackList);
            }
            else
            {
                throw new Exception("Product is not black listed");
            }
        }

        public bool GetPluginsEnabled()
        {
            try
            {
                return bool.Parse(this.repository.ConfigRepository.Get("PluginsEnabled"));
            }
            catch (Exception)
            {
                //throw new Exception("Invalid config value", ex);
                return false;
            }
        }

        public void SetPluginsEnabled(bool value)
        {
            this.repository.ConfigRepository.Set("PluginsEnabled", value.ToString());
        }
    }
}
