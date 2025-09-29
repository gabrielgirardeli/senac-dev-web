using MediatR;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Categorias.Commands
{
    public class AtualizarCategoriaCommand : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "Id da categoria  é obrigatorio.")]
        public required Guid CategoriaId { get; set; }
        [Required(ErrorMessage = "Nome da categoria  é obrigatorio.")]
        public required string Nome { get; set; }
        [Required(ErrorMessage = "Tipo (despesa oureceita) da categoria é obrigatorio")]
        public required TipoTransacao tipo { get; set; }
        public string? Descricao { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }
       
    }
    
    internal class AtualizarCategoriaCommandHandler : IRequestHandler<AtualizarCategoriaCommand, (string, bool)>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public AtualizarCategoriaCommandHandler(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task<(string, bool)> Handle(AtualizarCategoriaCommand request, CancellationToken cancellationToken)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(request.CategoriaId);

            if (categoria == null)
            {
                return ("Categoria não encontrada.", false);
            }
            var categoriaEstaDuplicada = await _categoriaRepository.NomeExisteParaUsuarioAsync(request.Nome, request.tipo, categoria.UsuarioId.Value);

            if (categoriaEstaDuplicada )
            {
                return ("Já existe uma categoria com esse nome para o usuário.", false);
            }

            categoria.AtualizarInformacoes(request.Nome, request.Descricao, request.Cor, request.Icone, request.tipo);
            await _categoriaRepository.AtualizarAsync(categoria);
            return ("Categoria atualizada com sucesso.", true);


        }
    }
}
