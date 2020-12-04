using API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task Add(TEntity obj);
        Task<bool> Exists(short id);
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById<T>(T id);
        void Remove(TEntity obj);
        void Update(TEntity obj);
    }

    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly Context _context;

        public RepositoryBase(Context context) =>
            _context = context;

        public virtual async Task Add(TEntity obj) => 
            await _context.Set<TEntity>().AddAsync(obj);

        public virtual async Task<IEnumerable<TEntity>> GetAll() =>
            await _context.Set<TEntity>().ToListAsync();

        public virtual async Task<TEntity> GetById<T>(T id) =>
            await _context.Set<TEntity>().FindAsync(id);

        public virtual void Remove(TEntity obj) => 
            _context.Set<TEntity>().Remove(obj);

        public virtual void Update(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property("DataCriacao").IsModified = false;
        }

        public virtual async Task<bool> Exists(short id) =>
            await _context.Set<TEntity>().FindAsync(id) != null;
    }
}
