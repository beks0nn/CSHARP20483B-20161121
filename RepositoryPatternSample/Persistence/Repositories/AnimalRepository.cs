using RepositoryPatternSample.Core.DomainModel;
using RepositoryPatternSample.Core.Repositories;

namespace RepositoryPatternSample.Persistence.Repositories
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        public AnimalRepository(ZooContext context) : base(context)
        {
        }
    }
}
