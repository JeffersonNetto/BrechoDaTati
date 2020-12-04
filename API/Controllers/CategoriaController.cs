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
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaRepository _repository;
        private readonly IUnitOfWork _uow;

        public CategoriaController(CategoriaRepository repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        // GET: api/Categoria
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var categorias = await _repository.GetAll();

                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Categoria/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(short id)
        {
            try
            {
                var categoria = await _repository.GetById(id);

                return categoria == null ? NotFound() : Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Categoria/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(short id, Categoria categoria)
        {
            try
            {
                if (id != categoria.Id)
                    return BadRequest();

                categoria.DataAtualizacao = DateTime.Now;

                _repository.Update(categoria);
                await _uow.Commit();

                return Ok(await Get(categoria.Id));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }
        }

        // POST: api/Categoria
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> Post(Categoria categoria)
        {
            try
            {
                await _repository.Add(categoria);
                await _uow.Commit();

                return CreatedAtAction("Post", await Get(categoria.Id));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }
        }

        // DELETE: api/Categoria/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(short id)
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
