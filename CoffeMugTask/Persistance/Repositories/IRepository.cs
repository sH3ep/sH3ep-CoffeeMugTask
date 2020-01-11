using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMugTask.Persistance.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(Guid id);

        Task<IEnumerable<TEntity>> GetAll();

        TEntity Add(TEntity entity);

        TEntity Update(TEntity entity);

        void Remove(TEntity entity);
    }
}
