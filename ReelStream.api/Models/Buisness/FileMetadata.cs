using ReelStream.api.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.api.Models.Buisness
{
    public class FileMetadata
    {
        public string Folder { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FileExtension { get; set; }

        public FileMetadata(string file)
        {
            Folder = Path.GetDirectoryName(file);
            FileName = Path.GetFileNameWithoutExtension(file); 
            FileSize = new FileInfo(file).Length; //Consider Using FileInfo for all of this
            FileExtension = Path.GetExtension(file);
        }

        public VideoFile MapToVideoFileEntity()
        {
            return new VideoFile()
            {
                FileName = this.FileName,
                Folder = this.Folder.Replace("wwwroot\\", "").Replace("\\", "/"),
                FileExtension = this.FileExtension
            };
        }
    }
}
