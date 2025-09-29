using MediatR;
using MeuCorre.Application.UseCases.Categorias.Dtos;
using MeuCorre.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Categorias.Querries
{
   public class ObterCategoriasQuerry : IRequest<CategoriaDto>
    {
        [Required(ErrorMessage = "informa o Id da categoria.")]
        public required Guid CategoriaId { get; set; }

    }
   
    internal class ObterCategoriasQuerryHandler : IRequestHandler<ObterCategoriasQuerry, CategoriaDto>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public ObterCategoriasQuerryHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

       
        public async Task<CategoriaDto> Handle(ObterCategoriasQuerry request, CancellationToken cancellationToken)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(request.CategoriaId);

            if (categoria == null)
            {
                return null;
            }
            var categoriaDto = new CategoriaDto
            {
                Nome = categoria.Nome,
                Descricao = categoria.Descricao,
                Cor = categoria.Cor,
                Icone = categoria.Icone,
                tipo = categoria.Tipo,
                
                
            };
            return categoriaDto;

        }
    }
}
