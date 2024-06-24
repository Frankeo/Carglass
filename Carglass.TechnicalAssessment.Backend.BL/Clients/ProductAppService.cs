using AutoMapper;
using Carglass.TechnicalAssessment.Backend.DL.Repositories;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Entities;
using FluentValidation;

namespace Carglass.TechnicalAssessment.Backend.BL;

internal class ProductAppService : BaseAppService<Product, ProductDto>
{
    public ProductAppService(ICrudRepository<Product> repository, IMapper mapper, IValidator<ProductDto> validator) : base(repository, mapper, validator)
    {
    }

    protected override void ValidateFieldsToInsert(ProductDto item)
    {
        if (_repository.GetAll().Any(x => x.ProductName == item.ProductName && x.Id != item.Id))
        {
            throw new Exception("Ya existe un producto con este ProductName.");
        }
    }

    protected override string GetItemExistanceMessage() => "No existe ningún producto con este Id.";
}