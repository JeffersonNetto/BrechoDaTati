using API.Controllers;
using API.Models;
using API.Repositories;
using API.Uow;
using API.Validators;
using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using FluentEmail.Core;
using FluentValidation;
using Moq;
using Moq.AutoMock;
using System;
using System.Threading.Tasks;
using Xunit;

namespace API.Tests
{
    public class AccountTests
    {
        [Fact]
        public async void Login_ShouldBeSuccessful()
        {
            //Arrange
            var mocker = new AutoMocker();

            var obj = new Faker<Cliente>("pt_BR")
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

            var controller = mocker.CreateInstance<AccountController>();
            mocker.GetMock<IClienteRepository>().Setup(c => c.GetByEmailSenha(obj.Email, obj.Senha)).ReturnsAsync(obj);            

            //Act
            var actual = await controller.Login(obj);

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(200);
            mocker.GetMock<IClienteRepository>().Verify(m => m.GetByEmailSenha(obj.Email, It.IsAny<string>()), Times.Once);            
        }

        [Fact]
        public async void Login_ShouldNotBeSuccessful()
        {
            //Arrange
            var mocker = new AutoMocker();

            var obj = new Faker<Cliente>("pt_BR")                                                
                .RuleFor(m => m.Email, f => f.Person.Email.ToLower())                
                .RuleFor(m => m.Senha, f => f.Random.AlphaNumeric(6))                
                .Generate(1)[0];

            var controller = mocker.CreateInstance<AccountController>();
            mocker.GetMock<IClienteRepository>().Setup(c => c.GetByEmailSenha(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((Cliente)null);

            //Act
            var actual = await controller.Login(obj);

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(404);
            mocker.GetMock<IClienteRepository>().Verify(m => m.GetByEmailSenha(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void Login_ShouldNotBeSuccessful2()
        {
            //Arrange
            var mocker = new AutoMocker();            

            var controller = mocker.CreateInstance<AccountController>();

            mocker.GetMock<IClienteRepository>()
                .Setup(c => c.GetByEmailSenha(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<NullReferenceException>();

            //Act
            var actual = await controller.Login(null as Usuario);

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(400);
            mocker.GetMock<IClienteRepository>().Verify(m => m.GetByEmailSenha(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async void Register_ShouldBeSuccessful()
        {
            //Arrange
            var mocker = new AutoMocker();

            var obj = new Faker<Cliente>("pt_BR")
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

            var result = new Faker<FluentValidation.Results.ValidationResult>()
                .RuleFor(v => v.IsValid, true)
                .Generate(1)[0];

            var controller = mocker.CreateInstance<AccountController>();        
            
            mocker.GetMock<ClienteValidator>()
                .Setup(v => v.ValidateAsync(obj, System.Threading.CancellationToken.None))
                .ReturnsAsync(result);

            //Act
            var actual = await controller.Register(obj);

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(200);
            mocker.GetMock<IClienteRepository>().Verify(m => m.Add(obj), Times.Once);
            mocker.GetMock<IUnitOfWork>().Verify(u => u.Commit(), Times.Once);
        }
    }
}
