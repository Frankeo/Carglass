using AutoMapper;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.BL;

internal class ClientAppService : BaseAppService<Client, ClientDto>
{
    public ClientAppService(ICrudRepository<Client> repository, IMapper mapper, IValidator<ClientDto> validator) : base(repository, mapper, validator)
    {
    }

    protected override string GetItemExistanceMessage() => "No existe ningún cliente con este Id.";

    protected override void ValidateFieldsToInsert(ClientDto item)
    {
        if (_repository.GetAll().Any(x => x.DocNum == item.DocNum && x.Id != item.Id))
        {
            throw new Exception("Ya existe un cliente con este DocNum.");
        }
    }
}