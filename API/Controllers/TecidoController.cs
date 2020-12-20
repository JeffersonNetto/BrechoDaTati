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
    public class TecidoController : ControllerBase
    {
        private readonly IRepositoryBase<Tecido> _repository;
        private readonly IUnitOfWork _uow;

        public TecidoController(IRepositoryBase<Tecido> repository, Uow.IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        // GET: api/Tecido
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Tecidos = await _repository.GetAll();

                return Ok(Tecidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Tecido/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(short id)
        {
            try
            {
                var tecido = await _repository.GetById(id);

                return tecido == null ? NotFound() : Ok(tecido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Tecido/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(short id, Tecido tecido)
        {
            try
            {
                if (id != tecido.Id)
                    return BadRequest();
                
                _repository.Update(tecido);
                await _uow.Commit();

                return Ok(await Get(tecido.Id));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }
        }

        // POST: api/Tecido
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> Post(Tecido tecido)
        {
            try
            {
                await _repository.Add(tecido);
                await _uow.Commit();

                return CreatedAtAction("Post", await Get(tecido.Id));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }
        }

        // DELETE: api/Tecido/5
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
