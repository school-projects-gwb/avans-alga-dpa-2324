using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BroadwayBB.Data.Structs
{
    public struct FileData
    {
        public string FileName;
        public string Extension;
        public string Data;

        public FileData(string fileName, string extension, string data)
        {
            FileName = fileName;
            Extension = extension;
            Data = data;
        }
    }
}
