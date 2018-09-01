using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.Repository
{
    public interface ICRUDRepository<ElementType, IdType>
    {
        ElementType GetById(IdType id);
        IEnumerable<ElementType> All();
        IEnumerable<ElementType> Find(Predicate<ElementType> p);
        void Add(ElementType element);
        void Update(ElementType element);
        void Remove(ElementType element);
    }
}
