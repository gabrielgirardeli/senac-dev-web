using MeuCorre.Domain.Entities;

namespace MeuCorre.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task CriarUsuarioAsync(Usuario usuario); //INSERT
        Task AtualizarUsuarioAsync(Usuario usuario); //UPDATE
        Task RemoverUsuarioAsync(Usuario usuario); //DELETE

        //? significa que o select pode retornar nulo, ou seja,
        //o usuário pode não ser encontrado
        Task<Usuario?> ObterUsuarioPorEmail(string email);
        Task<Usuario?> ObterUsuarioPorId(Guid id);

    }
}
