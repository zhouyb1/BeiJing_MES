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
            WriterInfaceLog(Context, reqData);
            var checkTime = ConvertStringToDateTime(reqData.time);
            var entity = new Mes_CheckRecordEntity()
            {
                C_Type = reqData.type,
                C_DeviceKey = reqData.deviceKey,
                C_PersonId = reqData.personId,
                C_Ip = reqData.ip,
                C_ScanDate = checkTime,
                C_ScanTime = checkTime

            };
            checkRecordIbll.SaveEntity("", entity);
            return SendSuccess(new {result = 1, success = true});
        }

        public static string GetTimeChuo(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t.ToString();
        }

        /// <summary>        
        /// 时间戳转为C#格式时间        
        /// </summary>        
        /// <param name=”timeStamp”></param>        
        /// <returns></returns>        
        private DateTime ConvertStringToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
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