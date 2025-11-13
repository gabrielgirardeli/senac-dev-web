using MediatR;
using MeuCorre.Application.UseCases.Usuarios.Commands;
using Microsoft.AspNetCore.Mvc;

namespace MeuCorre.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }
        ///<summary>
        ///Cria um novo usuário.
        ///<param name="command"></param>
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CriarUsuario([FromBody] CriarUsuarioCommand command)
        {
            var (mensagem, sucesso) = await _mediator.Send(command);
            if (sucesso)
            {
                return Ok(mensagem);
            }
            else
            {
                return Conflict(mensagem);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarUsuario(Guid id, [FromBody] AtualizarUsuarioCommand command)
        {
            command.Id = id;
            var(mensagem, sucesso) = await _mediator.Send(command);
            if (sucesso)
            {
                return Ok(mensagem);
            }
            else
            {
                return NotFound(mensagem);
            }
        }

    }
}
