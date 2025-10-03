using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public record ReativarContaResponse(bool Success, Guid ContaId);

    // --- Command (Request) ---
    /// <summary>
    /// Comando para reativar uma conta previamente inativada.
    /// </summary>
    public record ReativarContaCommand : IRequest<ReativarContaResponse>
    {
        public Guid ContaId { get; init; }
        public Guid UsuarioId { get; init; }
    }

    
    public class ReativarContaCommandHandler : IRequestHandler<ReativarContaCommand, ReativarContaResponse>
    {
        private readonly IContaRepository _contaRepository;

        public ReativarContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<ReativarContaResponse> Handle(ReativarContaCommand request, CancellationToken cancellationToken)
        {
           
            var conta = await _contaRepository.ObterPorIdEUsuarioAsync(request.ContaId, request.UsuarioId);

            
            if (conta == null)
            {
                throw new InvalidOperationException($"Conta com ID {request.ContaId} não encontrada ou não pertence ao usuário.");
            }

           
            if (conta.Ativo == true)
            {
                return new ReativarContaResponse(true, conta.Id);
            }

           
            conta.Ativar();

            // 5. Salvar
            _contaRepository.AdicionarAsync(conta);
            await _contaRepository.SalvarAsync();

            // 6. Retornar Response
            return new ReativarContaResponse(true, conta.Id);
        }
    }
}
