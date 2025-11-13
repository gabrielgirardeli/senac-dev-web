using System.ComponentModel.DataAnnotations;
using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Usuarios.Commands
{
    public class AtualizarUsuarioCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "É necessário informar o Id para atualizar os dados")]
        public required Guid Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Data de Nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }
    }

    internal class AtualizarUsuarioCommandHandler : IRequestHandler<AtualizarUsuarioCommand, (string, bool)>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public AtualizarUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<(string, bool)> Handle(AtualizarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterUsuarioPorId(request.Id);
            if(usuario == null)
            {
                return ("Usuário não encontrado.", false);
            }

            usuario.AtualizarInformacoes(request.Nome, request.DataNascimento);

            await _usuarioRepository.AtualizarUsuarioAsync(usuario);

            return ("Usuário atualizado com sucesso", true);
        }
    }
}
