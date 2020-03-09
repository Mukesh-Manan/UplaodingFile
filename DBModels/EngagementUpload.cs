using System;
using System.Collections.Generic;

namespace UploadingFileToDB.DBModels
{
    public partial class EngagementUpload
    {
        public int Id { get; set; }
        public int OpportunityId { get; set; }
        public int FileUploadId { get; set; }

        public virtual EngagementUploadFile FileUpload { get; set; }
    }
}
