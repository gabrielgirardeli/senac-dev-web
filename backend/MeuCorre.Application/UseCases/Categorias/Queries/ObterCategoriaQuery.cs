using System.ComponentModel.DataAnnotations;
using MediatR;
using MeuCorre.Application.UseCases.Categorias.Dtos;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Categorias.Queries
{
    public class ObterCategoriaQuery : IRequest<CategoriaDto>
    {
        [Required(ErrorMessage = "Informe o Id da categoria")]
        public required Guid CategoriaId { get; set; }
    }

    internal class ObterCategoriaQueryHandler : IRequestHandler<ObterCategoriaQuery, CategoriaDto>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public ObterCategoriaQueryHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<CategoriaDto> Handle(ObterCategoriaQuery request, CancellationToken cancellationToken)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(request.CategoriaId);

            if (categoria == null)
                return null;

            var categoriaDto = new CategoriaDto
            {
                Nome = categoria.Nome,
                Ativo = categoria.Ativo,
                Tipo = categoria.TipoDaTransacao,
                Cor = categoria.Cor,
                Descricao = categoria.Descricao,
                Icone = categoria.Icone,
            };

            return categoriaDto;
        }
    }
}
