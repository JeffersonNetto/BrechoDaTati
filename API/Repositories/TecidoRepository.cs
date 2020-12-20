using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class TecidoRepository : IRepositoryBase<Tecido>
    {
        private readonly Context _context;

        public TecidoRepository(Context context) =>
            _context = context;

        public async Task Add(Tecido obj) =>
            await _context.Tecido.AddAsync(obj);

        public async Task<bool> Exists<T>(T id) =>
            await _context.Tecido
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id)) != null;

        public async Task<IEnumerable<Tecido>> GetAll() =>
            await _context.Tecido
            .AsNoTracking()
            .ToListAsync();

        public async Task<Tecido> GetById<T>(T id) =>
            await _context.Tecido
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id));

        public void Remove(Tecido obj) =>
            _context.Tecido.Remove(obj);

        public void Update(Tecido obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property("DataCriacao").IsModified = false;
        }
    }
}
