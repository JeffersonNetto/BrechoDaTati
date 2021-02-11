using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface ICupomRepository : IRepositoryBase<Cupom>
    {
        Task<Cupom> VerificarValidade(string descricao);
    }
    public class CupomRepository : ICupomRepository
    {
        private readonly Context _context;

        public CupomRepository(Context context) =>
            _context = context;

        public async Task Add(Cupom obj) =>
            await _context.Cupom.AddAsync(obj);

        public async Task<bool> Exists<T>(T id) =>
            await _context.Cupom
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id)) != null;

        public async Task<List<Cupom>> GetAll() =>
            await _context.Cupom
            .AsNoTracking()
            .ToListAsync();

        public async Task<Cupom> GetById<T>(T id) =>
            await _context.Cupom
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id));

        public void Remove(Cupom obj) =>
            _context.Cupom.Remove(obj);

        public void Update(Cupom obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property("DataCriacao").IsModified = false;
        }

        public async Task<Cupom> VerificarValidade(string descricao) =>        
            await _context.Cupom
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Descricao.Equals(descricao) && _.DataInicio <= System.DateTime.Now && _.DataFim >= System.DateTime.Now && _.Ativo);                    
    }
}
