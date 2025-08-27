using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Domain.Entities
{
    public class Usuario : Entidade
    {
        public  string Nome { get;private set; }
        public string Email { get;private set; }
        public string Senha { get; private set; }
        public DateTime DataNascimento { get;private set; }
        public bool Ativo { get; private set; }

        public Usuario(string nome, string email, DateTime dataNascimento, bool ativo)
        {
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            Ativo = ativo;

        }

        private int CalcularIdade()
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - DataNascimento.Year;

            if (DataNascimento.Date > hoje.AddYears(-idade))
                idade--;

            return idade;

            
        }

        private bool TemIdadeMenina()
        {
            var resultado = CalcularIdade() >= 13;
            return resultado;
        }

        public void AtivarUsuario()
        {
            Ativo = true;
            AtualizarDataModificacao();
        }
        public void InativarUsuario()
        {
            Ativo = false;
            AtualizarDataModificacao();

        }
    }
}
