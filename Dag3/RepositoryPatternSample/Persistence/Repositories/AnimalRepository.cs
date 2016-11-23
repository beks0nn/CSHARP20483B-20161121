using RepositoryPatternSample.Core.DomainModel;
using RepositoryPatternSample.Core.Repositories;
using System.Collections.Generic;
using System.Linq;
namespace RepositoryPatternSample.Persistence.Repositories
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        public AnimalRepository(ZooContext context) : base(context)
        {
        }

        public System.Collections.Generic.IEnumerable<Animal> GetMostDangerousAnimals(int maxHits)
        {
            return this._entities.OrderByDescending(p => p.DangerScale).Take(maxHits).ToList();            
        }
    }
}
