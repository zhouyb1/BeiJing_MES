using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-18 09:40
    /// 描 述：篮子重量列表
    /// </summary>
    public partial class Mes_BasketEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// B_BasketCode
        /// </summary>
        [Column("B_BASKETCODE")]
        public string B_BasketCode { get; set; }
        /// <summary>
        /// B_BasketName
        /// </summary>
        [Column("B_BASKETNAME")]
        public string B_BasketName { get; set; }
        /// <summary>
        /// M_Weight
        /// </summary>
        [Column("M_WEIGHT")]
        public double? M_Weight { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.ID = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.ID = keyValue;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

