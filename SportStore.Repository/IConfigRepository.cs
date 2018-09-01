using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Repository
{
    public interface IConfigRepository
    {
        string Get(string key);
        void Set(string key, string value);
        void Delete(string key);
    }
}
