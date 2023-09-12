using BroadwayBB.Data.Factories.Interfaces;
using BroadwayBB.Data.Strategies.Interfaces;
using BroadwayBB.Data.Structs;
using System.Drawing;
using System.Reflection.PortableExecutable;

namespace BroadwayBB.Data.Factories;

public class FileReaderFactory : IFileReaderFactory
{

    private const string DefaultReaderStrategy = "txt";
    private const string ReaderStrategyClassName = "FileReaderStrategy";
    private readonly Dictionary<string, Type> ReaderStrategies = new Dictionary<string, Type>();

    public FileReaderFactory()
    {
        var interfaceType = typeof(IFileReaderStrategy);
        var implementingTypes = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(a => a.GetTypes())
                                .Where(type => interfaceType.IsAssignableFrom(type) &&
                                                !type.IsAbstract &&
                                                !type.IsInterface);

        foreach (var type in implementingTypes)
        {
            string name = type.Name.Replace(ReaderStrategyClassName, "").ToLower();
            ReaderStrategies[name] = type;
        }
    }

    public IFileReaderStrategy? GetFileReader(FileData file)
    {
        if (!ReaderStrategies.TryGetValue(file.Extension, out Type? readerStrategy))
        {
            ReaderStrategies.TryGetValue(DefaultReaderStrategy, out Type? defaultReader);
            return (IFileReaderStrategy?)Activator.CreateInstance(defaultReader);
        }

        return (IFileReaderStrategy?)Activator.CreateInstance(readerStrategy);
    }
}