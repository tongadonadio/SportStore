﻿using SportStore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportStore.BusinessLogic
{
    public interface ICategoryBusinessLogic : ICRUDBusinessLogic<Category, Guid>
    {
    }
}
