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
    public class CupomController : ControllerBase
    {
        private readonly ICupomRepository _repository;
        private readonly IUnitOfWork _uow;

        public CupomController(ICupomRepository repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        // GET: api/Cupom
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            try
            {
                var cupons = await _repository.GetAll();

                return Ok(cupons);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Cupom/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var cupom = await _repository.GetById(id);

                return cupom == null ? NotFound() : Ok(cupom);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Cupom/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(int id, Cupom cupom)
        {
            try
            {
                if (id != cupom.Id)
                    return BadRequest();

                _repository.Update(cupom);
                await _uow.Commit();

                return Ok(await Get(cupom.Id));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }
        }

        // POST: api/Cupom
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post(Cupom cupom)
        {
            try
            {
                await _repository.Add(cupom);
                await _uow.Commit();

                return CreatedAtAction("Post", await Get(cupom.Id));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }
        }

        // DELETE: api/Cupom/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
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

        // GET: api/Cupom/5
        [HttpGet("{descricao}/verificarvalidade")]
        [Authorize]
        public async Task<IActionResult> VerificarValidade(string descricao)
        {
            try
            {
                var cupom = await _repository.VerificarValidade(descricao);

                return cupom == null ? NotFound() : Ok(cupom);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
