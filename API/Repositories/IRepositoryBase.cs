using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task Add(TEntity obj);
        Task<bool> Exists<T>(T id);
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById<T>(T id);
        void Remove(TEntity obj);
        void Update(TEntity obj);
    }    
}
