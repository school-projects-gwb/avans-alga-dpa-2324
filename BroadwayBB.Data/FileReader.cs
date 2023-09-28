using BroadwayBB.Data.Factories;
using BroadwayBB.Data.Factories.Interfaces;
using BroadwayBB.Data.Strategies.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
