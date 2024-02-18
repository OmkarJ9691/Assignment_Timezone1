using Assignment.Application.Features.Guest.Commands;
using Assignment.Application.Features.Guest.Queries;
using Assignment.Domain;
using Assignment.Web.Controllers;
using AutoFixture;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
namespace Assignment.UnitTest;

public class GuestControllerTest
{
    [Fact]
    public async void GetAllGuests_ShouldReturn200Status()
    {
        //Arrange
        var fixture = new Fixture();
        var data = fixture.Create<List<Domain.GuestModel>>();
        var _mediator = new Mock<IMediator>();
        _mediator.Setup(_ => _.Send(It.IsAny<IRequest<IEnumerable<Domain.GuestModel>>> (), It.IsAny<CancellationToken>()))
            .ReturnsAsync(data);

        var guest = new GuestController(_mediator.Object);

        //Act
        var result = await guest.GetAllGuests();

        //Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult).StatusCode.Should().Be(200);
    }

    [Fact]
    public async void GetGuestById_ShouldReturn200Status()
    {
        //Arrange
        var fixture = new Fixture();
        var data = fixture.Create<Domain.GuestModel>();
        data.Id = Guid.NewGuid();
        var _mediator = new Mock<IMediator>();
        _mediator.Setup(_ => _.Send(It.IsAny<IRequest<Domain.GuestModel>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(data);
        var guest = new GuestController(_mediator.Object);

        //Act
        var result = await guest.GetGuestById(data.Id);

        //Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult).StatusCode.Should().Be(200);
    }

    [Fact]
    public async void AddGuests_ShouldReturn200Status()
    {
        //Arrange
        var fixture = new Fixture();
        var data = fixture.Create<AddGuestCommand>();
        ResponseModel responseModel = new ResponseModel();
        responseModel.GuestId = Guid.NewGuid();
        responseModel.Status = Status.Success.ToString();
        var _mediator = new Mock<IMediator>();
        _mediator.Setup(_ => _.Send(data, It.IsAny<CancellationToken>()))
            .ReturnsAsync(responseModel);
        var guest = new GuestController(_mediator.Object);

        //Act
        var result = await guest.AddGuest(data);

        //Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult).StatusCode.Should().Be(200);
    }

    [Fact]
    public async void AddPhones_ShouldReturn200Status()
    {
        //Arrange
        var fixture = new Fixture();
        var data = fixture.Create<AddPhoneCommand>();
        ResponseModel responseModel = new ResponseModel();
        responseModel.GuestId = Guid.NewGuid();
        responseModel.Status = Status.Success.ToString();
        var _mediator = new Mock<IMediator>();
        _mediator.Setup(_ => _.Send(data, It.IsAny<CancellationToken>()))
            .ReturnsAsync(responseModel);
        var guest = new GuestController(_mediator.Object);

        //Act
        var result = await guest.AddPhone(data);

        //Assert
        result.GetType().Should().Be(typeof(OkObjectResult));
        (result as OkObjectResult).StatusCode.Should().Be(200);
    }

    //private static IEnumerable<Domain.Guest> GetGuests()
    //{
    //    return new List<Domain.Guest> {
    //    new Domain.Guest { Id = Guid.NewGuid(), FirstName = "test", Rate = 100 }
    //    };
    //}
}
