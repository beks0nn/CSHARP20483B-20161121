using RepositoryPatternSample.Core.DomainModel;
using System.Collections.Generic;

namespace RepositoryPatternSample.Core.Repositories
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        // Extra method specific to IAnimal should be declared here!
        IEnumerable<Animal> GetMostDangerousAnimals(int maxHits); 
    }
}
