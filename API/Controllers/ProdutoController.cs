using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models;
using API.Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepository _repository;

        public ProdutoController(ProdutoRepository repository)
        {
            _repository = repository;
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

        // PUT: api/Produto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Produto produto)
        {
            try
            {
                if (id != produto.Id)
                    return BadRequest();

                produto.DataAtualizacao = DateTime.Now;
                await _repository.Update(produto);

                return Ok(await Get(produto.Id));
            }
            catch (Exception ex)
            {
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

                return CreatedAtAction("Post", await Get(produto.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/Produto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _repository.Remove(await _repository.GetById(id));

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }        
    }
}
