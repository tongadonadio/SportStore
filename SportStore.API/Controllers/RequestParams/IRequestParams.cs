using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportStore.API.Controllers.RequestParams
{
    public interface IRequestParams<T>
    {
        Predicate<T> GetPredicate();
    }
}