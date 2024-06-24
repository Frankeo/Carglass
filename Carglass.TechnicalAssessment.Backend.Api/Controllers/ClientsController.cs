using Carglass.TechnicalAssessment.Backend.BL;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Carglass.TechnicalAssessment.Backend.Api.Controllers;

[Route("clients/[action]")]
public class ClientsController : BaseController<ClientDto>
{
    public ClientsController(ICrudAppService<ClientDto> appService) : base(appService)
    {
    }
}