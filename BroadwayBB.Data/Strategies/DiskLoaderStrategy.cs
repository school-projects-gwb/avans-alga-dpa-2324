using BroadwayBB.Data.Strategies.Interfaces;
using BroadwayBB.Data.Structs;

namespace BroadwayBB.Data.Strategies;

public class DiskLoaderStrategy : IFileLoaderStrategy
{
    public FileData loadFile(Uri uri)
    {
        string filename = Path.GetFileName(uri.LocalPath);
        var nameData = filename.Split(".");
        string text = File.ReadAllText(uri.LocalPath);

        return new FileData(nameData[0], nameData[1], text);
    }
}