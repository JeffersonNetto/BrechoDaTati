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
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteRepository _repository;

        public ClienteController(ClienteRepository repository)
        {
            _repository = repository;
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
                await _repository.Update(cliente);

                return Ok(await Get(cliente.Id));
            }
            catch (Exception ex)
            {
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

                return CreatedAtAction("Post", await Get(cliente.Id));
            }
            catch (Exception ex)
            {
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
