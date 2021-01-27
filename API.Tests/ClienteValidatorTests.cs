using API.Models;
using API.Validators;
using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Xunit;

namespace API.Tests
{
    public class ClienteValidatorTests
    {
        [Fact]        
        public void CPF_DeveSerValido()
        {
            //Arrange
            var clienteValidator = new ClienteValidator();                        

            var cpf = new Faker("pt_BR").Person.Cpf(false);

            //Act
            bool result = clienteValidator.BeValidCpf(cpf);

            //Assert
            result.Should().BeTrue();            
        }

        [Theory]
        [InlineData("12345678910")]
        [InlineData("32165498700")]
        [InlineData("32165498")]
        [InlineData("3216549823165987")]
        public void CPF_DeveSerInvalido(string cpf)
        {
            //Arrange
            var clienteValidator = new ClienteValidator();            

            //Act
            bool result = clienteValidator.BeValidCpf(cpf);

            //Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("31991887646")]
        [InlineData("31985639002")]                
        public void Celular_DeveSerValido(string value)
        {
            //Arrange
            var clienteValidator = new ClienteValidator();

            //Act
            bool result = clienteValidator.BeValidCelular(value);            

            //Assert
            result.Should().BeTrue();
        }

        [Theory]
        [InlineData("319285478")]
        [InlineData("329654987453")]
        [InlineData("3193265")]
        [InlineData("3198754254")]
        public void Celular_DeveSerInvalido(string value)
        {
            //Arrange
            var clienteValidator = new ClienteValidator();

            //Act
            bool result = clienteValidator.BeValidCelular(value);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void Cliente_DeveSerValido()
        {
            //Arrange
            var clienteValidator = new ClienteValidator();

            var cliente = new Faker<Cliente>("pt_BR")
                .RuleFor(m => m.Id, f => System.Guid.NewGuid())
                .RuleFor(m => m.Nome, f => f.Person.FirstName)
                .RuleFor(m => m.Sobrenome, f => f.Person.LastName)
                .RuleFor(m => m.Cpf, f => f.Person.Cpf(false))
                .RuleFor(m => m.Email, f => f.Person.Email.ToLower())
                .RuleFor(m => m.DataNascimento, f => f.Date.Past(90, System.DateTime.Now.AddYears(-18)))
                .RuleFor(m => m.Senha, f => f.Random.AlphaNumeric(6))
                .RuleFor(m => m.Ativo, f => f.Random.Bool())
                .RuleFor(m => m.DataCriacao, f => f.Date.Recent())
                .Generate(1)[0];

            //Act
            var result = clienteValidator.Validate(cliente);

            //Assert
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void Cliente_DeveSerInvalido()
        {
            //Arrange
            var clienteValidator = new ClienteValidator();

            var cliente = new Faker<Cliente>("pt_BR")
                .RuleFor(m => m.Id, f => System.Guid.NewGuid())
                .RuleFor(m => m.Nome, f => f.Person.FirstName)
                .RuleFor(m => m.Sobrenome, f => f.Person.LastName)
                .RuleFor(m => m.Cpf, f => f.Person.Cpf(true))
                .RuleFor(m => m.Email, f => f.Person.Email.ToLower())
                .RuleFor(m => m.DataNascimento, f => f.Date.Past(1, System.DateTime.Now.AddYears(-17)))
                .RuleFor(m => m.Senha, f => f.Random.AlphaNumeric(5))
                .RuleFor(m => m.Ativo, f => f.Random.Bool())
                .RuleFor(m => m.DataCriacao, f => f.Date.Recent())
                .Generate(1)[0];

            //Act
            var result = clienteValidator.Validate(cliente);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
