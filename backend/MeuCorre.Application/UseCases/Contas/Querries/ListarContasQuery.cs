using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Contas.Querries
{
    public record ContaResumoResponse
   (
       Guid Id,
       string Nome,
       TipoConta Tipo,
       decimal SaldoAtual,
       bool Ativo,
       string Cor,
       string Icone,
       
       decimal? LimiteDisponivel
   );
      public record ListarContasQuery : IRequest<List<ContaResumoResponse>>
    {
        public Guid UsuarioId { get; init; }
        public TipoConta? FiltrarPorTipo { get; init; }
        public bool ApenasAtivas { get; init; } = true;
        public string OrdenarPor { get; init; } = "Nome";
    }

    public class ListarContasQueryHandler : IRequestHandler<ListarContasQuery, List<ContaResumoResponse>>
    {
        private readonly IContaRepository _contaRepository;

        public ListarContasQueryHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<List<ContaResumoResponse>> Handle(ListarContasQuery request, CancellationToken cancellationToken)
        {
            
            List<Conta> contas = await _contaRepository.ObterPorUsuarioAsync(request.UsuarioId, request.ApenasAtivas);

          
            if (request.FiltrarPorTipo.HasValue)
            {
                contas = contas.Where(c => c.Tipo == request.FiltrarPorTipo.Value).ToList();
            }

           
            switch (request.OrdenarPor.ToLower())
            {
                case "saldo":
                    contas = contas.OrderByDescending(c => c.Saldo).ToList();
                    break;
                case "nome":
                default:
                    contas = contas.OrderBy(c => c.Nome).ToList();
                    break;
            }

          
            return contas.Select(conta =>
            {
                decimal? limiteDisponivel = null;

                if (conta.Tipo == TipoConta.CartaoCredito && conta.Limite.HasValue)
                {
                    
                    limiteDisponivel = conta.Limite.Value - conta.Saldo;

                    
                    if (limiteDisponivel < 0) limiteDisponivel = 0;
                }

                return new ContaResumoResponse(
                    Id: conta.Id,
                    Nome: conta.Nome,
                    Tipo: conta.Tipo,
                    SaldoAtual: conta.Saldo,
                    Ativo: conta.Ativo,
                    Cor: conta.Cor ?? string.Empty,
                    Icone: conta.Icone ?? string.Empty,
                    LimiteDisponivel: limiteDisponivel
                );
            }).ToList();
        }
    }

}
