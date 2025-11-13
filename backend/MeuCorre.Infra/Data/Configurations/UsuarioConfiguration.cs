using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuCorre.Infra.Data.Configurations
{
    internal class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            //Define o nome da tabela no banco de dados.
            builder.ToTable("Usuarios");

            //Define a chave primária.
            builder.HasKey(usuario => usuario.Id);

            //Define as propriedades da entidade e suas configurações.
            builder.Property(usuario => usuario.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(usuario => usuario.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(usuario => usuario.Senha)
                .IsRequired();

            builder.Property(usuario => usuario.DataNascimento)
                .IsRequired();

            builder.Property(usuario => usuario.Ativo)
                .IsRequired();

            builder.Property(usuario => usuario.DataCriacao)
                .IsRequired();

            builder.Property(usuario => usuario.DataAtualizacao)
                .IsRequired(false);

            //Define que o e-mail é único.
            builder.HasIndex(usuario => usuario.Email)
                .IsUnique();
        }
    }
}
