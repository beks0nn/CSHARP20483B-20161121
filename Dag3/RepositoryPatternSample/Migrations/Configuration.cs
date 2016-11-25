namespace RepositoryPatternSample.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<RepositoryPatternSample.Persistence.ZooContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(RepositoryPatternSample.Persistence.ZooContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Animals.AddOrUpdate(p => p.Name,
                new Core.DomainModel.Animal { Name = "Tiger", Age = 23, DangerScale = 10, Dangerous = true },
                new Core.DomainModel.Animal { Name = "Elephant", Age = 90, DangerScale = 5, Dangerous = false },
                new Core.DomainModel.Animal { Name = "Mouse", Age = 1, DangerScale = 0, Dangerous = false},
                new Core.DomainModel.Animal { Name = "Badger", Age = 12, DangerScale = 4, Dangerous = false},
                new Core.DomainModel.Animal { Name = "Hippo", Age = 56, DangerScale = 9, Dangerous = true },
                new Core.DomainModel.Animal { Name = "Black Mamba", Age = 11, DangerScale = 10, Dangerous = true },
                new Core.DomainModel.Animal { Name = "Mosquito", Age = 3, DangerScale = 10, Dangerous = true },
                new Core.DomainModel.Animal { Name = "Swan", Age = 8, DangerScale = 3, Dangerous = false },
                new Core.DomainModel.Animal { Name = "Horse", Age = 4, DangerScale = 3, Dangerous = false},
                new Core.DomainModel.Animal { Name = "Shark", Age = 35, DangerScale = 8, Dangerous = true });
        }
    }
}
