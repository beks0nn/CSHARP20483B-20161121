using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternSample.Core.DomainModel
{
    public class Animal : PersistentEntity
    {
        public int Age { get; set; }

        public string Name{ get; set; }

        public bool Dangerous { get; set; }

        public int DangerScale { get; set; }
    }
}
