﻿using ReelStream.core.Models.Buisness;
using ReelStream.core.Models.DataTransfer.Form;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReelStream.core.Logic
{

    /// <summary>
    /// File Upload takes a file stream (chunks) from flow.js and creates a file on disk
    /// It does this by saving all chunks to disk until all have been recieved. 
    /// When Thay have been recieved, they are compiled together into the complete file. 
    /// </summary>
    public class FileUpload
    {
        int NumberOfRetries = 3;
        int DelayOnRetry = 1000;
        string root = "wwwroot";


        public async Task AddChunkFile(long userId, byte[] chunk, FlowUploadForm flow)
        {
            string filename = _createChunkFile(userId, flow.flowChunkNumber, flow.flowIdentifier);
            for (int i = 1; i <= NumberOfRetries; ++i)
            {
                try
                {
                    using (FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Write))
                    {
                        await fs.WriteAsync(chunk, 0, chunk.Length);
                        fs.Flush();
                        fs.Dispose();
                    }
                    break; 
                }
                catch (IOException e)
                {
                    if (i == NumberOfRetries)
                        throw;

                    Thread.Sleep(DelayOnRetry);
                }
            }
        }


        public bool ChunkHasArrived(long userId, int chunkNumber, string flowId)
        {
            string fileName = _getChunkFileName(userId, chunkNumber, flowId);
            return File.Exists(fileName);
        }


        public bool AttemptCompleteFileCreation(long userId, FlowUploadForm flow, string filename, out FileMetadata fileinfo )
        {
            if(_allChunksArrived(userId, flow.flowTotalChunks, flow.flowIdentifier))
            {
                var tempFileName = _combineChunkFiles(userId, flow);

                var finalFilename = _createFinalPath(userId, filename);
                finalFilename = FileMetadata.VerifyFileUniqueness(finalFilename);

                File.Move(tempFileName, finalFilename);
                _deleteChunkFiles(userId, flow);

                fileinfo = new FileMetadata(finalFilename);

                return true;
            }

            //File not yet ready to be reconstituted
            fileinfo = null;
            return false; 
        }


        public void SaveCompleteFile(Stream file, string path)
        {
            var filename = Path.Combine(root, path);
            using (var fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }
        }


        private string _combineChunkFiles(long userId, FlowUploadForm flow)
        {
            var tempFileName = Path.Combine(root, "temp", userId.ToString(), flow.flowIdentifier);
            for (int i = 1; i <= NumberOfRetries; ++i)
            {
                try
                {
                    using (var destStream = File.Create(tempFileName, 15000))
                    {
                        for (int chunkNumber = 1; chunkNumber <= flow.flowTotalChunks; chunkNumber++)
                        {
                            var chunkFileName = _getChunkFileName(userId, chunkNumber, flow.flowIdentifier);
                            using (var sourceStream = File.OpenRead(chunkFileName))
                            {
                                sourceStream.CopyTo(destStream);
                            }
                        }
                    }
                    break;
                }
                catch (IOException e)
                {
                    // You may check error code to filter some exceptions, not every error
                    // can be recovered.
                    if (i == NumberOfRetries) // Last one, (re)throw exception and exit
                        throw;

                    Thread.Sleep(DelayOnRetry);
                }
            }

            return tempFileName;
        }


        private void _deleteChunkFiles(long userId, FlowUploadForm flow){
            for (int chunkNumber = 1; chunkNumber <= flow.flowTotalChunks; chunkNumber++)
            {
                var chunkFileName = _getChunkFileName(userId, chunkNumber, flow.flowIdentifier);
                File.Delete(chunkFileName);
            }
        }


        private string _getChunkFileName(long userId, int chunkNumber, string chunkId)
        {
            return Path.Combine(root, "temp", userId.ToString(), string.Format("{0}-{1}", chunkId, chunkNumber.ToString()));
        }


        private string _createFinalPath(long userId, string filename)
        {
            string filepath = Path.Combine(root, userId.ToString());
            Directory.CreateDirectory(filepath);

            filepath = Path.Combine(filepath, "movies");
            Directory.CreateDirectory(filepath);

            return Path.Combine(filepath, filename);

        }


        private string _createChunkFile(long userId, int chunkNumber, string flowId)
        {
            string filename = _getChunkFileName(userId, chunkNumber, flowId);
            Directory.CreateDirectory(Path.GetDirectoryName(filename));
            File.Create(filename).Dispose();
            return filename;
        }


        private bool _allChunksArrived(long userId, int totalChunks, string flowId)
        {
            //Starting from the top lessons the loop length as the files are sent starting from 1
            for (int chunkNumber = totalChunks; chunkNumber >= 1; chunkNumber--)
                if (!ChunkHasArrived(userId, chunkNumber, flowId))
                    return false;
            return true;
        }
       
    }
}
