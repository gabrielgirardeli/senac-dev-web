using MediatR;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using MediatR;
using Application.UseCases.Contas.Commands;

namespace Application.UseCases.Contas.Commands
{
    public class AtualizarContaCommand : IRequest<(string, bool)>
    {
        public Guid ContaId { get; set; }
        public Guid UsuarioId { get; set; }

        // Campos editáveis
        public string? Nome { get; set; }      
        public decimal? Limite { get; set; }
        public int? DiaFechamento { get; set; }
        public int? DiaVencimento { get; set; }
        public string? Cor { get; set; }       
        public string? Icone { get; set; }    
        public bool? Ativo { get; set; }
        public TipoLimite? TipoLimite { get; internal set; }
    }

    public class AtualizarContaCommandHandler : IRequestHandler<AtualizarContaCommand, (string, bool)>
    {
        private readonly IContaRepository _contaRepository;

        public AtualizarContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }


        public async Task<(string, bool)> Handle(AtualizarContaCommand request, CancellationToken cancellationToken)
        {
          
            var conta = await _contaRepository.ObterPorIdEUsuarioAsync(request.ContaId, request.UsuarioId);

            if (conta == null)
                return ("Conta não encontrada ou não pertence ao usuário.", false);

           
            if (!string.IsNullOrWhiteSpace(request.Nome) && request.Nome.ToLower() != conta.Nome.ToLower())
            {
                if (await _contaRepository.ExisteContaComNomeAsync(request.UsuarioId, request.Nome, request.ContaId))
                {
                    return ($"Já existe outra conta com o nome '{request.Nome}' para este usuário.", false);
                }
            }

            // 3. ATUALIZAÇÃO DO DOMÍNIO - CHAMANDO OS MÉTODOS PÚBLICOS!

            // A. Atualizar Dados Gerais (Nome, Cor, Ícone)
            // NOTA: Você deve tratar os valores nulos para evitar erros no seu método AtualizarDados
            string novoNome = request.Nome ?? conta.Nome;
            string novaCor = request.Cor ?? conta.Cor;
            string novoIcone = request.Icone ?? conta.Icone;

            // CHAMADA AO MÉTODO DA ENTIDADE
            conta.AtualizarDados(novoNome, novaCor, novoIcone);

            // B. Atualizar Status Ativo (Adiciona um novo método simples na Entidade, se necessário)
            if (request.Ativo.HasValue && request.Ativo.Value != conta.Ativo)
            {
       
            }


          
            if (conta.EhCartaoCredito())
            {
                // Validação de Cartão (Recomendado mover para o Validator/Domain)
                if (request.Limite <= 0)
                    return ("O limite do cartão deve ser um valor positivo.", false);
                if (request.DiaVencimento < 1 || request.DiaVencimento > 31)
                    return ("O dia de vencimento deve ser entre 1 e 31.", false);

                // CHAMADA AO MÉTODO DA ENTIDADE
                conta.AtualizarLimiteECartao(
                    request.Limite ?? conta.Limite,
                    request.DiaFechamento ?? conta.DiaFechamento,
                    request.DiaVencimento ?? conta.DiaVencimento,
                    request.TipoLimite ?? conta.TipoLimite
                );
            }

            // NOTA: A DataAtualizacao é atualizada DENTRO dos métodos AtualizarDados e AtualizarLimiteECartao.

            // 4. Persistência
            _contaRepository.AdicionarAsync(conta);
            await _contaRepository.SalvarAsync();

            return ("Conta atualizada com sucesso.", true);
        }
    }
}
