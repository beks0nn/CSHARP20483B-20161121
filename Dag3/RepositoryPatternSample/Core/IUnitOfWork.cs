using RepositoryPatternSample.Core.Repositories;
using System;

namespace RepositoryPatternSample.Core
{
    public interface IUnitOfWork : IDisposable
    {
        //Register custom repositories here!!!
        IAnimalRepository Animals { get; }

        int Complete();
    }
}
