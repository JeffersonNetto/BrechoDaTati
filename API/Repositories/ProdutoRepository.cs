using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IProdutoRepository : IRepositoryBase<Produto>
    {
        Task<Produto> GetBySlug(string slug);
    }
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly Context _context;

        public ProdutoRepository(Context context) =>
            _context = context;

        public async Task Add(Produto obj) =>
            await _context.Produto.AddAsync(obj);

        public async Task<bool> Exists<T>(T id) =>
            await _context.Produto
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id)) != null;


        public async Task<IEnumerable<Produto>> GetAll() =>
             await
            _context.Produto
            .Include(_ => _.Marca)
            .Include(_ => _.Categoria)
            .Include(_ => _.Tamanho)
            .Include(_ => _.Manga)
            .Include(_ => _.Condicao)
            .Include(_ => _.Modelagem)
            .Include(_ => _.Tecido)
            .Include(_ => _.ProdutoImagem)
            .AsNoTracking()
            .ToListAsync();

        public async Task<Produto> GetById<T>(T id) =>
            await
            _context.Produto
            .Include(_ => _.Marca)
            .Include(_ => _.Categoria)
            .Include(_ => _.Tamanho)
            .Include(_ => _.Manga)
            .Include(_ => _.Condicao)
            .Include(_ => _.Modelagem)
            .Include(_ => _.Tecido)
            .Include(_ => _.ProdutoImagem)
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Id.Equals(id));

        public async Task<Produto> GetBySlug(string slug) =>
            await
            _context.Produto
            .Include(_ => _.Marca)
            .Include(_ => _.Categoria)
            .Include(_ => _.Tamanho)
            .Include(_ => _.Manga)
            .Include(_ => _.Condicao)
            .Include(_ => _.Modelagem)
            .Include(_ => _.Tecido)
            .Include(_ => _.ProdutoImagem)
            .AsNoTracking()
            .FirstOrDefaultAsync(_ => _.Slug == slug);

        public void Remove(Produto obj) =>
            _context.Produto.Remove(obj);        

        public void Update(Produto obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property("DataCriacao").IsModified = false;
        }
    }
}
