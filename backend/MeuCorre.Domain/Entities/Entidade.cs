using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Entities
{
     public abstract class Entidade
    {
        public Guid Id { get; private set; }
        public DateTime DataCriacao { get;private set; }
        public DateTime? DataAtualizacao { get;private set; }


        protected Entidade()
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
        }

        protected Entidade(Guid id)
        {
            Id = id;
            DataAtualizacao = DateTime.Now;
        }
        public void AtualizarDataModificacao()
        {
            DataAtualizacao = DateTime.Now;
          
        }

    }
}
