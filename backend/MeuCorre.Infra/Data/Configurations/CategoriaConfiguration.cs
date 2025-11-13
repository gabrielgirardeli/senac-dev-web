using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuCorre.Infra.Data.Configurations
{
    internal class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            //Define o nome da tabela no banco de dados.
            builder.ToTable("Categorias");

            //Define a chave primária.
            builder.HasKey(categoria => categoria.Id);
            
            //Define as propriedades da entidade e suas configurações.
            builder.Property(categoria => categoria.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(categoria => categoria.Descricao)
                .HasMaxLength(255);

            builder.Property(categoria => categoria.Cor)
                .HasMaxLength(10);

            builder.Property(categoria => categoria.Icone)
                .HasMaxLength(10);

            builder.Property(categoria => categoria.TipoDaTransacao)
                .IsRequired();

            builder.Property(usuario => usuario.DataCriacao)
                .IsRequired();

            builder.Property(usuario => usuario.DataAtualizacao)
                .IsRequired(false);

            //Chaves Estrangeiras FK
            //Define o relacionamento entre Categoria e Usuario 
            builder.HasOne(categoria => categoria.Usuario)
                .WithMany(usuario => usuario.Categorias)
                .HasForeignKey(categoria => categoria.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
