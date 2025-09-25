using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Infra.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly MeuDbContext _meuDbContext;

        public CategoriaRepository(MeuDbContext meuDbContext)
        {
            _meuDbContext = meuDbContext;
        }
       public async Task AdicionarAsync(Categoria categoria)
        {
            _meuDbContext.Categorias.Add(categoria);
            await _meuDbContext.SaveChangesAsync();
        }

       public async Task AtualizarAsync(Categoria categoria)
        {
           _meuDbContext.Categorias.Update(categoria);
            await _meuDbContext.SaveChangesAsync();
        }

       public async Task<bool> ExisteAsync(Guid categoriaId)
        {
            var existe = await _meuDbContext.Categorias.AnyAsync(c => c.Id == categoriaId);
            return existe;
        }

        public async Task<IEnumerable<Categoria>> ListarTodasPorUsuarioAsync(Guid usuarioId)
        {
            var ListaCategorias =
               _meuDbContext.Categorias.Where(c => c.UsuarioId == usuarioId);

            return await ListaCategorias.ToListAsync();
           
        }

       public async Task<bool> NomeExisteParaUsuarioAsync(string nome, TipoTransacao tipo, Guid usuarioId)
        {
            var existe = await _meuDbContext.Categorias.AnyAsync(c => c.Nome == nome && c.Tipo == tipo && c.UsuarioId == usuarioId);
            return existe;
        }

      public  async Task<Categoria> ObterPorIdAsync(Guid categoriaId)
        {
            var categoria =
               await _meuDbContext.Categorias.FirstOrDefaultAsync(c => c.Id == categoriaId );
            return categoria;
        }

       public async Task RemoverAsync(Categoria categoriaId)
        {
           _meuDbContext.Categorias.Remove(categoriaId);
            await _meuDbContext.SaveChangesAsync();
        }
    }
}
