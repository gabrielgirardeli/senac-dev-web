using System.ComponentModel.DataAnnotations;
using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Categorias.Commands
{
    public class InativarCategoriaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "É necessário informar o ID da categoria")]
        public required Guid CategoriaId { get; set; }
    }

    internal class InativarCategoriaCommandHandler : IRequestHandler<InativarCategoriaCommand, (string, bool)>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public InativarCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        
        public async Task<(string, bool)> Handle(InativarCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria =
                await _categoriaRepository.ObterPorIdAsync(request.CategoriaId);

            categoria.Inativar();

            await _categoriaRepository.AtualizarAsync(categoria);
            return ("Categoria desativada com sucesso", true);
        }
    }

}
