using ReelStream.api.Logic.Interfaces;
using ReelStream.api.Models.Buisness;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.api.Logic
{
    public class VideoFormatConverter
    {
        private FileMetadata _originalVideo;
        private IMediaManager _mediaManager;

        public VideoFormatConverter(FileMetadata originalFile, IMediaManager mediaManager)
        {
            _originalVideo = originalFile;
            _mediaManager = mediaManager;
        }

        /// <summary>
        /// Runs the ffmpeg.bat file that converts the orignal format to the desired, then deletes the original
        /// </summary>
        /// <param name="newVideo">The video format that we will be creating</param>
        /// <returns></returns>
        public FileMetadata ConvertTo(string newExtension)
        {
            string originalPath = Path.Combine(_originalVideo.Folder, $"{_originalVideo.FileName}{_originalVideo.FileExtension}");
            string newPath = FileMetadata.VerifyFileUniqueness(Path.Combine(_originalVideo.Folder,$"{_originalVideo.FileName}.{newExtension}"));
            

            try
            {
                _mediaManager.VideoConversion(originalPath, newPath);

                return new FileMetadata(newPath);
            }
            catch
            {
                Console.Write("There Was An Error");
                return _originalVideo;
            }
           
        }
        
    }
}
