﻿using System;
using System.Collections.Generic;
namespace Ayma.Util.Operat
{
    /// <summary>
    /// 创建人：Ayma
    /// 日 期：2017.03.08
    /// 描 述：当前连接用户信息
    /// </summary>
    public class Operator
    {
        /// <summary>
        /// 应用Id
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime logTime { get; set; }
        /// <summary>
        /// 登录IP地址
        /// </summary>
        public string iPAddress { get; set; }
        /// <summary>
        /// 浏览器名称
        /// </summary>
        public string browser { get; set; }
        /// <summary>
        /// 登录者标识
        /// </summary>
        public string loginMark { get; set; }
        /// <summary>
        /// 票据信息
        /// </summary>
        public string token { get; set; }
        /// <summary>
        /// 可管理的公司
        /// </summary>
        public List<string> userPowers_CompanyIds { get; set; }
        /// <summary>
        /// 可管理的车站
        /// </summary>
        public List<string> stationIds { get; set; }

        /// <summary>
        /// 可管理的仓库
        /// </summary>
        public List<string> warehouseIds { get; set; }

        /// <summary>
        /// 可管理的门店
        /// </summary>
        public List<string> shopIds { get; set; }
        /// <summary>
        /// 可管理商家
        /// </summary>
        public List<string> businessIds { get; set; }
    }
}