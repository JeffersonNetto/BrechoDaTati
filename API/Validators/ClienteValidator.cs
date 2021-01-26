using API.Models;
using FluentValidation;

namespace API.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Informe o nome")
                .Length(2, 150).WithMessage("O nome deve possuir pelo menos 2 caracteres");

            RuleFor(x => x.Sobrenome)
                .NotEmpty().WithMessage("Informe o sobrenome")
                .Length(2, 150).WithMessage("O sobrenome deve possuir pelo menos 2 caracteres");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Informe um e-mail")
                .EmailAddress().WithMessage("Informe um e-mail válido");

            RuleFor(x => x.DataNascimento)
                .NotEqual(System.DateTime.MinValue).WithMessage("Informe a data de nascimento")
                .InclusiveBetween(System.DateTime.Now.AddYears(-100), System.DateTime.Now.AddYears(-18)).WithMessage("Data de nascimento inválida");

            RuleFor(x => x.Senha)
                .NotEmpty().WithMessage("Informe a senha")
                .Length(6, 20).WithMessage("A senha deve possuir entre 6 e 20 caracteres");

            RuleFor(x => x.Cpf)
                .NotEmpty().WithMessage("Informe o CPF");

            RuleFor(x => x.Cpf)
                .Must(BeValidCpf).When(x => !string.IsNullOrWhiteSpace(x.Cpf)).WithMessage("Informe um CPF válido");

            RuleFor(x => x.Celular)
                .Must(BeValidCelular).When(x => !string.IsNullOrWhiteSpace(x.Celular)).WithMessage("Informe um número de celular válido");
        }

        public bool BeValidCelular(string celular) =>
            celular?.Length == 11 && celular?[2] == '9';        
            
        public bool BeValidCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            return cpf.EndsWith(digito);
        }
    }
}
