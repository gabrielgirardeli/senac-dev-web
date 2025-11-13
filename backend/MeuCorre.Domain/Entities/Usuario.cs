using System.Text.RegularExpressions;

namespace MeuCorre.Domain.Entities
{
    public class Usuario : Entidade
    {
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public bool Ativo { get; private set; }

        // Propriedade de navegação para a entidade Categoria pois
        // o usuário pode ter várias categorias
        public virtual ICollection<Categoria> Categorias { get; set; }


        //Construtor para criar um novo usuário.
        //Construtor é a primeira coisa que é executada quando uma classe é instanciada.
        public Usuario(string nome, string email, string senha, DateTime dataNascimento, bool ativo)
        {
            ValidarEntidadeUsuario(email, senha, dataNascimento);
            
            Nome = nome;
            Email = email;
            Senha = senha;
            DataNascimento = dataNascimento;
            Ativo = ativo;
        }

        public void AtualizarInformacoes(string nome, DateTime dataNascimento)
        {
            ValidarIdadeMinina(dataNascimento);
            Nome = nome;
            DataNascimento = dataNascimento;
            AtualizarDataMoficacao();
        }
        public void AtivarUsuario()
        {
            Ativo = true;
            AtualizarDataMoficacao();
        }
        public void InativarUsuario()
        {
            Ativo = false;
            AtualizarDataMoficacao();
        }



        private void ValidarEntidadeUsuario(string email, string senha, DateTime nascimento)
        {
            ValidarIdadeMinina(nascimento);
            ValidarSenha(senha);
            ValidarEmail(email);
        }
        private void ValidarIdadeMinina(DateTime nascimento)
        {
            var hoje = DateTime.Today;
            var idade = hoje.Year - nascimento.Year;

            if (nascimento.Date > hoje.AddYears(-idade))
                idade--;

            if (idade < 13)
            {
                //Interrompe o processo devolvendo o erro
                throw new Exception("Usuário deve ter no minimo 13 anos");
            }
        }
        public void ValidarSenha(string senha)
        {
            //Regra de dnegocio: pelo menos uma letra e um número.
            if (!Regex.IsMatch(senha, "[a-z]"))
            {
                throw new Exception("A senha deve contar pelo menos uma letra minuscula");
            }
            if (!Regex.IsMatch(senha, "[A-Z]"))
            {
                throw new Exception("A senha deve contar pelo menos uma letra maiuscula");
            }
            if (!Regex.IsMatch(senha,"[0-9]"))
            {
                throw new Exception("A senha deve contar pelo menos um números");
            }
        }
        private void ValidarEmail(string email)
        {
            //Regra de negocio: email deve conter @ e um domínio válido.
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new Exception("Email em formato inválido");
            }
        }
    }
}
