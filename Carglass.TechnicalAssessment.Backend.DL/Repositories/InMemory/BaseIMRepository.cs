using Carglass.TechnicalAssessment.Backend.Entities;

namespace Carglass.TechnicalAssessment.Backend.DL.Repositories.InMemory;

public abstract class BaseIMRepository<Entity> : ICrudRepository<Entity>
    where Entity : IEntity, new()
{
    protected ICollection<Entity> _collection;

    public IEnumerable<Entity> GetAll()
    {
        return _collection.ToList();
    }

    public Entity? GetById(params object[] keyValues)
    {
        return _collection.SingleOrDefault(x => x.Id.Equals(keyValues[0]));
    }

    public void Create(Entity item)
    {
        _collection.Add(item);
    }

    public void Update(Entity item)
    {
        Delete(item);
        Create(item);
    }

    public void Delete(Entity item)
    {
        var toDeleteItem = _collection.Single(x => x.Id.Equals(item.Id));
        _collection.Remove(toDeleteItem);
    }
}
