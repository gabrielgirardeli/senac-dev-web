using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Infra.Repositories
{
   public class UsuarioRepositories : IUsuarioRepositories
    {
        private readonly MeuDbContext _meuDbcontext;
        public UsuarioRepositories(MeuDbContext meuDbContext)
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
            await Task.CompletedTask;
            return _meuDbcontext.Usuarios.FirstOrDefault(u => u.Email == email);  
        }
    }
}
