namespace RepositoryPatternSample.Core.DomainModel
{
    public abstract class PersistentEntity : IPersistentEntity
    {
        public int Id { get; set; }
    }
}
