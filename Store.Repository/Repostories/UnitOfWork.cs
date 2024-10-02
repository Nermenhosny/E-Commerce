using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repostories
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(StoreDbContext context)
        {
            Context = context;
        }
        private Hashtable Repositories;

        public StoreDbContext Context { get; }

        public async Task<int> CompleteAsync()
        => await Context.SaveChangesAsync();

        public IGenereicRepository<TEntity, Tkey> Repository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
           if(Repositories == null)
            {
                Repositories = new Hashtable();
            }
           var entityKey=typeof(TEntity).Name;//Reposotory<Product , int>
            if (!Repositories.ContainsKey(entityKey))
            {
               var repositoryType = typeof(GenereicRepository<, >);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity) , typeof(Tkey)), Context);
                Repositories.Add(entityKey, repositoryInstance);
            }
            return (IGenereicRepository < TEntity, Tkey >) Repositories[entityKey];
        }

        
    }
}
