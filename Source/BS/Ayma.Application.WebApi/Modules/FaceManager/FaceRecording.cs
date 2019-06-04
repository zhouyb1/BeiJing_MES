using System;
using System.Collections.Generic;
using System.Linq;
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
        public Response GetUserInfo (dynamic _)
        {
            var reqData = Request.Form;
            if (reqData.Count==0)
            {
                return Fail("没有参数");
            }
            var ip = reqData["ip"].ToString();
            var personId = reqData["personId"].ToString();
            var path = reqData["path"].ToString();
            var type = reqData["type"].ToString();
            var deviceKey = reqData["deviceKey"].ToString();
            
            if (string.IsNullOrWhiteSpace(ip))
            {
                return Fail("ip为空！");
            }
            if (string.IsNullOrWhiteSpace(personId))
            {
                return Fail("personId为空！");
            }
            if (string.IsNullOrWhiteSpace(path))
            {
                return Fail("照片路径为空！");
            }
            if (string.IsNullOrWhiteSpace(type))
            {
                return Fail("类型为空！");
            }
            
            //查询有无该条人脸识别的数据
            var entity = checkRecordIbll.GetMes_CheckRecordEntity(personId);
            if (entity == null)
            {
                entity = new Mes_CheckRecordEntity()
                {
                    C_Type = type,
                    C_DeviceKey = deviceKey,
                    C_PersonId = personId,
                    C_Ip = ip
                };
                checkRecordIbll.SaveEntity("", entity);
            }
            else
            {
                entity.C_State = CheckState.成功;
                checkRecordIbll.SaveEntity(entity.ID, entity);
            }
            return Success(new { result = 1, success = true });
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