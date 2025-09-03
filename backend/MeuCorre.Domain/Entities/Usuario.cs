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

        public Usuario(string nome, string email, string senha, DateTime dataNascimento, bool ativo)
        {
            if(!TemIdadeMinima())
            {
                throw new Exception("Usuario deve ter no minimo 13 anos");
            }
            Nome = nome;
            Email = email;
            Senha = validarSenha(senha);
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

        private bool TemIdadeMinima()
        {
            var resultado = CalcularIdade() >= 13;
            return resultado;
        }
        public string validarSenha(string senha)
        {
            if(senha.Length < 6)
            {
                //todo fazer um tratamento de erro melhor
            }
            return senha;
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
