using BroadwayBB.Data.Factories.Interfaces;
using BroadwayBB.Data.Strategies;
using BroadwayBB.Data.Strategies.Interfaces;
using System.Reflection.PortableExecutable;

namespace BroadwayBB.Data.Factories;

public class FileLoaderFactory : IFileLoaderFactory
{
    public IFileLoaderStrategy? GetFileLoader(Uri uri)
    {
        if (uri.IsFile)
        {
            return new DiskLoaderStrategy();
        }
        else if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
        {
            return new WebLoaderStrategy();
        }

        return null;
    }

}