using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportStore.Model;

namespace SportStore.Repository.Entity
{
    public class ConfigRepository : IConfigRepository
    {
        public string Get(string key)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var configEntry = dbContext.ConfigDbSet.SingleOrDefault(e => e.Key == key);

                if (configEntry != null)
                {
                    return configEntry.Value;
                }
                else
                {
                    return null;
                }
            }
        }

        public void Set(string key, string value)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var configEntry = dbContext.ConfigDbSet.SingleOrDefault(e => e.Key == key);

                if (configEntry == null)
                {
                    dbContext.ConfigDbSet.Add(new ConfigEntry()
                    {
                        Key = key,
                        Value = value
                    });
                }
                else
                {
                    configEntry.Value = value;
                }

                dbContext.SaveChanges();
            }
        }

        public void Delete(string key)
        {
            using (var dbContext = new SportStoreDbContext())
            {
                var configEntry = dbContext.ConfigDbSet.SingleOrDefault(e => e.Key == key);

                if (configEntry == null)
                {
                    dbContext.ConfigDbSet.Remove(configEntry);
                }
            }
        }
    }
}
