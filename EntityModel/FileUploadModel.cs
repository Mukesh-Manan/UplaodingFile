using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UploadingFileToDB.EntityModel
{
    public class FileUploadModel
    {
        public int OpportunityID { get; set; }
        public string FileName { get; set; }
        public string UploadedBy { get; set; }
        public IFormFile File { get; set; }
    }
}
