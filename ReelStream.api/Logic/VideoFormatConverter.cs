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

        public VideoFormatConverter(FileMetadata originalFile)
        {
            _originalVideo = originalFile;
        }

        /// <summary>
        /// Runs the ffmpeg.bat file that converts the orignal format to the desired, then deletes the original
        /// </summary>
        /// <param name="newVideo">The video format that we will be creating</param>
        /// <returns></returns>
        public FileMetadata ConvertTo(string newExtension)
        {
            string originalPath = Path.Combine(_originalVideo.Folder, $"{_originalVideo.FileName}{_originalVideo.FileExtension}");
            string newPath = Path.Combine(_originalVideo.Folder,$"{_originalVideo.FileName}.{newExtension}");

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "ffmpeg.bat";
            startInfo.Arguments = $"{originalPath} {newPath}"; 

            try
            {
                _executeCommand(startInfo);

                _originalVideo.FileExtension = $".{newExtension}";
                return _originalVideo;
            }
            catch
            {
                Console.Write("There Was An Error");
                return _originalVideo;
            }
           
        }

        private void _executeCommand(ProcessStartInfo startInfo)
        {
            using (Process ffmeg = Process.Start(startInfo))
            {
                ffmeg.WaitForExit();
            }
        }
    }
}
