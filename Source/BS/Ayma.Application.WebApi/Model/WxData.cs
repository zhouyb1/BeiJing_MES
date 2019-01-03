using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ayma.Application.WebApi.Model
{
    public class WxData
    {
        /// <summary>
        /// 
        /// </summary>
        public bool SUCCESS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ERRCODE { get; set; }
        /// <summary>
        /// MAC验证失败！
        /// </summary>
        public string ERRMSG { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TXCODE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string timeStamp { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string nonceStr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string package { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string signType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string paySign { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string partnerid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string prepayid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string mweb_url { get; set; }
    }
}