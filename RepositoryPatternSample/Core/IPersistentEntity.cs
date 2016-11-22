namespace RepositoryPatternSample.Core
{
    /// <summary>
    /// All entities in our domain model should implement this interface
    /// </summary>
    public interface IPersistentEntity
    {
        int Id { get; set; }
    }
}
