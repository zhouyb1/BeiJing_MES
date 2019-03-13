using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-12 17:32
    /// 描 述：排班记录
    /// </summary>
    public partial class Mes_ArrangeEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        [Column("A_DATE")]
        public DateTime? A_Date { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [Column("A_DATETIME")]
        public DateTime? A_DateTime { get; set; }
        /// <summary>
        /// 生产订单号
        /// </summary>
        [Column("A_ORDERNO")]
        public string A_OrderNo { get; set; }
        /// <summary>
        /// 车间编码
        /// </summary>
        [Column("A_WORKSHOPCODE")]
        public string A_WorkShopCode { get; set; }
        /// <summary>
        /// 车间名称
        /// </summary>
        [Column("A_WORKSHOPNAME")]
        public string A_WorkShopName { get; set; }
        /// <summary>
        /// 用户编码
        /// </summary>
        [Column("A_F_ENCODE")]
        public string A_F_EnCode { get; set; }
        /// <summary>
        /// 工艺代码
        /// </summary>
        [Column("A_RECORD")]
        public string A_Record { get; set; }
        /// <summary>
        /// 工序号
        /// </summary>
        [Column("A_PROCODE")]
        public string A_ProCode { get; set; }
        /// <summary>
        /// 班次
        /// </summary>
        [Column("A_CLASSCODE")]
        public string A_ClassCode { get; set; }
        /// <summary>
        /// 是否生效，1.，生效，0不生效
        /// </summary>
        [Column("A_AVAIL")]
        public string A_Avail { get; set; }
        /// <summary>
        /// 添加日期
        /// </summary>
        [Column("A_CREATEDATE")]
        public DateTime? A_CreateDate { get; set; }
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("A_CREATEBY")]
        public string A_CreateBy { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("A_UPDATEBY")]
        public string A_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("A_UPDATEDATE")]
        public DateTime? A_UpdateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("A_REMARK")]
        public string A_Remark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = Guid.NewGuid().ToString();
            this.A_CreateDate = DateTime.Now;
            this.A_CreateBy = userInfo.realName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.ID = keyValue;
            var userInfo = LoginUserInfo.Get();
            this.A_UpdateDate = DateTime.Now;
            this.A_UpdateBy = userInfo.realName;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

