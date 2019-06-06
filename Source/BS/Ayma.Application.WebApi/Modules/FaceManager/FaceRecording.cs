using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Ayma.Application.TwoDevelopment.MesDev;
using Ayma.Application.WebApi.Model;
using Ayma.Util;
using Nancy;
using Senparc.CO2NET.Extensions;

namespace Ayma.Application.WebApi.Modules.FaceManager
{
    public class FaceRecording : BaseApi
    {
        private CheckRecordIBLL checkRecordIbll = new CheckRecordBLL();
        public FaceRecording() : base("/api/FaceRecording")
        {
            //注册api
            Post["GetUserInfo"] = GetUserInfo;
        }

        /// <summary>
        /// 人脸识别记录
        /// </summary>
        /// <returns></returns>
        public Response GetUserInfo(dynamic _)
        {
            var reqData = this.GetReq<CollectData>();
            WriterInfaceLog(Context,reqData);
            if (reqData == null)
            {
                return SendSuccess(new { result = 0, success = false });
            }
            var entity = new Mes_CheckRecordEntity()
            {
                C_Type = reqData.type,
                C_DeviceKey = reqData.deviceKey,
                C_PersonId = reqData.personId,
                C_Ip = reqData.ip,
                C_ScanDate = reqData.time,
                C_ScanTime = reqData.time
               
            };
            checkRecordIbll.SaveEntity("", entity);
            return SendSuccess(new {result = 1, success = true});
        }

        /// <summary>
        /// 人脸拍照
        /// </summary>
        /// <param name="_"></param>
        /// <returns></returns>
        public Response TakePhotos(dynamic _)
        {
            return Fail("");
        }
    }
}