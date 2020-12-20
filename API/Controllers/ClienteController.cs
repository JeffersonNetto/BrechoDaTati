using API.Models;
using API.Repositories;
using API.Uow;
using API.Validators;
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
        private readonly IClienteRepository _repository;
        private readonly IUnitOfWork _uow;

        public ClienteController(IClienteRepository repository, Uow.IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        // GET: api/Cliente
        [HttpGet]
        //[Authorize]        
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
        [HttpGet("{id}")]
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
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(Guid id, Cliente cliente, [FromServices] ClienteValidator validator)
        {
            try
            {
                if (id != cliente.Id)
                    return BadRequest();

                var result = await validator.ValidateAsync(cliente);

                if (!result.IsValid)
                    return UnprocessableEntity(new Retorno<Cliente>(result.Errors));

                _repository.Update(cliente);
                await _uow.Commit();

                cliente = await _repository.GetById(id);

                return Ok(new Retorno<Cliente> { Mensagem = "Cadastro atualizado com sucesso", Dados = cliente });
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(new Retorno<Cliente>(ex.InnerException?.Message));
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
