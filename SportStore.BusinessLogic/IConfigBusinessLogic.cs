using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SportStore.Model;

namespace SportStore.BusinessLogic
{
    public interface IConfigBusinessLogic
    {
        float GetDotPrice();
        void SetDotPrice(float value);

        List<Guid> GetDotBlackList();
        void SetDotBlackList(List<Guid> value);
        void AddProductToDotBlackList(Guid productCode);
        void RemoveProductFromDotBlackList(Guid productCode);

        bool GetPluginsEnabled();
        void SetPluginsEnabled(bool value);
    }
}
