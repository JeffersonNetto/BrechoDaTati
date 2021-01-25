using API.Controllers;
using API.Models;
using API.Repositories;
using API.Uow;
using Bogus;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace API.Tests
{
    public class MarcaTests
    {
        [Fact]
        public async void GetMarcasShouldNotBeNull()
        {
            //Arrange
            var mocker = new AutoMocker();

            var expected = new Faker<Marca>("pt_BR")
                .RuleFor(m => m.Id, f => f.Random.Short(1, 100))
                .RuleFor(m => m.Nome, f => f.Company.CompanyName())
                .RuleFor(m => m.Ativo, f => f.Random.Bool())
                .RuleFor(m => m.DataCriacao, f => f.Date.Recent())
                .Generate(5);

            mocker.GetMock<IRepositoryBase<Marca>>().Setup(_ => _.GetAll()).ReturnsAsync(expected);

            var controller = mocker.CreateInstance<MarcaController>();

            //Act
            var actual = await controller.Get();

            //Assert
            actual.GetType().GetProperty("Value").GetValue(actual).Should().BeEquivalentTo(expected);
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(200);
        }

        [Fact]
        public async void AddMarca()
        {
            //Arrange
            var mocker = new AutoMocker();

            var marca = new Faker<Marca>("pt_BR")
                .RuleFor(m => m.Id, f => f.Random.Short(1, 100))
                .RuleFor(m => m.Nome, f => f.Company.CompanyName())
                .RuleFor(m => m.Ativo, f => f.Random.Bool())
                .RuleFor(m => m.DataCriacao, f => f.Date.Recent())
                .Generate(1)[0];                        

            var controller = mocker.CreateInstance<MarcaController>();

            //Act
            var actual = await controller.Post(marca);

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(201);
            mocker.GetMock<IRepositoryBase<Marca>>().Verify(m => m.Add(marca), Times.Once);
            mocker.GetMock<IUnitOfWork>().Verify(u => u.Commit(), Times.Once);
        }

        [Fact]
        public async void RemoveMarca()
        {
            //Arrange
            var mocker = new AutoMocker();

            var marca = new Faker<Marca>("pt_BR")
                .RuleFor(m => m.Id, f => f.Random.Short(1, 100))
                .RuleFor(m => m.Nome, f => f.Company.CompanyName())
                .RuleFor(m => m.Ativo, f => f.Random.Bool())
                .RuleFor(m => m.DataCriacao, f => f.Date.Recent())
                .Generate(1)[0];

            var controller = mocker.CreateInstance<MarcaController>();

            mocker.GetMock<IRepositoryBase<Marca>>().Setup(m => m.GetById(marca.Id)).ReturnsAsync(marca);

            //Act
            var actual = await controller.Delete(marca.Id);

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(204);
            mocker.GetMock<IRepositoryBase<Marca>>().Verify(m => m.GetById(marca.Id), Times.Once);
            mocker.GetMock<IRepositoryBase<Marca>>().Verify(m => m.Remove(marca), Times.Once);
            mocker.GetMock<IUnitOfWork>().Verify(u => u.Commit(), Times.Once);
        }

        [Fact]
        public async void UpdateMarca()
        {
            //Arrange
            var mocker = new AutoMocker();

            var marca = new Faker<Marca>("pt_BR")
                .RuleFor(m => m.Id, f => f.Random.Short(1, 100))
                .RuleFor(m => m.Nome, f => f.Company.CompanyName())
                .RuleFor(m => m.Ativo, f => f.Random.Bool())
                .RuleFor(m => m.DataCriacao, f => f.Date.Recent())
                .Generate(1)[0];

            var controller = mocker.CreateInstance<MarcaController>();

            //Act
            var actual = await controller.Put(marca.Id, marca);

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(200);
            mocker.GetMock<IRepositoryBase<Marca>>().Verify(m => m.Update(marca), Times.Once);
            mocker.GetMock<IUnitOfWork>().Verify(u => u.Commit(), Times.Once);
        }

        [Fact]
        public async void GetMarcaShouldNotBeNull()
        {
            //Arrange
            var mocker = new AutoMocker();

            var expected = new Faker<Marca>("pt_BR")
                .RuleFor(m => m.Id, f => f.Random.Short(1, 100))
                .RuleFor(m => m.Nome, f => f.Company.CompanyName())
                .RuleFor(m => m.Ativo, f => f.Random.Bool())
                .RuleFor(m => m.DataCriacao, f => f.Date.Recent())
                .Generate(1)[0];

            mocker.GetMock<IRepositoryBase<Marca>>().Setup(m => m.GetById(It.Is<short>(_ => _ > 0))).ReturnsAsync(expected);

            var controller = mocker.CreateInstance<MarcaController>();

            //Act
            var actual = await controller.Get(1);

            //Assert
            actual.GetType().GetProperty("Value").GetValue(actual).Should().BeEquivalentTo(expected);
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(200);
        }

        [Fact]
        public async void GetMarcaShouldBeNull()
        {
            //Arrange
            var mocker = new AutoMocker();

            mocker.GetMock<IRepositoryBase<Marca>>().Setup(m => m.GetById(It.Is<short>(_ => _ <= 0))).ReturnsAsync((Marca)null);

            var controller = mocker.CreateInstance<MarcaController>();

            //Act
            var actual = await controller.Get(0);

            //Assert            
            actual.GetType().GetProperty("StatusCode").GetValue(actual).Should().BeEquivalentTo(404);
        }
    }
}
