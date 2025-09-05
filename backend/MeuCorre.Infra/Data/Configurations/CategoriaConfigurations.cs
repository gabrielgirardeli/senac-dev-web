using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeuCorre.Infra.Data.Configurations
{
    internal class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(categoria => categoria.Id);
            


            builder.Property(categoria => categoria.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(categoria => categoria.Descricao)
                  .IsRequired(false)
                  .HasMaxLength(255);

            builder.Property(categoria => categoria.Cor)
               .IsRequired();

            builder.Property(categoria => categoria.Icone)
                .IsRequired();

            builder.Property(categoria => categoria.TipoTransacao)
                .IsRequired();
            
                


            builder.Property(categoria => categoria.UsuarioId)
               .IsRequired();

            builder.Property(categoria => categoria.DataCriacao)
               .IsRequired();


            builder.Property(categoria => categoria.DataAtualizacao)
               .IsRequired(false);

            builder.HasOne(categoria => categoria.Usuario)
                .WithMany(usuario => usuario.Categorias)
                .HasForeignKey(categoria => categoria.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);




        }
    }
}
