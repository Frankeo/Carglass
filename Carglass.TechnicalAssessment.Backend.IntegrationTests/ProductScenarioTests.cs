using Carglass.TechnicalAssessment.Backend.Api.Controllers;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Numerics;
using System.Text;

namespace Carglass.TechnicalAssessment.Backend.IntegrationTests
{
    public class ProductScenarioTests : BaseScenario
    {
        public ProductScenarioTests(WebApplicationFactory<ProductsController> webApplicationFactory) : base(webApplicationFactory)
        {
        }

        private ProductDto product = new ProductDto
        {
            Id = 1111111,
            ProductName = "Cristal ventanilla delantera",
            ProductType = 25,
            NumTerminal = 933933933,
            SoldAt = DateTime.Parse("2019-01-09 14:26:17")
        };

        [Fact]
        public async Task Should_Get_All_Products()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            //Act
            var response = await httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>("products/GetAll");
            //Assert
            response.Should().NotBeNull();
        }

        [Fact]
        public async Task Should_Get_One_Product()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var product4 = new ProductDto
            {
                Id = 111120,
                ProductName = "Cristal ventanilla delantera bla bla",
                ProductType = 25,
                NumTerminal = 933933933,
                SoldAt = DateTime.Parse("2019-01-09 14:26:17")
            };
            //Act
            await httpClient.PostAsJsonAsync("products/Create", product4);
            var response = await httpClient.GetFromJsonAsync<ProductDto>($"products/GetById/{product4.Id}");
            //Assert
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(product4);
        }

        [Fact]
        public async Task Should_Be_Able_To_Delete_Product()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            //Act
            var request = new HttpRequestMessage(HttpMethod.Delete, "products/Delete");
            request.Content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Get_Error_To_Delete_Inexistence_Product()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var product = new ProductDto
            {
                Id = 3
            };

            //Act
            var request = new HttpRequestMessage(HttpMethod.Delete, "products/Delete");
            request.Content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Should_Be_Able_To_Create_Product()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var product = new ProductDto
            {
                Id = 1111112,
                ProductName = "Cristal ventanilla delantera 4",
                ProductType = 25,
                NumTerminal = 933933933,
                SoldAt = DateTime.Parse("2019-01-09 14:26:17")
            };

            //Act
            var response = await httpClient.PostAsJsonAsync("products/Create", product);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task Should_Be_Able_To_Update_Product()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var product2 = new ProductDto
            {
                Id = 1111112,
                ProductName = "pepe 20",
                ProductType = 25,
                NumTerminal = 933933933,
                SoldAt = DateTime.Parse("2019-01-09 14:26:17")
            };
            //Act
            var response = await httpClient.PutAsJsonAsync("products/Update", product2);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }



        [Fact]
        public async Task Should_Be_Get_An_Error_With_Same_ProductName_Creation()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var product3 = new ProductDto
            {
                Id = 111113,
                ProductName = "Cristal ventanilla delantera",
                ProductType = 25,
                NumTerminal = 933933933,
                SoldAt = DateTime.Parse("2019-01-09 14:26:17")
            };

            //Act
            await httpClient.PostAsJsonAsync("products/Create", product3);
            product3.Id = 123124;
            var response = await httpClient.PostAsJsonAsync("products/Create", product3);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }


        [Fact]
        public async Task Should_Get_An_Error_Updating_Inexistent_Product()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var product = new ProductDto
            {
                Id = 111119,
                ProductName = "Cristal ventanilla delantera",
                ProductType = 25,
                NumTerminal = 933933933,
                SoldAt = DateTime.Parse("2019-01-09 14:26:17")
            };

            //Act
            var response = await httpClient.PutAsJsonAsync("products/Update", product);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}