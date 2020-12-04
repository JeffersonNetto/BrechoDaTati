using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models;
using API.Repositories;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CondicaoController : ControllerBase
    {
        private readonly CondicaoRepository _repository;

        public CondicaoController(CondicaoRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Condicao
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var condicoes = await _repository.GetAll();

                return Ok(condicoes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Condicao/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(short id)
        {
            try
            {
                var condicao = await _repository.GetById(id);

                return condicao == null ? NotFound() : Ok(condicao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Condicao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(short id, Condicao condicao)
        {
            try
            {
                if (id != condicao.Id)
                    return BadRequest();

                condicao.DataAtualizacao = DateTime.Now;
                await _repository.Update(condicao);

                return Ok(await Get(condicao.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: api/Condicao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> Post(Condicao condicao)
        {
            try
            {
                await _repository.Add(condicao);

                return CreatedAtAction("Post", await Get(condicao.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE: api/Condicao/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(short id)
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
