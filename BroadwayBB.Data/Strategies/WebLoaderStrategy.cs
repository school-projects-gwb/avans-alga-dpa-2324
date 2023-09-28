using BroadwayBB.Data.Strategies.Interfaces;
using BroadwayBB.Data.Structs;
using System;
using System.Net;

namespace BroadwayBB.Data.Strategies;

public class WebLoaderStrategy : IFileLoaderStrategy
{
    public FileData loadFile(Uri uri)
    {
        using (var client = new HttpClient())
        using (var response = client.GetAsync(uri).Result)
        {
            // make sure our request was successful
            response.EnsureSuccessStatusCode();

            // read the filename from the Content-Disposition header
            var filename = response?.Content?.Headers?.ContentDisposition?.FileNameStar;

            if (filename == null)
            {
                throw new ArgumentException(uri.OriginalString);
            }

            var nameData = filename.Split(".");
                
            // read the downloaded file data
            var fileContent = response?.Content.ReadAsStringAsync().Result;

            if (fileContent == null)
            {
                fileContent = string.Empty;
            }

            return new FileData(nameData[0], nameData[1], fileContent);
        }
    }
}