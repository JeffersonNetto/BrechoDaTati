using API.Models;
using API.Repositories;
using API.Uow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepository _repository;
        private readonly IUnitOfWork _uow;

        public ClienteController(ClienteRepository repository, Uow.IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        // GET: api/Cliente
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var clientes = await _repository.GetAll();

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Cliente/5
        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult> Get(Guid id)
        {
            try
            {
                var cliente = await _repository.GetById(id);

                return cliente == null ? NotFound() : Ok(cliente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Cliente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Put(Guid id, Cliente cliente)
        {
            try
            {
                if (id != cliente.Id)
                    return BadRequest();

                cliente.DataAtualizacao = DateTime.Now;
                _repository.Update(cliente);
                await _uow.Commit();

                return Ok(await Get(cliente.Id));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }
        }

        // POST: api/Cliente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post(Cliente cliente)
        {
            try
            {
                await _repository.Add(cliente);
                await _uow.Commit();

                return CreatedAtAction("Post", await Get(cliente.Id));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }
        }

        // DELETE: api/Cliente/5
        [HttpDelete("{id:int}")]
        [Authorize]
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
