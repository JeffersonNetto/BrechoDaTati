using API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public class ProdutoRepository : RepositoryBase<Produto>
    {
        public ProdutoRepository(Data.Context context) : base(context)
        {

        }

        public override async Task<IEnumerable<Produto>> GetAll() =>
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

        public override async Task<Produto> GetById<T>(T id) => 
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
            .SingleOrDefaultAsync(_ => _.Id.Equals(id));

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
            .SingleOrDefaultAsync(_ => _.Slug == slug);
    }
}
