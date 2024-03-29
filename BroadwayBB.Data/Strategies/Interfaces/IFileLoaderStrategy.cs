using BroadwayBB.Data.Structs;

namespace BroadwayBB.Data.Strategies.Interfaces;

public interface IFileLoaderStrategy
{
    FileData loadFile(Uri uri);
}