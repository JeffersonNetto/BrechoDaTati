using API.Models;
using API.Repositories;
using API.Uow;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoRepository _repository;
        private readonly IUnitOfWork _uow;
        private readonly IMemoryCache _cache;

        public ProdutoController(IProdutoRepository repository, IUnitOfWork uow, IMemoryCache cache)
        {
            _repository = repository;
            _uow = uow;
            _cache = cache;
        }

        // GET: api/Produto
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var produtos = await ObterProdutosEmCache();

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Produto/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var produto = null as Produto;

                var produtos = await ObterProdutosEmCache();

                produto = produtos?.Find(_ => _.Id == id);

                if (produto == null)
                    produto = await _repository.GetById(id);

                if (produto != null)
                    AtualizarProdutosEmCache(produtos ?? null);

                return produto == null ? NotFound() : Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Produto/5        
        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> Get(string slug)
        {
            try
            {
                var produto = null as Produto;

                var produtos = await ObterProdutosEmCache();

                produto = produtos?.Find(_ => _.Slug == slug);

                if (produto == null)
                    produto = await _repository.GetBySlug(slug);

                if (produto != null)
                    AtualizarProdutosEmCache(produtos ?? null);

                return produto == null ? NotFound() : Ok(produto);                                
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Produto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Produto produto)
        {
            try
            {
                if (id != produto.Id)
                    return BadRequest();

                _repository.Update(produto);
                await _uow.Commit();

                var produtos = await _repository.GetAll();

                AtualizarProdutosEmCache(produtos);

                return Ok(produtos.Find(_ => _.Id == produto.Id));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }
        }

        // POST: api/Produto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post(Produto produto)
        {
            try
            {
                await _repository.Add(produto);
                await _uow.Commit();

                var produtos = await _repository.GetAll();

                AtualizarProdutosEmCache(produtos);

                return CreatedAtAction("Post", produtos.Find(_ => _.Id == produto.Id));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }
        }

        // DELETE: api/Produto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                _repository.Remove(await _repository.GetById(id));
                await _uow.Commit();

                AtualizarProdutosEmCache();

                return NoContent();
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }
        }

        private async void AtualizarProdutosEmCache(List<Produto> produtos = null)
        {
            _cache.Set("produtos", produtos ?? await _repository.GetAll(), TimeSpan.FromHours(2));
        }

        private async Task<List<Produto>> ObterProdutosEmCache()
        {
            var produtos = _cache.Get("produtos") as List<Produto>;

            if (produtos == null)
            {
                produtos = await _repository.GetAll();

                AtualizarProdutosEmCache(produtos);
            }

            return produtos;
        }

        [HttpGet("{id}/estoque")]
        public async Task<int> VerificarEstoque(Guid id)
        {
            try
            {                
                var produtos = await ObterProdutosEmCache();
                var produtoEmCache = produtos?.Find(_ => _.Id == id);                                

                return produtoEmCache == null ? 0 : produtoEmCache.Estoque;
            }
            catch
            {
                return 0;
            }
        }

        [HttpGet("{id}/incrementarestoque")]
        public async Task<IActionResult> IncrementarEstoque(Guid id)
        {
            try
            {                
                var produtos = await ObterProdutosEmCache();
                var produtoEmCache = produtos?.Find(_ => _.Id == id);

                if(produtoEmCache != null)
                    produtoEmCache.Estoque++;

                AtualizarProdutosEmCache(produtos);

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}/decrementarestoque")]
        public async Task<IActionResult> DecrementarEstoque(Guid id)
        {
            try
            {
                var produtos = await ObterProdutosEmCache();
                var produtoEmCache = produtos?.Find(_ => _.Id == id);

                if (produtoEmCache != null)
                    produtoEmCache.Estoque--;

                AtualizarProdutosEmCache(produtos);

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
