using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MeuCorre.Infra.Repositories
{
   public class UsuarioRepository : IUsuarioRepository
    {
        private readonly MeuDbContext _meuDbcontext;
        public UsuarioRepository(MeuDbContext meuDbContext)
        {
            _meuDbcontext = meuDbContext;
        }
        public async Task CriarUsuarioAsync(Usuario usuario)
        {
            await _meuDbcontext.Usuarios.AddAsync(usuario);
            await _meuDbcontext.SaveChangesAsync();
        }

        public async Task AtualizarUsuarioAsync(Usuario usuario)
        {
            _meuDbcontext.Usuarios.Update(usuario);
            await _meuDbcontext.SaveChangesAsync();
        }
       public async Task RemoverUsuarioAsync(Usuario usuario)
        {
             _meuDbcontext.Usuarios.Remove(usuario);
            await _meuDbcontext.SaveChangesAsync();
        }

        public async Task<Usuario?> ObterUsuarioPorEmail(string email)
        {
           
            return await _meuDbcontext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);  
        }

        public async Task<Usuario?> ObterUsuarioPorId(Guid id)
        {

            return await _meuDbcontext.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
           
        }
    }
}
