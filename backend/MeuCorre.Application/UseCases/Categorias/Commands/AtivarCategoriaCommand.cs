using System.ComponentModel.DataAnnotations;
using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Categorias.Commands
{
    public class AtivarCategoriaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "É necessário informar o ID da categoria")]
        public required Guid CategoriaId { get; set; }
    }
    internal class AtivarCategoriaCommandHandler : IRequestHandler<AtivarCategoriaCommand, (string, bool)>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public AtivarCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }


        public async Task<(string, bool)> Handle(AtivarCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria = 
                await _categoriaRepository.ObterPorIdAsync(request.CategoriaId);

            categoria.Ativar();

            await _categoriaRepository.AtualizarAsync(categoria);
            return ("Categoria ativada com sucesso", true);
        }
    }
}
