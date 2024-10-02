﻿using Store.Data.Entities;
using Store.Repository.Specification;
using Store.Repository.Specification.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Repostories
{
   public interface IGenereicRepository<TEntity ,TKey> where TEntity : BaseEntity<TKey>
    {
        Task<TEntity> GetByIdAsync(TKey? id);
        Task<IReadOnlyList<TEntity>> GetAllAsync();

        Task<TEntity> GetWithSpecificationByIdAsync(ISpecification<TEntity> specs);
        Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specs);
        Task<int> CountSpecificationAsyn(ISpecification<TEntity> spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
         void Delete(TEntity entity);
       // Task GetWithSpecificationByIdAsync(OrderWithPaymentIntentSpecification specs);
    }
}
