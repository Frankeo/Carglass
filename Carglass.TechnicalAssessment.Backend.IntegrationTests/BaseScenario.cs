using Carglass.TechnicalAssessment.Backend.Api.Controllers;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Carglass.TechnicalAssessment.Backend.IntegrationTests
{
    public abstract class BaseScenario : IClassFixture<WebApplicationFactory<ProductsController>>
    {
        protected readonly WebApplicationFactory<ProductsController> _webApplicationFactory;

        public BaseScenario(WebApplicationFactory<ProductsController> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }
    }
}
