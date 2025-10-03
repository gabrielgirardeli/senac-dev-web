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
   public class ContaRepository : IContaRepository
    {
        private readonly MeuDbContext _meuDbContext;

        public ContaRepository(MeuDbContext context) 
        {
            _meuDbContext = context;
        }

       
        public async Task<List<Conta>> ObterPorUsuarioAsync(Guid usuarioId, bool apenasAtivas = true)
        {
            var query = _meuDbContext.Contas
                .AsNoTracking()
                .Where(c => c.UsuarioId == usuarioId);

            if (apenasAtivas)
            {
                query = query.Where(c => c.Ativo);
            }

            return await query
             
                .OrderBy(c => c.Nome) 
                .ToListAsync();
        }

       
        public async Task<List<Conta>> ObterPorTipoAsync(Guid usuarioId, TipoConta tipo)
        {
            return await _meuDbContext.Contas
                .Where(c => c.UsuarioId == usuarioId && c.Tipo == tipo)
                .Where(c => c.Ativo) 
                .Include(c => c.Usuario)
                .OrderBy(c => c.Nome) 
                .AsNoTracking()
                .ToListAsync();
        }

     
        public async Task<Conta?> ObterPorIdEUsuarioAsync(Guid contaId, Guid usuarioId)
        {
            return await _meuDbContext.Contas
                .Where(c => c.Id == contaId && c.UsuarioId == usuarioId)
                .Include(c => c.Usuario)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        
        public async Task<bool> ExisteContaComNomeAsync(Guid usuarioId, string nome, Guid? contaIdExcluir = null)
        {
           
            var query = _meuDbContext.Contas
                .Where(c => c.UsuarioId == usuarioId && c.Nome.ToLower() == nome.ToLower());


            if (contaIdExcluir.HasValue)
            {
                query = query.Where(c => c.Id != contaIdExcluir.Value);
            }

            return await query.AnyAsync(); 
        }

       
        public async Task<decimal> CalcularSaldoTotalAsync(Guid usuarioId)
        {
           
            return await _meuDbContext.Contas
                .Where(c => c.UsuarioId == usuarioId && c.Ativo)
                .SumAsync(c => c.Saldo);
        }


        public async Task AdicionarAsync(Conta conta)
        {
            await _meuDbContext.Contas.AddAsync(conta);
        }
        public async Task SalvarAsync()
        {
            await _meuDbContext.SaveChangesAsync();
        }
        public void Remover(Conta conta)
        {
            // Usa o DbSet para marcar a entidade para remoção
            _meuDbContext.Contas.Remove(conta);
        }


    }
}
