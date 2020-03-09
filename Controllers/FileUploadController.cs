using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadingFileToDB.DBModels;
using UploadingFileToDB.EntityModel;

namespace UploadingFileToDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
    public class FileUploadController : ControllerBase
    {
        [HttpPost]
        [Route("UploadFiles")]
        public ActionResult UploadFiles([FromForm]FileUploadModel fileUploadModel)
        {
            try
            {
                using (FileUploadContext dbContext = new FileUploadContext())
                {
                    var engModel = new EngagementUploadFile()
                    {
                        FileName = fileUploadModel.FileName,
                        UploadedBy = fileUploadModel.UploadedBy,
                        UploadedDate = DateTime.Now
                    };

                    using (var memoryStream = new MemoryStream())
                    {
                        fileUploadModel.File.CopyTo(memoryStream);                        
                        engModel.UploadedFile = memoryStream.ToArray();
                        engModel.FileContentType = fileUploadModel.File.ContentType;
                    }

                    dbContext.EngagementUploadFile.Add(engModel);
                    dbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {

            }
            return Ok();
        }


        [HttpGet]
        [Route("DownloadFiles")]
        public async Task<ActionResult> DownloadFiles( int engagementUploadFileID)
        {
            try
            {
                using (FileUploadContext dbContext = new FileUploadContext())
                {
                    EngagementUploadFile engUploadFile =
                                dbContext.EngagementUploadFile.Where(obj => obj.Id == engagementUploadFileID).FirstOrDefault();


                    return File(new MemoryStream(engUploadFile.UploadedFile), engUploadFile.FileContentType, engUploadFile.FileName);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}