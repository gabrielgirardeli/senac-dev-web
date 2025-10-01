using MeuCorre.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Entities
{
    public class  Conta : Entidade
    {
       
        public string Nome { get; private set; }

        public TipoConta Tipo { get; private set; }

        public decimal Saldo { get; private set; } = 0.00m;

        public Guid UsuarioId { get; private set; }

        public bool Ativo { get; private set; } = true;

        public decimal? Limite { get; private set; }

        public TipoLimite? TipoLimite { get; private set; } 

        public int? DiaFechamento { get; private set; }

        public int? DiaVencimento { get; private set; }

        public string Cor { get; private set; }

        public string Icone { get; private set; }

        public DateTime? DataAtualizacao { get; private set; }

        
        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; private set; }


        
        protected Conta() { }
        public Conta(Guid id, string nome, TipoConta tipo, decimal saldoInicial, Guid usuarioId, bool ativo = true,
                     decimal? limite = null, TipoLimite? tipoLimite = null, 
                     int? diaFechamento = null, int? diaVencimento = null, string cor = null, string icone = null)
        {
           
            this.Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            this.Tipo = tipo;
            this.Saldo = saldoInicial;
            this.UsuarioId = usuarioId;
            this.Ativo = ativo;

            
            this.Limite = limite;
            this.DiaFechamento = diaFechamento;
            this.DiaVencimento = diaVencimento;
            this.Cor = cor;
            this.Icone = icone;
            this.TipoLimite = tipoLimite; 
        }

        public bool EhCartaoCredito()
        {
            return this.Tipo == TipoConta.CartaoCredito;
        }

        
        public bool EhCarteira()
        {
            return this.Tipo == TipoConta.Carteira;
        }

        /// <summary>
        /// Calcula o limite de crédito disponível.
        /// </summary>
        public decimal CalcularLimiteDisponivel()
        {
            if (!EhCartaoCredito() || this.Limite == null)
            {
                return 0.00m;
            }
            // Limite disponível = Limite Total - Saldo Usado
            return this.Limite.Value - this.Saldo;
        }

        /// <summary>
        /// Valida se é possível debitar um valor da conta.
        /// </summary>
        public bool PodeFazerDebito(decimal valor)
        {
            if (valor <= 0 || this.Ativo == false)
            {
                return false;
            }

            if (EhCartaoCredito())
            {
                // Para Cartão: verifica se o valor cabe no limite disponível
                return valor <= CalcularLimiteDisponivel();
            }
            else
            {
                // Para outros tipos: verifica se há saldo suficiente
                return this.Saldo >= valor;
            }
        }

        /// <summary>
        /// Realiza o débito na conta.
        /// </summary>
        public void Debitar(decimal valor)
        {
            if (!PodeFazerDebito(valor))
            {
                throw new InvalidOperationException("Tentativa de débito não permitida. Verifique saldo ou limite.");
            }

            // Aumenta o saldo usado no Cartão, ou diminui o saldo disponível nas Outras Contas.
            this.Saldo += EhCartaoCredito() ? valor : -valor;
            this.DataAtualizacao = DateTime.UtcNow;
        }

        /// <summary>
        /// Realiza o crédito (depósito/pagamento) na conta.
        /// </summary>
        public void Creditar(decimal valor)
        {
            if (valor <= 0)
            {
                throw new ArgumentException("O valor a ser creditado deve ser positivo.");
            }

            if (this.Ativo == false)
            {
                throw new InvalidOperationException("Conta inativa não permite crédito.");
            }

            // Diminui o saldo usado no Cartão (pagamento), ou aumenta o saldo disponível nas Outras Contas (depósito).
            this.Saldo -= EhCartaoCredito() ? valor : -valor;
            this.DataAtualizacao = DateTime.UtcNow;
        }

        // --- Métodos de Atualização de Propriedades ---

        public void Inativar()
        {
            this.Ativo = false;
            this.DataAtualizacao = DateTime.UtcNow;
        }

        public void AtualizarDados(string novoNome, string novaCor, string novoIcone)
        {
            this.Nome = novoNome;
            this.Cor = novaCor;
            this.Icone = novoIcone;
            this.DataAtualizacao = DateTime.UtcNow;
        }

        /// <summary>
        /// Atualiza os dados de limite e fatura (apenas para Cartões de Crédito).
        /// </summary>
        public void AtualizarLimiteECartao(decimal? novoLimite, int? novoDiaFechamento, int? novoDiaVencimento, TipoLimite? novoTipoLimite) // <--- ATUALIZADO AQUI
        {
            if (EhCartaoCredito())
            {
                this.Limite = novoLimite;
                this.DiaFechamento = novoDiaFechamento;
                this.DiaVencimento = novoDiaVencimento;
                this.TipoLimite = novoTipoLimite; // <--- ATUALIZAÇÃO DA PROPRIEDADE
                this.DataAtualizacao = DateTime.UtcNow;
            }
        }
    }
}

