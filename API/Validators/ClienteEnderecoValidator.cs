using API.Models;
using FluentValidation;

namespace API.Validators
{
    public class ClienteEnderecoValidator : AbstractValidator<ClienteEndereco>
    {
        public ClienteEnderecoValidator()
        {
            RuleFor(x => x.Logradouro)
                .NotEmpty().WithMessage("Informe o logradouro")
                .Length(2, 150).WithMessage("O logradouro deve possuir pelo menos 2 caracteres");

            RuleFor(x => x.Numero)
                .InclusiveBetween(1, int.MaxValue).When(x => x.Numero.HasValue).WithMessage("Informe um número válido");

            RuleFor(x => x.Complemento)
                .Length(1, 100).When(x => !string.IsNullOrWhiteSpace(x.Complemento)).WithMessage("O complemento deve possuir no máximo 200 caracteres");

            RuleFor(x => x.Bairro)
                .Length(2, 100).When(x => !string.IsNullOrWhiteSpace(x.Bairro)).WithMessage("O bairro deve possuir entre 2 e 100 caracteres");

            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("Informe a cidade")
                .Length(2, 100).WithMessage("A cidade deve possuir entre 2 e 100 caracteres");

            RuleFor(x => x.Uf)
                .NotEmpty().WithMessage("Informe o estado (UF)")
                .Length(2).WithMessage("O estado (UF) deve possuir 2 caracteres");

            RuleFor(x => x.Cep)                
                .Length(8).When(x => !string.IsNullOrWhiteSpace(x.Cep)).WithMessage("O cep deve possuir 8 dígitos");
        }
    }
}
