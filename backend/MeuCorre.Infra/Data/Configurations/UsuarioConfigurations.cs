using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MeuCorre.Infra.Data.Configurations
{
    internal class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(usuario => usuario.Id);


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


            builder.HasIndex(usuario => usuario.Email)
                .IsUnique();
        }
    }
}
