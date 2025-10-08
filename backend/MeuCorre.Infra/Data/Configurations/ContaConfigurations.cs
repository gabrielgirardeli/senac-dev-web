using MeuCorre.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Infra.Data.Configurations
{
    public class ContaConfigurations : IEntityTypeConfiguration<Conta>
    {
        public void Configure(EntityTypeBuilder<Conta> builder)
        {
            
            builder.ToTable("Contas");

          
            builder.HasKey(c => c.Id);


          
            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(50); 

            builder.Property(c => c.Tipo)
                .IsRequired();

            builder.Property(c => c.UsuarioId)
                .IsRequired();

            builder.Property(c => c.Ativo)
                .IsRequired();

         
            builder.Property(c => c.Saldo)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(c => c.Limite)
                .HasColumnType("decimal(10,2)"); 

            builder.Property(c => c.TipoLimite)
                .IsRequired(false);

            builder.Property(c => c.DiaFechamento)
                .IsRequired(false);

            builder.Property(c => c.DiaVencimento)
                .IsRequired(false);

            builder.Property(c => c.Cor)
                .HasMaxLength(7) 
                .IsRequired(false);

            builder.Property(c => c.Icone)
                .HasMaxLength(20) 
                .IsRequired(false);

            
            builder.HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

           
            builder.HasIndex(c => c.UsuarioId);
            builder.HasIndex(c => c.Tipo);
            builder.HasIndex(c => c.Ativo);

            
            builder.HasIndex(c => new { c.UsuarioId, c.Ativo });
        }
    }
}

