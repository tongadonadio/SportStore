using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.BusinessLogic
{
    public interface ICRUDBusinessLogic<ElementType, IdType>
    {
        ElementType GetById(IdType id);
        IEnumerable<ElementType> All();
        IEnumerable<ElementType> Find(Predicate<ElementType> p);
        IdType Create(ElementType element);
        void Update(ElementType element);
        void Delete(ElementType element);
        void DeleteById(IdType id);
    }
}
