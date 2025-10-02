using MediatR;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Contas.Querries
{

   
    public record ContaDetalheDto
    (
        Guid Id,
        string Nome,
        TipoConta Tipo,
        decimal Saldo,
        Guid UsuarioId,
        bool Ativo,
        decimal? Limite,
        TipoLimite? TipoLimite,
        int? DiaFechamento,
        int? DiaVencimento,
        string? Cor,
        string? Icone

    
    );

   
    public record ObterContaQuery : IRequest<ContaDetalheDto>
    {
        public Guid ContaId { get; init; }
        public Guid UsuarioId { get; init; }
    }


    public class ObterContaQueryHandler : IRequestHandler<ObterContaQuery, ContaDetalheDto>
    {
        private readonly IContaRepository _contaRepository;

        public ObterContaQueryHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<ContaDetalheDto> Handle(ObterContaQuery request, CancellationToken cancellationToken)
        {
           
            var conta = await _contaRepository.ObterPorIdEUsuarioAsync(request.ContaId, request.UsuarioId);

           
            if (conta == null)
            {
                
                throw new Exception($"Conta com ID {request.ContaId} não encontrada ou não pertence ao usuário logado.");
            }

           
            return new ContaDetalheDto(
                Id: conta.Id,
                Nome: conta.Nome,
                Tipo: conta.Tipo,
                Saldo: conta.Saldo,
                UsuarioId: conta.UsuarioId,
                Ativo: conta.Ativo,
                Limite: conta.Limite,
                TipoLimite: conta.TipoLimite,
                DiaFechamento: conta.DiaFechamento,
                DiaVencimento: conta.DiaVencimento,
                Cor: conta.Cor,
                Icone: conta.Icone
            );
        }
    }
}
