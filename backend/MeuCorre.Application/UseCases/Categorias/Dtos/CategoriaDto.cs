using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeuCorre.Domain.Enums;

namespace MeuCorre.Application.UseCases.Categorias.Dtos
{
    public record CategoriaDto
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public string? Cor { get; set; }
        public string? Icone { get; set; }
        public bool Ativo { get; set; }
        public TipoTransacao Tipo { get; set; }
        public DateTime? UltimaAlteracao { get; set; }
    }
}
