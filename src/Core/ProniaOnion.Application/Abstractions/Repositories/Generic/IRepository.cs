﻿using ProniaOnion.Domain.Entities;
using System.Linq.Expressions;


namespace ProniaOnion.Application.Abstractions.Repositories
{
    public interface IRepository<T> where T : BaseEntity,new()
    {
        IQueryable<T> GetAll(
           Expression<Func<T, bool>>? expression = null,
           Expression<Func<T, object>>? orderExpression = null,
            int skip = 0,
           //int take=int.MaxValue, 
           int take = 0,
           bool isDescending = false,
           bool isTracking = false,
           params string[]? includes
           );
        Task<T> GetByIdAsync(int id, params string[] includes);
        Task AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
        Task<int> SaveChangesAsync();
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);

    }
}