using MediatR;
using MeuCorre.Domain.Entities;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    // ... (CriarContaResponse e CriarContaCommand permanecem inalterados)

    public record CriarContaResponse(Guid Id, string Nome, decimal Saldo);

    public record CriarContaCommand : IRequest<CriarContaResponse>
    {
        public Guid UsuarioId { get; init; }
        public string Nome { get; init; } = string.Empty;
        public TipoConta Tipo { get; init; }
        public decimal SaldoInicial { get; init; } = 0.00m;
        public bool Ativo { get; init; } = true;

        public decimal? Limite { get; init; }
        public TipoLimite? TipoLimite { get; init; }
        public int? DiaFechamento { get; init; }
        public int? DiaVencimento { get; init; }
        public string? Cor { get; init; }
        public string? Icone { get; init; }
    }

    public class CriarContaCommandHandler : IRequestHandler<CriarContaCommand, CriarContaResponse>
    {
        private readonly IContaRepository _contaRepository;

        public CriarContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<CriarContaResponse> Handle(CriarContaCommand request, CancellationToken cancellationToken)
        {
          
            if (string.IsNullOrWhiteSpace(request.Nome) || request.Nome.Length < 2 || request.Nome.Length > 50)
            {
                throw new ArgumentException("O nome da conta é obrigatório e deve ter entre 2 e 50 caracteres.");
            }

            
            if (await _contaRepository.ExisteContaComNomeAsync(request.UsuarioId, request.Nome))
            {
                throw new InvalidOperationException($"Já existe uma conta com o nome '{request.Nome}' para este usuário.");
            }

          
            if (!Enum.IsDefined(typeof(TipoConta), request.Tipo))
            {
                throw new ArgumentException("O tipo de conta informado é inválido.");
            }

           
            if (!string.IsNullOrWhiteSpace(request.Cor) && !System.Text.RegularExpressions.Regex.IsMatch(request.Cor, "^#([A-Fa-f0-9]{6})$"))
            {
                throw new ArgumentException("A cor deve estar no formato hexadecimal (#RRGGBB).");
            }

           
            bool ehCartao = request.Tipo == TipoConta.CartaoCredito;
            if (ehCartao)
            {
             
                if (request.Limite == null || request.Limite <= 0)
                {
                    throw new ArgumentException("O limite de crédito é obrigatório e deve ser maior que zero para Cartões de Crédito.");
                }

                
                if (request.DiaVencimento == null || request.DiaVencimento < 1 || request.DiaVencimento > 31)
                {
                    throw new ArgumentException("O dia de vencimento é obrigatório e deve ser um dia válido do mês (entre 1 e 31) para Cartões de Crédito.");
                }
            }

            


            

            decimal saldoProcessado = request.SaldoInicial;
            if (ehCartao && saldoProcessado < 0)
            {
               
                throw new InvalidOperationException("O saldo inicial de um Cartão de Crédito não pode ser negativo.");
            }

            int? diaFechamento = request.DiaFechamento;
            if (ehCartao && !diaFechamento.HasValue && request.DiaVencimento.HasValue)
            {
                int diaVencimento = request.DiaVencimento.Value;
                int diaCalculado = diaVencimento - 10;
                diaFechamento = diaCalculado > 0 ? diaCalculado : (diaCalculado + 30);
            }


            var novaConta = new Conta(
                id: Guid.NewGuid(),
                nome: request.Nome,
                tipo: request.Tipo,
                saldoInicial: saldoProcessado,
                usuarioId: request.UsuarioId,
                ativo: request.Ativo,
                limite: request.Limite,
                tipoLimite: request.TipoLimite,
                diaFechamento: diaFechamento,
                diaVencimento: request.DiaVencimento,
                cor: request.Cor,
                icone: request.Icone
            );

            await _contaRepository.AdicionarAsync(novaConta);
            await _contaRepository.SalvarAsync();

           

            return new CriarContaResponse(
                Id: novaConta.Id,
                Nome: novaConta.Nome,
                Saldo: novaConta.Saldo
            );
        }
    }
}