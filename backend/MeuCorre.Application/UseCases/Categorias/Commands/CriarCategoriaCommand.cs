using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace MeuCorre.Application.UseCases.Categorias.Commands
{
   public class CriarCategoriaCommand :IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "Nome da categoria  é importante.")]
        public required string Nome { get; set; }
        public string? Cor { get; set; }
        [Required(ErrorMessage = "E necessário informar o id da usuario.")]
        public required Guid UsuarioId { get; set; }
        
        [Required(ErrorMessage =" Tipo da transação (despensa ou receita e obrigatorio)")]
        public required TipoTransacao Tipo { get; set; }
        public string? Icone { get; set; }
        public string? Descricao { get; set; }

       
    }
    internal class CriarCategoriaCommanHandler : IRequestHandler<CriarCategoriaCommand, (string, bool)>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public CriarCategoriaCommanHandler(ICategoriaRepository categoriaRepository, IUsuarioRepository usuarioRepository)
        {
            _categoriaRepository = categoriaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<(string, bool)> Handle(CriarCategoriaCommand request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.ObterUsuario(request.UsuarioId);
            var existe = await _categoriaRepository.NomeExisteParaUsuarioAsync(request.Nome, request.Tipo, request.UsuarioId);

            if (existe)
            {
                return ("categoria ja cadastrada.", false);
            }

            var novaCategoria = new Categoria(request.Nome, request.Descricao,request.Cor, request.Icone, request.UsuarioId, request.Tipo);

            await _categoriaRepository.AdicionarAsync(novaCategoria);
            return ("Categoria cadastrada com sucesso.", true);

        }
    }
}
