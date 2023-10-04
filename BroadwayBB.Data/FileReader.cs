using BroadwayBB.Data.Factories;
using BroadwayBB.Data.Factories.Interfaces;
using BroadwayBB.Data.Strategies.Interfaces;

namespace BroadwayBB.Data
{
    public class FileReader
    {
        private IFileLoaderFactory _loaderFactory;
        private IFileReaderFactory _readerFactory;

        public FileReader()
        {
            _loaderFactory = new FileLoaderFactory();
            _readerFactory = new FileReaderFactory();
        }

        public DTO ReadFile(string path)
        {
            var uri = new Uri(path);

            IFileLoaderStrategy? fileLoader = _loaderFactory.GetFileLoader(uri);
            if (fileLoader == null)
            {
                throw new ArgumentException("File not found");
            }

            var file = fileLoader.loadFile(uri);

            IFileReaderStrategy? fileReader = _readerFactory.GetFileReader(file);
            if (fileReader == null)
            {
                throw new ArgumentException($"File type {file.Extension} not supported");
            }

            return fileReader.ReadFile(file);
        }
    }
}
