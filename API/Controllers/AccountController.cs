using API.Models;
using API.Repositories;
using API.Uow;
using API.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ClienteRepository _repository;
        private readonly IUnitOfWork _uow;

        public AccountController(ClienteRepository repository, IUnitOfWork uow)
        {
            _repository = repository;
            _uow = uow;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(Usuario usuario)
        {
            try
            {
                var cliente = await _repository.GetByEmailSenha(usuario.Email, usuario.Senha);

                if (cliente == null)
                    return NotFound(new Retorno<Usuario> { Mensagem = "Usuário ou senha inválidos", Dados = null });

                cliente.Senha = null;
                cliente.Token = Services.TokenService.GenerateToken(cliente, System.DateTime.UtcNow.AddHours(8));
                cliente.RefreshToken = Services.TokenService.GenerateToken(cliente, System.DateTime.UtcNow.AddHours(16));

                return Ok(new Retorno<Cliente> { Mensagem = "Login realizado com sucesso", Dados = cliente });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new Retorno<Usuario>(ex.InnerException?.Message) { Mensagem = "Ocorreu um erro", Dados = null });
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(Cliente usuario, [FromServices] ClienteValidator validator)
        {
            try
            {
                var result = await validator.ValidateAsync(usuario);

                if (!result.IsValid)
                    return UnprocessableEntity(new Retorno<Usuario>(result.Errors) { Mensagem = null, Dados = null });

                await _repository.Add(usuario);
                await _uow.Commit();

                return Ok(new Retorno<Cliente> { Mensagem = "Cadastro realizado com sucesso", Dados = usuario });
            }
            catch (System.Exception ex)
            {
                await _uow.Rollback();

                return BadRequest(new Retorno<Usuario>(ex.InnerException?.Message) { Mensagem = "Ocorreu um erro", Dados = null });
            }
        }
    }
}
