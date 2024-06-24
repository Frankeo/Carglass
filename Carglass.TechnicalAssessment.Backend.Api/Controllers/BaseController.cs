using Carglass.TechnicalAssessment.Backend.BL;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Carglass.TechnicalAssessment.Backend.Api.Controllers;

[ApiController]
public abstract class BaseController<Dto> : ControllerBase
    where Dto : IDto
{
    protected readonly ICrudAppService<Dto> _appService;

    public BaseController(ICrudAppService<Dto> appService)
    {
        this._appService = appService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_appService.GetAll());
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetById(int id)
    {
        return Ok(_appService.GetById(id));
    }

    [HttpPost]
    public IActionResult Create([FromBody] Dto dto)
    {
        _appService.Create(dto);
        return Ok();
    }

    [HttpPut]
    public IActionResult Update([FromBody] Dto dto)
    {
        _appService.Update(dto);
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete([FromBody] Dto dto)
    {
        _appService.Delete(dto);
        return Ok();
    }
}