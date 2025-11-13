namespace MeuCorre.Domain.Entities
{
    public abstract class Entidade
    {
        public Guid Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime? DataAtualizacao { get; private set; }

        //Construtur que cria uma nova entidade
        protected Entidade() 
        {
            Id = Guid.NewGuid();
            DataCriacao = DateTime.Now;
        }

        //Construtor que cria entidades que já existem
        protected Entidade(Guid id)
        {
            Id = id;
            DataAtualizacao = DateTime.Now;
        }

        public void AtualizarDataMoficacao()
        {
            DataAtualizacao = DateTime.Now;
        }
    }
}
