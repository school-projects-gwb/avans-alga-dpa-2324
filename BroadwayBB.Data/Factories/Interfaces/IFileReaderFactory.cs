using BroadwayBB.Data.Strategies.Interfaces;
using BroadwayBB.Data.Structs;

namespace BroadwayBB.Data.Factories.Interfaces;

public interface IFileReaderFactory
{
    IFileReaderStrategy? GetFileReader(FileData file);
}