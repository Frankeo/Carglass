using Carglass.TechnicalAssessment.Backend.BL;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Carglass.TechnicalAssessment.Backend.Api.Controllers;

[Route("products/[action]")]
public class ProductsController : BaseController<ProductDto>
{
    public ProductsController(ICrudAppService<ProductDto> appService) : base(appService)
    {
    }
}