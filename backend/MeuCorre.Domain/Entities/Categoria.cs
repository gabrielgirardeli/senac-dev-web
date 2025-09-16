
using MeuCorre.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public Categoria(string nome, string? descricao, string? cor, string? icone, bool ativo, Guid usuarioid,TipoTransacao tipo)
        {
            Nome = nome;
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            Ativo = true;
            UsuarioId = usuarioid;
            Tipo = new TipoTransacao();

        }

        public void AtualizarInformacoes(string nome, string descricao, string cor, string icone, bool ativo, TipoTransacao tipo)
        {
            Nome = nome.ToUpper();
            Descricao = descricao;
            Cor = cor;
            Icone = icone;
            Ativo = true;
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

    }

   
}
