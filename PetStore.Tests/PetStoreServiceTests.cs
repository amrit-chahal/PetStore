namespace PetStore.Tests
{
     
    using System.Net;
    using System.Net.Http;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;
    using Moq;
    using Moq.Protected;
    using PetStore.Models;
    using PetStore.Services;

    public class PetStoreServiceTests
    {
        [Fact]
        public async Task GetPetDataAsync_ReturnsDataObject()
        {
            // Arrange
            var petCategory = new PetStoreCategory { Id = 1, Name = "Dogs" };
            var tags = new List<PetStoreTag>
            {
                new PetStoreTag {Id = 1, Name ="tag1"},
                new PetStoreTag {Id = 2, Name ="tag2"},
            };
            var photoUrls = new List<string> { "url1", "url2" };
            var expectedData = new List<PetStoreModel>
            {
                new PetStoreModel { Id = 11, Category = petCategory, Name = "Good Boi", PhotoUrls = photoUrls, Status = "available", Tags = tags },
            };
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonSerializer.Serialize(expectedData))
                });

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost:8080/api/v3/pet/")
            };

            var service = new PetStoreApiClient(httpClient);

            // Act
            var result = await service.GetPetsAsync("findByStatus?status=available");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedData, result);
            //Assert.Equal(expectedData.Category, result.Cat);
        }

        [Fact]
        public async Task GetPetDataAsync_ThrowsExceptionOnBadRequest()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Content = new StringContent("")
                });

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://localhost:8080/api/v3/pet/")
            };

            var service = new PetStoreApiClient(httpClient);

            // Act & Assert
            await Assert.ThrowsAsync<HttpRequestException>(() => service.GetPetsAsync("findByStatus?status=available"));
        }
    }

}