using MediatR;
using MeuCorre.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public record InativarContaResponse(bool Success, Guid ContaId);


    public record InativarContaCommand : IRequest<InativarContaResponse>
    {
        public Guid ContaId { get; init; }
        public Guid UsuarioId { get; init; }
    }


    public class InativarContaCommandHandler : IRequestHandler<InativarContaCommand, InativarContaResponse>
    {
        private readonly IContaRepository _contaRepository;

        public InativarContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<InativarContaResponse> Handle(InativarContaCommand request, CancellationToken cancellationToken)
        {

            var conta = await _contaRepository.ObterPorIdEUsuarioAsync(request.ContaId, request.UsuarioId);


            if (conta == null)
            {
                throw new InvalidOperationException($"Conta com ID {request.ContaId} não encontrada ou não pertence ao usuário.");
            }

            if (Math.Abs(conta.Saldo) > 0.00m)
            {
                throw new InvalidOperationException($"Não é possível inativar a conta '{conta.Nome}'. O saldo atual ({conta.Saldo:C}) deve ser zero.");
            }


            if (conta.Ativo == false)
            {
                return new InativarContaResponse(true, conta.Id);
            }


            conta.Inativar();

            // 5. Salvar
            _contaRepository.AdicionarAsync(conta);
            await _contaRepository.SalvarAsync();

            // 6. Retornar Response
            return new InativarContaResponse(true, conta.Id);
        }
    }
}
