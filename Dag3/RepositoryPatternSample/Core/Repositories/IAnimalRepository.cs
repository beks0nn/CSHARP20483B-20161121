using RepositoryPatternSample.Core.DomainModel;

namespace RepositoryPatternSample.Core.Repositories
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        // Extra method specific to IAnimal should be declared here!
    }
}
