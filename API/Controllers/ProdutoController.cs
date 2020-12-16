using API.Models;
using API.Repositories;
using API.Uow;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepository _repository;
        private readonly IUnitOfWork _uow;

        public ProdutoController(ProdutoRepository repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        // GET: api/Produto
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            try
            {
                var produtos = await _repository.GetAll();

                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Produto/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            try
            {
                var produto = await _repository.GetById(id);

                return produto == null ? NotFound() : Ok(produto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Produto/5        
        [HttpGet("slug/{slug}")]
        public async Task<ActionResult> Get(string slug)
        {
            try
            {
                var produto = await _repository.GetBySlug(slug);

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

                return Ok(await Get(produto.Id));
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
        public async Task<ActionResult<Produto>> Post(Produto produto)
        {
            try
            {
                await _repository.Add(produto);
                await _uow.Commit();

                return CreatedAtAction("Post", await Get(produto.Id));
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

                return NoContent();
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }
        }        
    }
}
