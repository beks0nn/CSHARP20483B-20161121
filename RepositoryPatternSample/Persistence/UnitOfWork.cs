using RepositoryPatternSample.Core;
using RepositoryPatternSample.Core.Repositories;
using RepositoryPatternSample.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternSample.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ZooContext _context;

        public UnitOfWork(ZooContext context)
        {
            _context = context;
            Animals = new AnimalRepository(_context);
        }

        public IAnimalRepository Animals{ get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
