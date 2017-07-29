using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReelStream.core.Models.DataTransfer.Form
{
    public class FlowUploadForm
    {
        public int flowChunkNumber { get; set; }
        public int flowTotalChunks { get; set; }
        public int flowChunkSize { get; set; }
        public long flowTotalSize { get; set; }
        public string flowIdentifier { get; set; }
        public string flowFilename { get; set; }
        public string flowRelatviePath { get; set; }
    }
}
