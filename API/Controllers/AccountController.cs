using API.Models;
using API.Repositories;
using API.Uow;
using API.Validators;
using FluentEmail.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IClienteRepository _repository;
        private readonly IUnitOfWork _uow;
        private readonly IMemoryCache _cache;

        public AccountController(
            IClienteRepository repository,
            IUnitOfWork uow,
            IMemoryCache cache
            )
        {
            _repository = repository;
            _uow = uow;
            _cache = cache;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Usuario usuario)
        {
            try
            {
                var cliente = await _repository.GetByEmailSenha(usuario.Email, usuario.Senha);

                if (cliente == null)
                    return NotFound(new Retorno<Usuario> { Mensagem = "Usuário ou senha inválidos", Dados = null });

                cliente.Senha = null;
                cliente.Token = Services.TokenService.GenerateToken(cliente, System.DateTime.UtcNow.AddMinutes(2));
                cliente.RefreshToken = Services.TokenService.GenerateToken(cliente, System.DateTime.UtcNow.AddHours(16));

                SetToCache(cliente.Id.ToString(), cliente);

                return Ok(new Retorno<Cliente> { Mensagem = "Login realizado com sucesso", Dados = cliente });
            }
            catch (Exception ex)
            {
                return BadRequest(new Retorno<Usuario>(ex.InnerException?.Message));
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(
            Cliente usuario,
            [FromServices] ClienteValidator validator,
            [FromServices] IFluentEmail email
            )
        {
            try
            {
                var result = await validator.ValidateAsync(usuario);

                if (!result.IsValid)
                    return UnprocessableEntity(new Retorno<Usuario>(result.Errors));

                await _repository.Add(usuario);
                await _uow.Commit();

                _ = email
                    .To(usuario.Email, usuario.Nome)
                    .Subject("Cadastro realizado com sucesso")
                    .Body("Corpo da mensagem de teste")
                    .SendAsync();

                return Ok(new Retorno<Cliente> { Mensagem = "Cadastro realizado com sucesso", Dados = usuario });
            }
            catch (Exception ex)
            {
                await _uow.Rollback();

                return BadRequest(new Retorno<Usuario>(ex.InnerException?.Message));
            }
        }

        [HttpPost("cache/{key}")]
        [Authorize]
        public IActionResult SetToCache([FromRoute] string key, [FromBody] object value)
        {
            try
            {
                _cache.Set(key, value, TimeSpan.FromHours(8));
                return Ok(true);
            }
            catch
            {
                return BadRequest(false);
            }
        }

        [HttpGet("cache/{key}")]
        [Authorize]
        public IActionResult GetFromCache([FromRoute] string key)
        {
            if (_cache.TryGetValue(key, out object value))
                return Ok(value);
            else
                return NotFound(default);
        }

        [HttpGet("refreshtoken/{key}")]
        [Authorize]
        public async Task<IActionResult> RefreshToken([FromRoute] System.Guid key)
        {
            try
            {
                var cliente = await _repository.GetById(key);

                if (cliente == null)
                    return NotFound(new Retorno<Usuario> { Mensagem = "Usuário não encontrado na base de dados", Dados = null });

                cliente.Senha = null;
                cliente.Token = Services.TokenService.GenerateToken(cliente, System.DateTime.UtcNow.AddMinutes(2));
                cliente.RefreshToken = Services.TokenService.GenerateToken(cliente, System.DateTime.UtcNow.AddHours(16));

                SetToCache(cliente.Id.ToString(), cliente);

                return Ok(new Retorno<Cliente> { Mensagem = "Token atualizado com sucesso", Dados = cliente });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new Retorno<Usuario>(ex.InnerException?.Message));
            }
        }
    }
}
