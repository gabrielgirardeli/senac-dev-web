using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace MeuCorre.Infra.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly MeuDbContext _meuDbContext;
        public CategoriaRepository(MeuDbContext meuDbContext)
        {
            _meuDbContext = meuDbContext;
        }

        public async Task<Categoria?> ObterPorIdAsync(Guid categoriaId)
        {
            var categoria = await _meuDbContext.Categorias.FindAsync(categoriaId);
            return categoria;
        }

        public async Task<IList<Categoria>> ListarTodasPorUsuarioAsync(Guid usuarioId)
        {
            var listaCategorias =  _meuDbContext.Categorias
                .Where(c => c.UsuarioId == usuarioId);

            return await listaCategorias.ToListAsync();
        }

        public async Task<bool> ExisteAsync(Guid categoriaId)
        {
            var existe = await _meuDbContext.Categorias
                .AnyAsync(c => c.Id == categoriaId);

            return existe;
        }

        public async Task<bool> NomeExisteParaUsuarioAsync(string nome, TipoTransacao tipo, Guid usuarioId)
        {
            var existe = await _meuDbContext.Categorias
                .AnyAsync(
                            c => c.Nome == nome && 
                            c.UsuarioId == usuarioId &&
                            c.TipoDaTransacao == tipo
                        );

            return existe;
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

        public async Task RemoverAsync(Categoria categoria)
        {
            _meuDbContext.Categorias.Remove(categoria);
            await _meuDbContext.SaveChangesAsync();
        }
    }
}
