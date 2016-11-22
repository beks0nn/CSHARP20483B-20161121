using RepositoryPatternSample.Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternSample.Persistence.Configuration
{
    public class AnimalConfiguration : EntityTypeConfiguration<Animal>
    {
        public AnimalConfiguration()
        {
            Property(c => c.Age)
                .IsRequired();

            Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}
