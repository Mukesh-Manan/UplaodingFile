using System;
using System.Collections.Generic;

namespace UploadingFileToDB.DBModels
{
    public partial class EngagementUploadFile
    {
        public EngagementUploadFile()
        {
            EngagementUpload = new HashSet<EngagementUpload>();
        }

        public int Id { get; set; }
        public string FileName { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedDate { get; set; }
        public byte[] UploadedFile { get; set; }
        public string FileContentType { get; set; }

        public virtual ICollection<EngagementUpload> EngagementUpload { get; set; }
    }
}
