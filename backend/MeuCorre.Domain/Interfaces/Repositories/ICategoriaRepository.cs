using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;

namespace MeuCorre.Domain.Interfaces.Repositories
{
   public interface ICategoriaRepository
    {
        Task<Categoria?> ObterPorIdAsync(Guid categoriaId);
        Task<IEnumerable<Categoria>> ListarTodasPorUsuarioAsync(Guid usuarioId);

        Task<bool> ExisteAsync(Guid categoriaId);
        Task<bool> NomeExisteParaUsuarioAsync(string nome, TipoTransacao tipo, Guid usuarioId);

        Task AdicionarAsync(Categoria categoria);
        Task AtualizarAsync(Categoria categoria);
        Task RemoverAsync(Categoria categoriaId);
    }
}
