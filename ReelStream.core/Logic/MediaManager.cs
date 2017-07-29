using ReelStream.core.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.core.Logic
{
    /// <summary>
    /// This class is an abstration of all media file actions. It wraps exe tools like ffmpeg for ease of future conversion. 
    /// The Executables can be found in the "Executables" Driectory at the base of the core. 
    /// </summary>
    public class MediaManager : IMediaManager
    {
        //Working Directory will be ReelStream.api
        string exeDirectory = @"..\ReelStream.core\Executables";

        /// <summary>
        /// Runs the ffmpeg_video-converson.bat.bat file that converts the orignal format to the desired, then deletes the original
        /// </summary>
        /// <param name="newVideo">The video format that we will be creating</param>
        /// <returns></returns>
        public void VideoConversion(string originalPath, string newPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = Path.Combine(exeDirectory, "ffmpeg.exe");
            startInfo.Arguments = $"-i {originalPath} {newPath}";

            try
            {
                using (Process exe = Process.Start(startInfo))
                {
                    exe.WaitForExit();
                }
                File.Delete(originalPath);
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Retrives the duration of the provided video in miliseconds
        /// </summary>
        /// <param name="videoPath">The path to the video</param>
        /// <returns>An unsigned long containing the duration in miliseconds</returns>
        public long GetVideoDuration(string videoPath)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.FileName = Path.Combine(exeDirectory, "MediaInfo.exe");
            startInfo.Arguments = $"--Inform=\"Video;%Duration%\" {videoPath}";
            long duration;

            try
            {
                using (Process exe = Process.Start(startInfo))
                {
                    duration = Convert.ToInt64(exe.StandardOutput.ReadToEnd());
                    exe.WaitForExit();
                }
                return duration;
            }
            catch
            {
                throw;
            }
            
        }
    }
}
