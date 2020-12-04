using API.Models;
using API.Repositories;
using API.Uow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
                var cliente = await _repository.GetByEmailSenha(usuario.Login, usuario.Senha);

                if (cliente == null)
                    return NotFound(new Retorno<Usuario> { mensagem = "Usuário ou senha inválidos", dados = null });

                usuario.Senha = null;               
                usuario.Token = Services.TokenService.GenerateToken(cliente);

                return Ok(new Retorno<Usuario> { mensagem = "Login realizado com sucesso", dados = usuario });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new Retorno<Usuario> { mensagem = ex.Message, dados = null });
            }            
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<ActionResult> Register(Cliente usuario)
        {
            try
            {
                await _repository.Add(usuario);
                await _uow.Commit();

                return Ok(new Retorno<Cliente> { mensagem = "Cadastro realizado com sucesso", dados = usuario });
            }
            catch (System.Exception ex)
            {
                await _uow.Rollback();
                return BadRequest(new Retorno<Usuario> { mensagem = ex.Message, dados = null });
            }
        }
    }
}
