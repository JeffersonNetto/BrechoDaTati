using API.Models;
using API.Validators;
using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using Xunit;

namespace API.Tests
{
    public class ClienteEnderecoValidatorTests
    {
        [Fact]
        public void ClienteEndereco_DeveSerValido()
        {
            //Arrange
            var validator = new ClienteEnderecoValidator();

            var clienteEndereco = new Faker<ClienteEndereco>("pt_BR")
                .RuleFor(m => m.Id, f => f.Random.Number(int.MaxValue))
                .RuleFor(m => m.Logradouro, f => f.Address.StreetName())
                .RuleFor(m => m.Numero, f => int.Parse(f.Address.BuildingNumber()))                
                .RuleFor(m => m.Complemento, f => f.Address.SecondaryAddress())
                .RuleFor(m => m.Bairro, f => f.Random.String2(10))
                .RuleFor(m => m.Cidade, f => f.Address.City())
                .RuleFor(m => m.Uf, f => f.Address.StateAbbr())
                .RuleFor(m => m.Cep, f => f.Address.ZipCode("########"))
                .RuleFor(m => m.Ativo, f => f.Random.Bool())
                .RuleFor(m => m.DataCriacao, f => f.Date.Recent())
                .Generate(1)[0];

            //Act
            var result = validator.Validate(clienteEndereco);

            //Assert
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void ClienteEndereco_DeveSerInvalido()
        {
            //Arrange
            var validator = new ClienteEnderecoValidator();

            var clienteEndereco = new Faker<ClienteEndereco>("pt_BR")
                .RuleFor(m => m.Id, f => f.Random.Number(0))
                .RuleFor(m => m.Logradouro, f => f.Random.String2(1))                
                .RuleFor(m => m.Complemento, f => f.Random.String2(101))
                .RuleFor(m => m.Bairro, f => f.Random.String2(101))
                .RuleFor(m => m.Cidade, f => f.Random.String2(1))
                .RuleFor(m => m.Uf, f => f.Random.String2(101))
                .RuleFor(m => m.Cep, f => f.Random.String2(101))                
                .Generate(1)[0];

            //Act
            var result = validator.Validate(clienteEndereco);

            //Assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
