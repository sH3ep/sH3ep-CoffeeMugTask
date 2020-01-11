﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMugTask.Persistance.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public Repository(DbContext context)
        {
            Context = context;
            context.Database.EnsureCreated();
            Entities = Context.Set<TEntity>();
        }

        protected DbContext Context { get; }

        protected DbSet<TEntity> Entities { get; }

        public virtual TEntity Add(TEntity entity)
        {
            Entities.Add(entity);
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            Entities.Update(entity);
            return entity;
        }

        public async Task<TEntity> Get(Guid id)
        {
            return await Entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Entities.ToListAsync();
        }

        public virtual void Remove(TEntity entity)
        {
            Entities.Remove(entity);
        }
    }
}
