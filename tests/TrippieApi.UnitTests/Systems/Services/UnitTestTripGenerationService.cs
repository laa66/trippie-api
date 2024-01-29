using Moq;
using Services;
using Xunit;
using Models;
using System.Collections;
using TrippieApi.UnitTests.Fixtures;
using Xunit.Abstractions;
using Exceptions;

namespace TrippieApi.UnitTests.Systems.Services;


public class UnitTestTripGenerationService(ITestOutputHelper outputHelper)
{
    private readonly ITestOutputHelper _outputHelper = outputHelper;

    [Fact]
    public async void CreateTrip_Should_ReturnOptimizedTrip()
    {
        var mockPoiApiService = new Mock<IPoiApiService>();
        var tripPoints = TripPointsFixtures.GetTestTripPoints();
        mockPoiApiService.Setup(s => s.GetPoiCollection(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double>()))
            .ReturnsAsync(tripPoints); 
        var sut = new TripGenerationService(mockPoiApiService.Object);
        
        var result = await sut.CreateTrip(4, 1.0, 1.0, ["category"]);

        Assert.Collection(result, 
            e => 
            {
                Assert.Equal("first", e.Name);
            },
            e => {
                Assert.Equal("second", e.Name);
            }, 
            e => {
                Assert.Equal("third", e.Name);
            }, 
            e => {
                Assert.Equal("fourth", e.Name);
            });
    }

    [Fact]
    public async void GetSingleCategoryTrip_Should_ReturnPoiCollection()
    {
        var mockPoiApiService = new Mock<IPoiApiService>();
        var tripPoints = TripPointsFixtures.GetTestTripPoints();
        mockPoiApiService.Setup(s => s.GetPoiCollection(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double>()))
            .ReturnsAsync(tripPoints);    
        var sut = new TripGenerationService(mockPoiApiService.Object);

        var result = await sut.GetSingleCategoryTrip(40.5, 32.2, "category");

        Assert.Equal(tripPoints, result);

    }

    [Fact]
    public async void GetSingleCategoryTrip_Should_ThrowPoiApiException()
    {
        var mockPoiApiService = new Mock<IPoiApiService>();
        mockPoiApiService.Setup(s => s.GetPoiCollection(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<double>()))
            .ThrowsAsync(new HttpRequestException()); 
        var sut = new TripGenerationService(mockPoiApiService.Object);

        await Assert.ThrowsAsync<PoiApiException>(() => sut.GetSingleCategoryTrip(50.3, 21.2, "category"));
    }

    [Fact]
    public async void OptimizeTrip_Should_ReturnOptimizedTrip()
    {
        var mockPoiApiService = new Mock<IPoiApiService>();
        var sut = new TripGenerationService(mockPoiApiService.Object);
        var tripPoints = TripPointsFixtures.GetTestTripPoints();

        var result = await sut.OptimizeTrip(1.0, 1.5, tripPoints, 4);
        
        Assert.Collection(result, 
            e => 
            {
                Assert.Equal("first", e.Name);
            },
            e => {
                Assert.Equal("second", e.Name);
            }, 
            e => {
                Assert.Equal("third", e.Name);
            }, 
            e => {
                Assert.Equal("fourth", e.Name);
            });

    }

    [Fact]
    public async void OptimizeTrip_Should_ReturnEmptyTrip()
    {
        var mockPoiApiService = new Mock<IPoiApiService>();
        var sut = new TripGenerationService(mockPoiApiService.Object);
        var tripPoints = Enumerable.Empty<TripPoint>();

        var result = await sut.OptimizeTrip(1.0, 1.5, tripPoints, 20);

        Assert.Empty(result);
    }

}
