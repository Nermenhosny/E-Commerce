using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities;
using Store.Repository.Repostories;
using Store.Repository.Specification;
using Store.Repository.Specification.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Interfaces
{
    public class GenereicRepository<TEntity, TKey> : IGenereicRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public GenereicRepository(StoreDbContext context) 
        {
            Context = context;
        }

        public StoreDbContext Context { get; }

        public async Task AddAsync(TEntity entity)
        =>await Context.Set<TEntity>().AddAsync(entity);

      

        public void Delete(TEntity entity)
        => Context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        => await Context.Set<TEntity>().ToListAsync();

        public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specs)
        
         =>await SpecificationEvaluator<TEntity , TKey>.GetQuery(Context.Set<TEntity>(), specs).ToListAsync();    
        

        public async Task<TEntity> GetByIdAsync(TKey id)
            => await Context.Set<TEntity>().FindAsync(id);

        public async Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> specs)
        => await SpecificationEvaluator<TEntity, TKey>.GetQuery(Context.Set<TEntity>(), specs).FirstOrDefaultAsync();

        public async Task<int> CountSpecificationAsyn(ISpecification<TEntity> spec)
               => await SpecificationEvaluator<TEntity, TKey>.GetQuery(Context.Set<TEntity>(), spec).CountAsync();

        public void Update(TEntity entity)
       => Context.Set<TEntity>().Update(entity);

      
    }
}
