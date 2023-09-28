using BroadwayBB.Data.Strategies.Interfaces;

namespace BroadwayBB.Data.Factories.Interfaces;

public interface IFileLoaderFactory
{
    IFileLoaderStrategy? GetFileLoader(Uri path);
}