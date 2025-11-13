using System.ComponentModel.DataAnnotations;
using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Usuarios.Commands
{
    /// <summary>
    /// Comando para criar um novo usuário.
    /// Aqui você pode adicionar propriedades necessárias para criar o usuário
    /// </summary>
    public class CriarUsuarioCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "Email é obrigatório")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
        public required string Senha { get; set; }

        [Required(ErrorMessage = "Data de Nascimento é obrigatória")]
        public DateTime DataNascimento { get; set; }
    }

    internal class CriarUsuarioCommandHandler : IRequestHandler<CriarUsuarioCommand, (string,bool)>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public CriarUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<(string, bool)> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {
            //vai no banco e verifica se já existe um usuário com o email informado
            var usuarioExistente = await _usuarioRepository.ObterUsuarioPorEmail(request.Email);
            if (usuarioExistente != null)
            {
                return ("Já existe um usuário cadastrado com este email.", false);
            }

            var novoUsuario = new Usuario(
                request.Nome, 
                request.Email, 
                request.Senha, 
                request.DataNascimento, 
                true);

            await _usuarioRepository.CriarUsuarioAsync(novoUsuario);
            return ("Usuário criado com sucesso.", true);
        }
    }
}
