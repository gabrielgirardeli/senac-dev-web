using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Usuarios.Commands
{
   public  class CriarUsuarioCommand : IRequest<(string,bool)> 
    {
        [Required(ErrorMessage = "Nome é obrigatorio")]
        public required string Nome { get; set; }
        [Required(ErrorMessage = "Email é obrigatorio")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatorio")]
        [MinLength(6, ErrorMessage = "Senha deve ter no minimo 6 caracteres")]
        public required string Senha { get; set; }

        [Required(ErrorMessage = "Data de Nascimento é obrigatorio")]
        public DateTime DataNascimento { get; set; }
        
    }
    internal class CriarUsuarioCommandHandler : IRequestHandler<CriarUsuarioCommand, (string, bool)>
    {
        private readonly IUsuarioRepositories _usuarioRepositories;
        public CriarUsuarioCommandHandler(IUsuarioRepositories usuarioRepositories)
        {
            _usuarioRepositories = usuarioRepositories;
        }
       public async Task<(string,bool)> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
        {
            var usuarioExistente = await _usuarioRepositories.ObterUsuarioPorEmail(request.Email);
            if (usuarioExistente != null)
            {
                return ("Já existe um usuario cadastrado com este email", false);
            }

        
            var novoUsuario = new Usuario(
                request.Nome,
                request.Email,
                request.Senha,
                request.DataNascimento,
                true);
            await _usuarioRepositories.CriarUsuarioAsync(novoUsuario);
            return ("Usuário criado com sucesso", true);


        }
    }
}
