using System.ComponentModel.DataAnnotations;
using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Categorias.Commands
{
    public class DeletarCategoriaCommad : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "E necessário informar o id do usuário")]
        public required Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "E necessário informar o id da categoria")]
        public required Guid CategoriaId { get; set; }
    }
    internal class DeletarCategoriaCommadHandler : IRequestHandler<DeletarCategoriaCommad, (string, bool)>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public DeletarCategoriaCommadHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<(string, bool)> Handle(DeletarCategoriaCommad request, CancellationToken cancellationToken)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(request.CategoriaId);

            if (categoria == null)
                return ("Categoria não encontrada", false);

            if (categoria.UsuarioId != request.UsuarioId)
                return ("Categoria não pertence ao usuário", false);

            await _categoriaRepository.RemoverAsync(categoria);

            return ("Categoria removida com sucesso", true);
        }
    }


}
