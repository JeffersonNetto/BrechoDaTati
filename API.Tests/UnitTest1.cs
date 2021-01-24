using API.Controllers;
using API.Models;
using API.Repositories;
using Bogus;
using FluentAssertions;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace API.Tests
{
    public class UnitTest1
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

        //[Fact]
        //public async void AddMarca()
        //{
        //    Mock<MarcaRepository> mock = new Mock<MarcaRepository>(new Mock<Data.Context>().Object);

        //    mock.Setup(_ => _.Add(It.IsAny<Marca>())).Returns(Task.CompletedTask).Verifiable();

        //    await mock.Object.Add(new Marca());

        //    mock.Verify();
        //}

        //[Fact]
        //public async void RemoveMarca()
        //{
        //    Mock<MarcaRepository> mock = new Mock<MarcaRepository>(new Mock<Data.Context>().Object);

        //    mock.Setup(_ => _.Remove(It.IsAny<Marca>())).Returns(Task.CompletedTask).Verifiable();

        //    mock.Object.Remove(new Marca());

        //    mock.Verify();
        //}

        //[Fact]
        //public async void UpdateMarca()
        //{
        //    Mock<MarcaRepository> mock = new Mock<MarcaRepository>(new Mock<Data.Context>().Object);

        //    mock.Setup(_ => _.Update(It.IsAny<Marca>())).Returns(Task.CompletedTask).Verifiable();

        //    mock.Object.Update(new Marca());

        //    mock.Verify();
        //}

        //[Fact]
        //public async void GetMarcaShouldNotBeNull()
        //{
        //    Mock<MarcaRepository> mock = new Mock<MarcaRepository>(new Mock<Data.Context>().Object);

        //    mock.Setup(_ => _.GetById(It.Is<short>(_ => _ > 0))).ReturnsAsync(new Marca());

        //    var actual = await mock.Object.GetById(1);

        //    actual.Should().NotBeNull().And.BeOfType<Marca>();
        //}

        //[Fact]
        //public async void GetMarcaShouldBeNull()
        //{
        //    Mock<MarcaRepository> mock = new Mock<MarcaRepository>(new Mock<Data.Context>().Object);

        //    mock.Setup(_ => _.GetById(It.Is<short>(_ => _ < 0))).ReturnsAsync(null as Marca);

        //    var actual = await mock.Object.GetById(-1);

        //    actual.Should().BeNull();
        //}
    }
}
