using BroadwayBB.Data.Structs;

namespace BroadwayBB.Data.Strategies.Interfaces;

public interface IFileReaderStrategy
{
    DTO ReadFile(FileData file);
}