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

        public void ReadFile(string path)
        {
            // var uri = new Uri(path);

            Uri[] uris = {
                new Uri(path),
                new Uri("D:/Avans/Advanced Design Patterns/Assesment Files/artists.csv"),
                new Uri("https://firebasestorage.googleapis.com/v0/b/dpa-files.appspot.com/o/grid.txt?alt=media"),
                new Uri("https://firebasestorage.googleapis.com/v0/b/dpa-files.appspot.com/o/graph.xml?alt=media"),
                new Uri("https://www.geeksforgeeks.org/quick-sort/"),
                new Uri("D:/Avans/Advanced Design Patterns/Assesment Files/")
            };

            foreach (var uri in uris)
            {
                IFileLoaderStrategy? fileLoader = _loaderFactory.GetFileLoader(uri);
                if (fileLoader == null)
                {
                    return /*null*/;
                }

                var file = fileLoader.loadFile(uri);

                IFileReaderStrategy? fileReader = _readerFactory.GetFileReader(file);
                if (fileReader == null)
                {
                    return /*null*/;
                }

                var jsonFileData = fileReader.ReadFile(file);
            }
        }
    }
}
