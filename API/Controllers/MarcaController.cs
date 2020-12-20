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
    public class MarcaController : ControllerBase
    {
        private readonly IRepositoryBase<Marca> _repository;
        private readonly IUnitOfWork _uow;

        public MarcaController(IRepositoryBase<Marca> repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        // GET: api/Marca
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var marcas = await _repository.GetAll();

                return Ok(marcas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Marca/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(short id)
        {
            try
            {
                var marca = await _repository.GetById(id);

                return marca == null ? NotFound() : Ok(marca);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Marca/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(short id, Marca marca)
        {
            try
            {
                if (id != marca.Id)
                    return BadRequest();
                
                _repository.Update(marca);
                await _uow.Commit();

                return Ok(await Get(marca.Id));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }                                  
        }

        // POST: api/Marca
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> Post(Marca marca)
        {
            try
            {
                await _repository.Add(marca);
                await _uow.Commit();

                return CreatedAtAction("Post", await Get(marca.Id));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);                
            }            
        }

        // DELETE: api/Marca/5
        [HttpDelete("{id}")]
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
