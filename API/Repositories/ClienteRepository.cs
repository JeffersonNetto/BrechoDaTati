using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IClienteRepository : IRepositoryBase<Cliente>
    {
        Task<Cliente> GetByEmailSenha(string email, string senha);
    }
    public class ClienteRepository : IClienteRepository
    {
        private readonly Context _context;
        
        public ClienteRepository(Context context) => 
            _context = context;        

        public async Task Add(Cliente obj) =>       
            await _context.Cliente.AddAsync(obj);        

        public async Task<bool> Exists<T>(T id) =>
            await _context.Cliente
            .AsNoTracking()
            .FirstOrDefaultAsync(_=>_.Id.Equals(id)) != null;

        public async Task<IEnumerable<Cliente>> GetAll() =>        
            await _context.Cliente
            .Include(_ => _.ClienteEndereco)
            .Include(_ => _.ClienteProdutoFavorito).ThenInclude(_ => _.Produto)
            .AsNoTracking()
            .ToListAsync();
       
        public async Task<Cliente> GetByEmailSenha(string email, string senha) => 
            await _context.Cliente
            .Include(_ => _.ClienteEndereco)
            .Include(_ => _.ClienteProdutoFavorito).ThenInclude(_ => _.Produto)
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Email == email && _.Senha == senha);

        public async Task<Cliente> GetById<T>(T id) =>        
            await _context.Cliente
            .Include(_ => _.ClienteEndereco)
            .Include(_ => _.ClienteProdutoFavorito).ThenInclude(_ => _.Produto)
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id));

        public void Remove(Cliente obj) =>        
            _context.Cliente.Remove(obj);        

        public void Update(Cliente obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property("DataCriacao").IsModified = false;
        }
    }    
}
