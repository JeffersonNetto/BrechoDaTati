using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class CondicaoRepository : IRepositoryBase<Condicao>
    {
        private readonly Context _context;

        public CondicaoRepository(Context context) =>
            _context = context;

        public async Task Add(Condicao obj) =>
            await _context.Condicao.AddAsync(obj);

        public async Task<bool> Exists<T>(T id) =>
            await _context.Condicao
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id)) != null;

        public async Task<IEnumerable<Condicao>> GetAll() =>
            await _context.Condicao
            .AsNoTracking()
            .ToListAsync();

        public async Task<Condicao> GetById<T>(T id) =>
            await _context.Condicao
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id));

        public void Remove(Condicao obj) =>
            _context.Condicao.Remove(obj);

        public void Update(Condicao obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property("DataCriacao").IsModified = false;
        }
    }
}
