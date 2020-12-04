using API.Models;
using API.Repositories;
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

        public MangaController(MangaRepository repository)
        {
            _repository = repository;
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
                await _repository.Update(manga);

                return Ok(await Get(manga.Id));
            }
            catch (Exception ex)
            {
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

                return CreatedAtAction("Post", await Get(manga.Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);                
            }            
        }

        // DELETE: api/Manga/5
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
