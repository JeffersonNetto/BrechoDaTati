using System.Collections.Generic;
using System.Linq;

namespace API.Models
{
    public record Retorno<T>
    {
        public Retorno(IList<FluentValidation.Results.ValidationFailure> validationFailure = null) =>
            ConvertErrors(validationFailure);

        public Retorno(string errorMessage)
        {
            if (errorMessage.ToLower().Contains("uq_cpf"))
                Erros.Add("O CPF informado já está cadastrado na base de dados");
            else if (errorMessage.ToLower().Contains("uq_email"))
                Erros.Add("O e-mail informado já está cadastrado na base de dados");
        }

        public string Mensagem { get; init; }
        public T Dados { get; init; }
        public List<string> Erros { get; private init; } = new List<string>();

        private void ConvertErrors(IList<FluentValidation.Results.ValidationFailure> validationFailure) =>
            validationFailure?.ToList().ForEach(_ => Erros.Add(_.ErrorMessage));
    }
}
