using ReelStream.api.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ReelStream.api.Logic
{
    public class VideoStream
    {
        private FileStream _stream;

        public VideoStream(VideoFile file)
        {
            var filename = $"wwwroot/video/{file.Folder}/{file.FileName}.{file.FileExtension}";
            _stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
            
        }

        public Stream GetStream()
        {
            return _stream;
        }
        
    }
}
