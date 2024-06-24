using Carglass.TechnicalAssessment.Backend.Entities;

namespace Carglass.TechnicalAssessment.Backend.DL.Repositories.InMemory;

internal class ProductIMRepository : BaseIMRepository<Product>
{
    public ProductIMRepository()
    {
        _collection = new HashSet<Product>()
        {
            new Product()
            {
                Id = 1111111,
                ProductName = "Cristal ventanilla delantera",
                ProductType = 25,
                NumTerminal = 933933933,
                SoldAt = DateTime.Parse("2019-01-09 14:26:17")
            }
        };
    }
}
