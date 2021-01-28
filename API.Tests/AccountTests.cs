using API.Controllers;
using API.Models;
using API.Repositories;
using API.Uow;
using API.Validators;
using Bogus;
using Bogus.Extensions.Brazil;
using FluentAssertions;
using FluentEmail.Core;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.AutoMock;
using System;
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

            mocker.GetMock<IMemoryCache>()
                .Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(Mock.Of<ICacheEntry>());

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
                .RuleFor(m => m.Email, f => "jeffersonneto@live.com")
                .RuleFor(m => m.DataNascimento, f => f.Date.Past(90, System.DateTime.Now.AddYears(-18)))
                .RuleFor(m => m.Senha, f => f.Random.AlphaNumeric(6))
                .RuleFor(m => m.Ativo, f => f.Random.Bool())
                .RuleFor(m => m.DataCriacao, f => f.Date.Recent())
                .Generate(1)[0];

            var controller = mocker.CreateInstance<AccountController>();            

            //Act
            var actual = await controller.Register(obj, new ClienteValidator(), mocker.GetMock<Email>().Object);

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(200);
            mocker.GetMock<IClienteRepository>().Verify(m => m.Add(obj), Times.Once);
            mocker.GetMock<IUnitOfWork>().Verify(u => u.Commit(), Times.Once);
            mocker.GetMock<Email>().Verify(u => u.SendAsync(null), Times.Once);
        }

        [Fact]
        public async void Register_ShouldNotBeSuccessful()
        {
            //Arrange
            var mocker = new AutoMocker();

            var obj = new Faker<Cliente>("pt_BR")
                .RuleFor(m => m.Id, f => System.Guid.NewGuid())
                .RuleFor(m => m.Nome, f => f.Person.FirstName)
                .RuleFor(m => m.Sobrenome, f => f.Person.LastName)
                .RuleFor(m => m.Cpf, f => f.Person.Cpf(true))
                .RuleFor(m => m.Email, f => "jeffersonneto@live.com")
                .RuleFor(m => m.DataNascimento, f => f.Date.Past(1))
                .RuleFor(m => m.Senha, f => f.Random.AlphaNumeric(5))
                .RuleFor(m => m.Ativo, f => f.Random.Bool())
                .RuleFor(m => m.DataCriacao, f => f.Date.Recent())
                .Generate(1)[0];

            var controller = mocker.CreateInstance<AccountController>();                        

            //Act
            var actual = await controller.Register(obj, new ClienteValidator(), mocker.GetMock<Email>().Object);

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(422);
            mocker.GetMock<IClienteRepository>().Verify(m => m.Add(obj), Times.Never);
            mocker.GetMock<IUnitOfWork>().Verify(u => u.Commit(), Times.Never);
            mocker.GetMock<Email>().Verify(u => u.SendAsync(null), Times.Never);
        }

        [Fact]
        public async void Register_ShouldNotBeSuccessful2()
        {
            //Arrange
            var mocker = new AutoMocker();

            var controller = mocker.CreateInstance<AccountController>();

            //Act
            var actual = await controller.Register(null as Cliente, null as ClienteValidator, mocker.GetMock<Email>().Object);

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(400);            
            mocker.GetMock<IUnitOfWork>().Verify(u => u.Rollback(), Times.Once);            
        }

        [Fact]
        public void GetFromCache_ShouldBeSuccessful()
        {
            //Arrange
            var mocker = new AutoMocker();

            var controller = mocker.CreateInstance<AccountController>();

            object value;

            mocker.GetMock<IMemoryCache>().Setup(_ => _.TryGetValue(It.IsAny<object>(), out value)).Returns(true);

            //Act
            var actual = controller.GetFromCache(It.IsAny<string>());

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(200);            
        }

        [Fact]
        public void GetFromCache_ShouldNotBeSuccessful()
        {
            //Arrange
            var mocker = new AutoMocker();

            var controller = mocker.CreateInstance<AccountController>();

            object value;

            mocker.GetMock<IMemoryCache>().Setup(_ => _.TryGetValue(It.IsAny<object>(), out value)).Returns(false);

            //Act
            var actual = controller.GetFromCache(It.IsAny<string>());

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(404);
        }

        [Fact]
        public void SetToCache_ShouldBeSuccessful()
        {
            //Arrange
            var mocker = new AutoMocker();

            var controller = mocker.CreateInstance<AccountController>();

            mocker.GetMock<IMemoryCache>()
                .Setup(m => m.CreateEntry(It.IsAny<object>()))                
                .Returns(Mock.Of<ICacheEntry>());

            //Act
            var actual = controller.SetToCache("abc", new { });

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(200);
        }

        [Fact]
        public async void RefreshToken_ShouldBeSuccessful()
        {
            //Arrange
            var mocker = new AutoMocker();

            var controller = mocker.CreateInstance<AccountController>();

            var obj = new Faker<Cliente>("pt_BR")
                .RuleFor(m => m.Id, f => System.Guid.NewGuid())
                .RuleFor(m => m.Nome, f => f.Person.FirstName)
                .RuleFor(m => m.Sobrenome, f => f.Person.LastName)
                .RuleFor(m => m.Cpf, f => f.Person.Cpf(false))
                .RuleFor(m => m.Email, f => f.Internet.Email())
                .RuleFor(m => m.DataNascimento, f => f.Date.Past(90, System.DateTime.Now.AddYears(-18)))
                .RuleFor(m => m.Senha, f => f.Random.AlphaNumeric(6))
                .RuleFor(m => m.Ativo, f => f.Random.Bool())
                .RuleFor(m => m.DataCriacao, f => f.Date.Recent())
                .Generate(1)[0];

            mocker.GetMock<IClienteRepository>()
                .Setup(_ => _.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(obj);

            mocker.GetMock<IMemoryCache>()
                .Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(Mock.Of<ICacheEntry>());

            //Act
            var actual = await controller.RefreshToken(Guid.NewGuid());

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(200);
        }

        [Fact]
        public async void RefreshToken_ShouldNotBeSuccessful()
        {
            //Arrange
            var mocker = new AutoMocker();

            var controller = mocker.CreateInstance<AccountController>();

            mocker.GetMock<IClienteRepository>()
                .Setup(_ => _.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(null as Cliente);

            //Act
            var actual = await controller.RefreshToken(Guid.NewGuid());

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(404);
        }

        [Fact]
        public async void RefreshToken_ShouldNotBeSuccessful2()
        {
            //Arrange
            var mocker = new AutoMocker();

            var controller = mocker.CreateInstance<AccountController>();

            mocker.GetMock<IClienteRepository>()
                .Setup(_ => _.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(new Cliente());

            //Act
            var actual = await controller.RefreshToken(Guid.NewGuid());

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(400);
        }
    }
}
