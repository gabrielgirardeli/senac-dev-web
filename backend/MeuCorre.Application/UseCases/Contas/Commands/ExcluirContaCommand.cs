using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    // --- DTO de Resposta (Vazio, indica sucesso na exclusão) ---
    // Usaremos Unit.Value, o padrão para comandos MediatR que não retornam dados.
    // Mas para manter o padrão anterior, usamos um record de sucesso simples.
    public record ExcluirContaResponse(bool Success, Guid ContaId);

    // --- Command (Request) ---
    /// <summary>
    /// Comando para excluir uma conta permanentemente.
    /// Exige Saldo Zero e confirmação explícita.
    /// </summary>
    public record ExcluirContaCommand : IRequest<ExcluirContaResponse>
    {
        public Guid ContaId { get; init; }
        public Guid UsuarioId { get; init; }
        public bool Confirmar { get; init; } 
    }

   
    public class ExcluirContaCommandHandler : IRequestHandler<ExcluirContaCommand, ExcluirContaResponse>
    {
        private readonly IContaRepository _contaRepository;
       
        public ExcluirContaCommandHandler(IContaRepository contaRepository )
        {
            _contaRepository = contaRepository;
            
        }

        public async Task<ExcluirContaResponse> Handle(ExcluirContaCommand request, CancellationToken cancellationToken)
        {
            
            if (!request.Confirmar)
            {
                
                throw new InvalidOperationException("A exclusão permanente requer que o parâmetro 'Confirmar' seja true.");
            }

            
            var conta = await _contaRepository.ObterPorIdEUsuarioAsync(request.ContaId, request.UsuarioId);

            if (conta == null)
            {
                
                throw new InvalidOperationException($"Conta com ID {request.ContaId} não encontrada ou não pertence ao usuário.");
            }

           
            if (Math.Abs(conta.Saldo) > 0.00m)
            {
                
                throw new InvalidOperationException($"Não é possível excluir a conta '{conta.Nome}'. O saldo atual ({conta.Saldo:C}) deve ser zero.");
            }

            

            _contaRepository.Remover(conta);

            await _contaRepository.SalvarAsync();

            
            return new ExcluirContaResponse(true, conta.Id);
        }
    }
}
