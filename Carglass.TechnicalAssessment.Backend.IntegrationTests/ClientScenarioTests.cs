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
    public class ClientScenarioTests : BaseScenario
    {
        public ClientScenarioTests(WebApplicationFactory<ProductsController> webApplicationFactory) : base(webApplicationFactory)
        {
        }

        [Fact]
        public async Task Should_Get_All_Clients()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var client = new ClientDto
            {
                Id = 1,
                DocType = "nif",
                DocNum = "11223344E",
                Email = "eromani@sample.com",
                GivenName = "Enriqueta",
                FamilyName1 = "Romani",
                Phone = "668668668"
            };
            //Act
            var response = await httpClient.GetFromJsonAsync<IEnumerable<ClientDto>>("clients/GetAll");
            //Assert
            response.Should().NotBeNull();
            response.Should().ContainSingle();
            response.Should().ContainEquivalentOf(client);
        }

        [Fact]
        public async Task Should_Get_One_Client()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var id = 1;
            var client = new ClientDto
            {
                Id = id,
                DocType = "nif",
                DocNum = "11223344E",
                Email = "eromani@sample.com",
                GivenName = "Enriqueta",
                FamilyName1 = "Romani",
                Phone = "668668668"
            };
            //Act
            var response = await httpClient.GetFromJsonAsync<ClientDto>($"clients/GetById/{id}");
            //Assert
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(client);
        }

        [Fact]
        public async Task Should_Be_Able_To_Delete_Client()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var client = new ClientDto
            {
                Id = 1
            };

            //Act
            var request = new HttpRequestMessage(HttpMethod.Delete, "clients/Delete");
            request.Content = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Should_Get_Error_To_Delete_Inexistence_Client()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var client = new ClientDto
            {
                Id = 3
            };

            //Act
            var request = new HttpRequestMessage(HttpMethod.Delete, "clients/Delete");
            request.Content = new StringContent(JsonConvert.SerializeObject(client), Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(request);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

        [Fact]
        public async Task Should_Be_Able_To_Create_Client()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var client = new ClientDto
            {
                Id = 2,
                DocType = "nif",
                DocNum = "11223344R",
                Email = "eromani@sample.com",
                GivenName = "Enriqueta",
                FamilyName1 = "Romani",
                Phone = "668668668"
            };

            //Act
            var response = await httpClient.PostAsJsonAsync("clients/Create", client);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }


        [Fact]
        public async Task Should_Be_Able_To_Update_Client()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var client = new ClientDto
            {
                Id = 2,
                DocType = "nif",
                DocNum = "11223344J",
                Email = "eromani@sample.com",
                GivenName = "Enriqueta",
                FamilyName1 = "Romani",
                Phone = "668668668"
            };

            //Act
            var response = await httpClient.PutAsJsonAsync("clients/Update", client);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }



        [Fact]
        public async Task Should_Be_Get_An_Error_With_Wrong_DocNum_Creation()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var client = new ClientDto
            {
                Id = 10,
                DocType = "nif",
                DocNum = "1122dasdJ",
                Email = "eromani@sample.com",
                GivenName = "Enriqueta",
                FamilyName1 = "Romani",
                Phone = "668668668"
            };

            //Act
            var response = await httpClient.PostAsJsonAsync("clients/Create", client);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }


        [Fact]
        public async Task Should_Get_An_Error_Updating_Inexistent_Client()
        {
            //Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var client = new ClientDto
            {
                Id = 12,
                DocType = "nif",
                DocNum = "11223344J",
                Email = "eromani@sample.com",
                GivenName = "Enriqueta",
                FamilyName1 = "Romani",
                Phone = "668668668"
            };

            //Act
            var response = await httpClient.PutAsJsonAsync("clients/Update", client);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
    }
}