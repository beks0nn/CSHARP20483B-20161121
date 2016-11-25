using RepositoryPatternSample.Core.DomainModel;
using RepositoryPatternSample.Persistence.Configuration;
using System.Data.Entity;

namespace RepositoryPatternSample.Persistence
{
    public class ZooContext : DbContext
    {
        public ZooContext()
            : base("name=ZooContext")
        {
            this.Configuration.LazyLoadingEnabled = false;            
        }

        /// <summary>
        /// Register our entities as DbSet, in this case just animals...
        /// </summary>
        public virtual DbSet<Animal> Animals{ get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AnimalConfiguration());
        }
    }
}
