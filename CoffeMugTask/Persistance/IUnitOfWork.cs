using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMugTask.Persistance
{
    public interface IUnitOfWork
    {
        Task<int> Complete();

        Task Dispose();
    }
}
