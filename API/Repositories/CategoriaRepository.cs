using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class CategoriaRepository : IRepositoryBase<Categoria>
    {
        private readonly Context _context;

        public CategoriaRepository(Context context) =>
            _context = context;

        public async Task Add(Categoria obj) =>
            await _context.Categoria.AddAsync(obj);

        public async Task<bool> Exists<T>(T id) =>
            await _context.Categoria
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id)) != null;

        public async Task<List<Categoria>> GetAll() =>
            await _context.Categoria
            .AsNoTracking()
            .ToListAsync();

        public async Task<Categoria> GetById<T>(T id) =>
            await _context.Categoria
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id));

        public void Remove(Categoria obj) =>
            _context.Categoria.Remove(obj);

        public void Update(Categoria obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property("DataCriacao").IsModified = false;
        }
    }
}
