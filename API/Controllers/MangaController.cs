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
    public class MangaController : ControllerBase
    {
        private readonly MangaRepository _repository;
        private readonly IUnitOfWork _uow;

        public MangaController(MangaRepository repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        // GET: api/Manga
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var mangas = await _repository.GetAll();

                return Ok(mangas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/Manga/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> Get(short id)
        {
            try
            {
                var manga = await _repository.GetById(id);

                return manga == null ? NotFound() : Ok(manga);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Manga/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(short id, Manga manga)
        {
            try
            {
                if (id != manga.Id)
                    return BadRequest();

                manga.DataAtualizacao = DateTime.Now;                
                _repository.Update(manga);
                await _uow.Commit();

                return Ok(await Get(manga.Id));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);
            }                                  
        }

        // POST: api/Manga
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> Post(Manga manga)
        {
            try
            {
                await _repository.Add(manga);
                await _uow.Commit();

                return CreatedAtAction("Post", await Get(manga.Id));
            }
            catch (Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(ex);                
            }            
        }

        // DELETE: api/Manga/5
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
