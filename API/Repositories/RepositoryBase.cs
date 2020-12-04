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
        Task Remove(TEntity obj);
        Task Update(TEntity obj);
    }

    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly Context _context;

        public RepositoryBase(Context context) => 
            _context = context;

        public virtual async Task Add(TEntity obj)
        {
            //var transaction = _context.Database.BeginTransaction();

            try
            {
                await _context.Set<TEntity>().AddAsync(obj);
                //await _context.SaveChangesAsync();
                //transaction.Commit();
            }
            catch (Exception ex)
            {
                //transaction.Rollback();
                throw new Exception(ex.Message, ex?.InnerException);
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll() =>
            await _context.Set<TEntity>().ToListAsync();

        public virtual async Task<TEntity> GetById<T>(T id) =>
            await _context.Set<TEntity>().FindAsync(id);

        public virtual async Task Remove(TEntity obj)
        {
            //var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.Set<TEntity>().Remove(obj);
                //await _context.SaveChangesAsync();
                //transaction.Commit();
            }
            catch (Exception)
            {
                //transaction.Rollback();
            }
        }

        public virtual async Task Update(TEntity obj)
        {
            //var transaction = _context.Database.BeginTransaction();

            try
            {
                _context.Entry(obj).State = EntityState.Modified;
                _context.Entry(obj).Property("DataCriacao").IsModified = false;
                //await _context.SaveChangesAsync();
                //transaction.Commit();
            }
            catch (Exception ex)
            {
                //transaction.Rollback();
                throw new Exception(ex.Message, ex?.InnerException);
            }
        }

        public virtual async Task<bool> Exists(short id) =>
            await _context.Set<TEntity>().FindAsync(id) != null;      
    }
}
