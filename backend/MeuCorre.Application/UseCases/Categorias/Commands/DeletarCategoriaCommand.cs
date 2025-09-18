using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Categorias.Commands
{
   public class DeletarCategoriaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "e necessario informar o id do Categoria.")]
        public required Guid CategoriaId { get; set; }
        [Required(ErrorMessage = "e necessario informar o id do usuario.")]
    
        public required Guid UsuarioId { get; set; }
     
    }
    internal class DeletarCategoriaCommandHandler : IRequestHandler<DeletarCategoriaCommand, (string, bool)>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public DeletarCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
        {
             _categoriaRepository = categoriaRepository;
        }
        public Task<(string, bool)> Handle(DeletarCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria = _categoriaRepository.ObterPorIdAsync(request.CategoriaId).Result;

            if (categoria == null || categoria.UsuarioId != request.UsuarioId)
            {
                return Task.FromResult(("Categoria não encontrada ou não pertence ao usuário.", false));
            }
           
            throw new NotImplementedException();
        }
    }

}
