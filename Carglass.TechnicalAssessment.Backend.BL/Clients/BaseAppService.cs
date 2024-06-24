using AutoMapper;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.BL;

internal abstract class BaseAppService<Entity, Dto> : ICrudAppService<Dto>
    where Entity : IEntity, new()
    where Dto : IDto
{
    protected readonly ICrudRepository<Entity> _repository;
    protected readonly IMapper _mapper;
    protected readonly IValidator<Dto> _validator;

    public BaseAppService(ICrudRepository<Entity> repository, IMapper mapper, IValidator<Dto> validator)
    { 
        this._repository = repository;
        this._mapper = mapper;
        this._validator = validator;
    }

    protected abstract string GetItemExistanceMessage();

    protected abstract void ValidateFieldsToInsert(Dto item);

    private void CheckItemExistance(int id)
    {
        if (_repository.GetById(id) == null)
        {
            throw new Exception(GetItemExistanceMessage());
        }
    }

    private void ValidateDto(Dto item)
    {
        var validationResult = _validator.Validate(item);
        if (validationResult.Errors.Any())
        {
            string toShowErrors = string.Join("; ", validationResult.Errors.Select(s => s.ErrorMessage));
            throw new Exception($"No cumple los requisitos de validación. Errores: '{toShowErrors}'");
        }
    }

    public IEnumerable<Dto> GetAll()
    {
        var entities = _repository.GetAll();
        return _mapper.Map<IEnumerable<Dto>>(entities);
    }

    public Dto GetById(params object[] keyValues)
    {
        var entity = _repository.GetById(keyValues);
        return _mapper.Map<Dto>(entity);
    }

    public void Create(Dto item)
    {
        if (_repository.GetById(item.Id) != null)
        {
            throw new Exception("Ya existe un elemento con este Id");
        }

        ValidateFieldsToInsert(item);
        ValidateDto(item);
        var entity = _mapper.Map<Entity>(item);
        _repository.Create(entity);
    }

    public void Update(Dto item)
    {
        CheckItemExistance(item.Id);
        ValidateFieldsToInsert(item);
        ValidateDto(item);
        var entity = _mapper.Map<Entity>(item);
        _repository.Update(entity);
    }

    public void Delete(Dto item)
    {
        CheckItemExistance(item.Id);
        var entity = _mapper.Map<Entity>(item);
        _repository.Delete(entity);
    }
}