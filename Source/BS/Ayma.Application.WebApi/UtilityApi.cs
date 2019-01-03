using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Ayma.Application.Base.SystemModule;
using Ayma.Util;
using Nancy;

namespace Ayma.Application.WebApi
{
    public class UtilityApi:BaseApi
    {
        private AnnexesFileIBLL annexesFileIbll = new AnnexesFileBLL();
        public UtilityApi() : base("/webapi/Utility")
        {
            Get["/GetFile"] = GetFile;

        }

        public Response GetFile(dynamic _)
        {
            var fileId = this.GetReqData().ToJObject()["fileId"].ToString();
            var data = annexesFileIbll.GetEntity(fileId);
            if (data!=null)
            {
                var fileType = data.F_FileType;
                var filesType = Config.GetValue("ImageTypes").ToLower();
                var filePath = data.F_FilePath;
                if (filesType.Contains(fileType))
                {
                    if (FileDownHelper.FileExists(filePath))
                    {
                        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                        if (fileType == "jpg" || fileType == "jpeg")
                        {
                            fileType = "jpeg";
                        }
                        return Response.FromStream(stream, @"image/" + filesType);
                    }
                }
            }
            return Fail("资源不存在");
        }
    }
}