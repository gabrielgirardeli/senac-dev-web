using MediatR;
using MeuCorre.Domain.Enums;
using MeuCorre.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application.UseCases.Contas.Commands
{
    public record AtualizarContaResponse(bool Success, Guid ContaId);

    public record  AtualizarContaCommand : IRequest<AtualizarContaResponse>
    {
        public Guid ContaId { get; set; }
        public Guid UsuarioId { get; set; } 

       
        public string? Nome { get; set   ; }
        public bool? Ativo { get; set; }
        public decimal? Limite { get; set; }
        public TipoLimite? TipoLimite { get; set; }
        public int? DiaFechamento { get; set; }
        public int? DiaVencimento { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }

    }

    public class AtualizarContaCommandHandler : IRequestHandler<AtualizarContaCommand, AtualizarContaResponse>
    {
        private readonly IContaRepository _contaRepository;

        public AtualizarContaCommandHandler(IContaRepository contaRepository)
        {
            _contaRepository = contaRepository;
        }

        public async Task<AtualizarContaResponse> Handle(AtualizarContaCommand request, CancellationToken cancellationToken)
        {
            // 1. Buscar conta por ID e USUÁRIO (Garantia de segurança)
            // Usamos o método sem AsNoTracking() para poder rastrear e atualizar a entidade.
            var conta = await _contaRepository.ObterPorIdEUsuarioAsync(request.ContaId);

            if (conta == null || conta.UsuarioId != request.UsuarioId)
            {
                throw new Exception($"Conta com ID {request.ContaId} não encontrada ou não pertence ao usuário.");
            }

            
            if (request.Nome != null && request.Nome.ToLower() != conta.Nome.ToLower())
            {
                if (await _contaRepository.ExisteContaComNomeAsync(request.UsuarioId, request.Nome, request.ContaId))
                {
                    throw new InvalidOperationException($"Já existe outra conta com o nome '{request.Nome}' para este usuário.");
                }
            }

          
            if (request.Nome != null) conta.Nome = request.Nome;
            if (request.Ativo.HasValue) conta.Ativo = request.Ativo.Value;
            if (request.Cor != null) conta.Cor = request.Cor;
            if (request.Icone != null) conta.Icone = request.Icone;

            if (conta.Tipo == TipoConta.CartaoCredito)
            {
               
                if (request.Limite.HasValue)
                {
                    if (request.Limite <= 0)
                        throw new ArgumentException("O limite do cartão deve ser um valor positivo.");

                    conta.Limite = request.Limite.Value;
                }

                if (request.TipoLimite.HasValue) conta.TipoLimite = request.TipoLimite.Value;
                if (request.DiaFechamento.HasValue) conta.DiaFechamento = request.DiaFechamento.Value;

                
                if (request.DiaVencimento.HasValue)
                {
                    if (request.DiaVencimento < 1 || request.DiaVencimento > 31)
                        throw new ArgumentException("O dia de vencimento deve ser entre 1 e 31.");

                    conta.DiaVencimento = request.DiaVencimento.Value;
                }
            }

            // 5. Atualizar DataAtualizacao e Salvar
            conta.UpdatedAt = DateTime.UtcNow; // Assume a existência de uma propriedade UpdatedAt

            _contaRepository.AdicionarAsync(conta);
            await _contaRepository.SalvarAsync();

            // 6. Retornar Response
            return new AtualizarContaResponse(true, conta.Id);
        }
    }
}
