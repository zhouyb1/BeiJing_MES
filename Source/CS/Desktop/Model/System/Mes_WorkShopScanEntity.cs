﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public partial class Mes_WorkShopScanEntity
    {
        #region 属性\
        /// <summary>
        /// 
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string W_RecordCode { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string W_RecordName { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string W_ProceCode { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string W_ProceName { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string W_WorkShop { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string W_WorkShopName { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string W_OrderNo { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public int W_Status { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string W_CreateBy { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime W_CreateDate { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string W_GoodsCode { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string W_GoodsName { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string W_Unit { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public decimal W_Qty { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public string W_Batch { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public Decimal W_Price { set; get; }
        
        /// <summary>
        /// 
        /// </summary>
        public string W_Remark { set; get; }
        #endregion
    }
}