using System.ComponentModel.DataAnnotations;
using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Categorias.Commands
{
    public class CriarCategoriaCommad : IRequest<(string, bool)>
    {
        [Required(ErrorMessage = "E necessário informar o id do usuário")]
        public required Guid UsuarioId { get; set; }
        
        [Required(ErrorMessage = "Nome da categoria é obrigatório")]
        public required string Nome { get; set; }
        
        [Required(ErrorMessage = "Tipo da transação (despesa ou receita) é obrigatório")]
        public required TipoTransacao Tipo { get; set; }
        
        public string? Descricao { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }

    }

    internal class CriarCategoriaCommandHandler : IRequestHandler<CriarCategoriaCommad, (string, bool)>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IUsuarioRepository _usuarioRepository;

        public CriarCategoriaCommandHandler(ICategoriaRepository categoriaRepository, 
                                            IUsuarioRepository usuarioRepository)
        {
            _categoriaRepository = categoriaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<(string, bool)> Handle(CriarCategoriaCommad request, CancellationToken cancellationToken)
        {
            //VERIFICAR SE O USUÁRIO EXISTE
            var usuario = await _usuarioRepository.ObterUsuarioPorId(request.UsuarioId);
            if (usuario == null)
            {
                return ("Usuário inválido", false);
            }
            
            //NÃO PODE CADASTRAR CATEGORIA REPETIDA PARA O MESMO USUÁRIO
            var existe =
                await _categoriaRepository.NomeExisteParaUsuarioAsync(
                    request.Nome, request.Tipo, request.UsuarioId);

            if (existe)
            {
                return ("Categoria já cadastrada", false);
            }

            var novaCategoria = new Categoria(
                    request.UsuarioId,
                    request.Nome,
                    request.Tipo,
                    request.Descricao,
                    request.Cor,
                    request.Icone
                );

            await _categoriaRepository.AdicionarAsync(novaCategoria);
            return ("Categoria cadastrada com sucesso", true);
        }
    }
}
