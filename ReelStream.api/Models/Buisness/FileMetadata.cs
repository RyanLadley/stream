using ReelStream.api.Logic.Interfaces;
using ReelStream.data.Models.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ReelStream.api.Logic;

namespace ReelStream.api.Models.Buisness
{
    public class FileMetadata
    {
        #region Properties
        public string Folder { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public string FileExtension { get; set; }

        /// <summary>
        /// Duration in miliseconds
        /// </summary>
        public long Duration { get; set; }

        public string FilePath
        {
            get { return Path.Combine(Folder, $"{FileName}{FileExtension}"); }
        }
        #endregion

        public FileMetadata(string file)
        {
            Folder = Path.GetDirectoryName(file);
            FileName = Path.GetFileNameWithoutExtension(file); 
            FileSize = new FileInfo(file).Length; //Consider Using FileInfo for all of this
            FileExtension = Path.GetExtension(file);
        }

        public void GetDuration(IMediaManager mediaManager)
        {
            Duration = mediaManager.GetVideoDuration(FilePath);
        }
        
        public VideoFile MapToVideoFileEntity()
        {
            return new VideoFile()
            {
                FileName = this.FileName,
                Folder = this.Folder.Replace("wwwroot\\", "").Replace("\\", "/"),
                FileExtension = this.FileExtension.TrimStart('.'),
                Duration = TimeSpan.FromMilliseconds(this.Duration)
            };
        }
        

        /// <summary>
        /// This function maps the metadat to an already existing VideoFile Entity
        /// This is used when the db context is already tracking the object, but we want to replace the meta data within
        /// </summary>
        /// <param name="videoFile"></param>
        /// <returns></returns>
        public VideoFile MapToExistingVideoFileEntity(VideoFile videoFile)
        {
            videoFile.FileName = this.FileName;
            videoFile.Folder = this.Folder.Replace("wwwroot\\", "").Replace("\\", "/");
            videoFile.FileExtension = this.FileExtension.TrimStart('.');
            videoFile.Duration = TimeSpan.FromMilliseconds(this.Duration);

            return videoFile;
        }

        public static string VerifyFileUniqueness(string filename)
        {
            var i = 0;
            var newFilename = filename;
            while (File.Exists(newFilename))
            {
                var folder = Path.GetDirectoryName(filename);
                var fileName = Path.GetFileNameWithoutExtension(filename) + $"({i})";
                var fileExtension = Path.GetExtension(filename);
                newFilename = Path.Combine(folder, fileName + fileExtension);
                i++;
            }

            return newFilename;
        }

    }
}
