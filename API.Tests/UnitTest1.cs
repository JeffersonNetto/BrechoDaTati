using Moq;
using System.Collections.Generic;
using Xunit;
using API.Models;
using API.Repositories;
using FluentAssertions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Tests
{
    public class UnitTest1
    {


        //[Fact]
        //public async void GetMarcasShouldNotBeNull()
        //{
        //    Mock<MarcaRepository> mock = new Mock<MarcaRepository>(new Mock<Data.Context>().Object);

        //    mock.Setup(_ => _.GetAll()).ReturnsAsync(new List<Marca>());

        //    var expected = await mock.Object.GetAll();

        //    var optionsBuilder = new DbContextOptionsBuilder<Data.Context>().UseSqlServer("Data Source=localhost;Initial Catalog=BrechoDaTati;User Id=sa;Password=123456;");

        //    var actual = await new MarcaRepository(new Data.Context(optionsBuilder.Options)).GetAll();

        //    expected.Should().BeEquivalentTo(actual);

        //    actual.Should().NotBeNull().And.BeOfType<List<Marca>>();
        //}

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
