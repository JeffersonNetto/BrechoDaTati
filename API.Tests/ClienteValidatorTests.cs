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
    }
}
