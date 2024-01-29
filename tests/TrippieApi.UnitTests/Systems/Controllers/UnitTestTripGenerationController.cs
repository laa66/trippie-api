using System.Security.Claims;
using Controllers;
using Dtos;
using Entities;
using Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Services;
using TrippieApi.UnitTests.Fixtures;
using Xunit;

namespace TrippieApi.UnitTests.Systems;

public class UnitTestTripGenerationController
{
    [Fact]
    public async void GetTrip_Should_ResponseStatus200()
    {
        var mockTripGenerationService = new Mock<ITripGenerationService>();
        var mockTripService = new Mock<ITripService>();
        var mockUserService = new Mock<IUserService>();

        var points = TripPointsFixtures.GetTestTripPoints();

        mockTripGenerationService.Setup(s => s.CreateTrip(
            It.IsAny<int>(), 
            It.IsAny<double>(),
            It.IsAny<double>(),
            It.IsAny<string[]>()
        )).ReturnsAsync(points);

        var user = UserFixtures.GetUser();
        var trip = new Trip
        {
            TripId = 1,
            TripPoints = points.Select(p => new Entities.TripPoint
            {
                TripPointId = 0,
                TripId = 1,
                Longitude = p.Coordinates[0],
                Latitude = p.Coordinates[1],
                Name = p.Name,
                FullAddress = p.FullAddress
            }).ToList()
        };
        mockUserService.Setup(u => u.GetUser(It.IsAny<string>()))
            .Returns(user)
            .Verifiable();

        user.UserTrips.Add(trip);
        mockTripService.Setup(t => t.Save(user, It.Is<Trip>(trip => 
            trip.TripId == 0 
            && trip.User.Equals(user)
            && points.Count == trip.TripPoints.Count 
        ))).Returns(user)
        .Verifiable();


        var contextUser = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.NameIdentifier, "name")]));
        var sut = new TripGenerationController(mockTripGenerationService.Object, mockTripService.Object, mockUserService.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
                {
                    User = contextUser
                }
            }
        };

        var result = await sut.GetTrip(10, 50.4, 50.3, new PoisDTO(["poi", "poi1"]));
        
        var okResult = result as OkObjectResult;
        var tripPoints = okResult?.Value as IEnumerable<Models.TripPoint>;

        Assert.Equal(points.Count, tripPoints?.Count());
        Mock.VerifyAll(mockUserService, mockTripService);
    }

    [Fact]
    public async Task GetTrip_Should_Response404()
    {
        var mockTripGenerationService = new Mock<ITripGenerationService>();
        var mockTripService = new Mock<ITripService>();
        var mockUserService = new Mock<IUserService>();

        var points = TripPointsFixtures.GetTestTripPoints();

        mockTripGenerationService.Setup(s => s.CreateTrip(
            It.IsAny<int>(), 
            It.IsAny<double>(),
            It.IsAny<double>(),
            It.IsAny<string[]>()
        )).ReturnsAsync(points);

        var sut = new TripGenerationController(mockTripGenerationService.Object, mockTripService.Object, mockUserService.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            }
        };
        
        await Assert.ThrowsAsync<UserNotFoundException>(() => sut.GetTrip(10, 5.4, 4.3, new PoisDTO(["POI"])));
    }
}