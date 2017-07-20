using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.api.Models.Entities
{
    /// <summary>
    /// This class is the represintaion of the file system Video. 
    /// It contains all meta pertaining to the file
    /// </summary>
    public class VideoFile 
    {
        public long VideoFileId { get; set; }
        public string Folder { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public TimeSpan? Duration { get; set; }

        public Movie Movie { get; set; }
    }
}
