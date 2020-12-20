using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class MarcaRepository : IRepositoryBase<Marca>
    {
        private readonly Context _context;

        public MarcaRepository(Context context) =>
            _context = context;

        public async Task Add(Marca obj) =>
            await _context.Marca.AddAsync(obj);

        public async Task<bool> Exists<T>(T id) =>
            await _context.Marca
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id)) != null;

        public async Task<IEnumerable<Marca>> GetAll() =>
            await _context.Marca
            .AsNoTracking()
            .ToListAsync();

        public async Task<Marca> GetById<T>(T id) =>
            await _context.Marca
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id));

        public void Remove(Marca obj) =>
            _context.Marca.Remove(obj);

        public void Update(Marca obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property("DataCriacao").IsModified = false;
        }
    }
}
