using BroadwayBB.Data.Structs;

namespace BroadwayBB.Data.Strategies.Interfaces;

public interface IFileReaderStrategy
{
    object ReadFile(FileData file);
}