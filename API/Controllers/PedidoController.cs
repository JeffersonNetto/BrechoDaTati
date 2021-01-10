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
    public class PedidoController : ControllerBase
    {        
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IUnitOfWork _uow;

        public PedidoController(IPedidoRepository pedidoRepository, IUnitOfWork wow)
        {
            _pedidoRepository = pedidoRepository;
            _uow = wow;
        }

        // GET: api/Pedido
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var pedidos = await _pedidoRepository.GetAll();

                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Pedido/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var pedido = await _pedidoRepository.GetById(id);

                return pedido == null ? NotFound() : Ok(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Pedido/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, Pedido pedido)
        {
            try
            {
                if (id != pedido.Id)
                    return BadRequest();

                //var result = await validator.ValidateAsync(cliente);

                //if (!result.IsValid)
                //    return UnprocessableEntity(new Retorno<Cliente>(result.Errors));

                _pedidoRepository.Update(pedido);
                await _uow.Commit();

                pedido = await _pedidoRepository.GetById(id);

                return Ok(new Retorno<Pedido> { Mensagem = "Registro atualizado com sucesso", Dados = pedido });
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(new Retorno<Pedido>(ex.InnerException?.Message));
            }
        }

        // POST: api/Pedido
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post(Pedido pedido)
        {           
            try
            {
                await _pedidoRepository.Add(pedido);
                await _uow.Commit();

                return CreatedAtAction("Post", await Get(pedido.Id));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }
        }

        // DELETE: api/Pedido/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                _pedidoRepository.Remove(await _pedidoRepository.GetById(id));
                await _uow.Commit();

                return NoContent();
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }
        }

        private async Task<bool> Exists(Guid id) =>
            await _pedidoRepository.Exists(id);        
    }
}
