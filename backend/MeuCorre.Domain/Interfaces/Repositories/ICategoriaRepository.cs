using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;

namespace MeuCorre.Domain.Interfaces.Repositories
{
    public interface ICategoriaRepository
    {
        //Retorna do banco de dados os dados de uma categoria que possua o Id informado
        Task<Categoria?>ObterPorIdAsync(Guid categoriaId);

        //Retorna do banco de dados todas as categorias que pertençam ao usuário informado
        Task<IList<Categoria>> ListarTodasPorUsuarioAsync(Guid usuarioId);

        //Verificar se uma categoria existe no banco de dados com o Id informado
        //SELECT * FROM Categorias WHERE Id = 5
        Task<bool> ExisteAsync(Guid categoriaId);

        //Verifica se já existe uma categoria com o mesmo
        //nome e tipo para o usuário informado
        //nome e tipo para o usuário informado
        Task<bool> NomeExisteParaUsuarioAsync(string nome, TipoTransacao tipo, Guid usuarioId);

        //Adiciona uma nova categoria no banco de dados
        Task AdicionarAsync(Categoria categoria);

        //Atualiza os dados de uma categoria no banco de dados
        Task AtualizarAsync(Categoria categoria);

        //Remove uma categoria do banco de dados
        Task RemoverAsync(Categoria categoria);
    }
}