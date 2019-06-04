using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ayma.Application.WebApi.Model
{
    public class CollectData
    {
        /// <summary>
        /// 设备ip
        /// </summary>
        public string ip { get; set; }
        /// <summary>
        /// 设备出厂唯一标识码
        /// </summary>
        public string deviceKey { get; set; }
        /// <summary>
        /// 人员id
        /// </summary>
        public string personId { get; set; }
        /// <summary>
        /// 识别方式（face/card）、识别出的人员类型face_0（表示是人脸识别，且该人员在 passtime 权限时间内）， card_1(表示是刷卡识别，且该人员在 passtime 权限时间外)， face_2（表示是人脸识别，且识别失败或识别到的是陌生人）
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 现场照在设备内的保存路径，访问此 url 需设备局域网在线，且发送请求的客户端与设备处于局域网同一网段
        /// </summary>
        public string path { get; set; }
    }
}