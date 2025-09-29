
using MeuCorre.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Entities
{
   public class   Categoria: Entidade
    {
      

        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public TipoTransacao Tipo { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }
        public bool Ativo { get;set; }
        public Guid? UsuarioId { get; set; }
        
        public Categoria() { }
       

        public virtual Usuario Usuario { get; set; }

        public Categoria(string nome, string? descricao, string? cor, string? icone, Guid usuarioid, TipoTransacao tipo)
        {
            ValidarEntidadeCategoria(cor);

            Nome = nome;
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            Ativo = true;
            UsuarioId = usuarioid;
            Tipo = tipo;

        }

        public void AtualizarInformacoes(string nome, string descricao, string cor, string icone,  TipoTransacao tipo)
        {
            Nome = nome.ToUpper();
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            Tipo = new TipoTransacao();
            AtualizarDataModificacao();
        }

        public void Ativar()
        {
            Ativo = true;
            AtualizarDataModificacao();
        }
        public void Inativar()
        {
            Ativo = false;
            AtualizarDataModificacao();
        }

        private void ValidarEntidadeCategoria(string cor)
        {
            if(string.IsNullOrEmpty(cor))
            {
                return;
            }
            var corRegex = new Regex(@"^#?([0-9A-Fa-f]{6}|[0-9A-Fa-f]{8})$");

            if (!corRegex.IsMatch(cor))
            {
                throw new Exception(" A cor deve estar no formato hexadecimal");
            }
                
        }

    }

   
}
