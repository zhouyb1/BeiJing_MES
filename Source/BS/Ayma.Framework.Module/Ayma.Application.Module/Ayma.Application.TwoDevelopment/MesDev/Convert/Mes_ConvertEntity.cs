using Ayma.DataBase.SqlServer;
using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-20 09:36
    /// 描 述：物料转换对应表
    /// </summary>
    public partial class Mes_ConvertEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        [Column("C_CODE")]
        public string C_Code { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        [Column("C_NAME")]
        public string C_Name { get; set; }
        /// <summary>
        /// 转换后物料编码
        /// </summary>
        [Column("C_SECCODE")]
        public string C_SecCode { get; set; }
        /// <summary>
        /// 转换后物料名称
        /// </summary>
        [Column("C_SECNAME")]
        public string C_SecName { get; set; }
        /// <summary>
        /// 最大转化率
        /// </summary>
        [Column("C_MAX")]
        [DecimalPrecision(18, 6)]
        public decimal? C_Max { get; set; }   
        /// <summary>
        /// 最小转化率
        /// </summary>
        [Column("C_MIN")]
        [DecimalPrecision(18, 6)]
        public decimal? C_Min { get; set; }
        /// <summary>
        /// 工艺代码
        /// </summary>
        [Column("C_RECORDCODE")]
        public string C_RecordCode { get; set; }  
        /// <summary>
        /// 工序号
        /// </summary>
        [Column("C_PRONO")]
        public string C_ProNo { get; set; }    
        /// <summary>
        /// 工序名称
        /// </summary>
        [Column("C_PRONAME")]
        public string C_ProName { get; set; }  
        /// <summary>
        /// 添加人
        /// </summary>
        [Column("C_CREATEBY")]
        public string C_CreateBy { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [Column("C_CREATEDATE")]
        public DateTime? C_CreateDate { get; set; }
        /// <summary>
        /// 修改人
        /// </summary>
        [Column("C_UPDATEBY")]
        public string C_UpdateBy { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        [Column("C_UPDATEDATE")]
        public DateTime? C_UpdateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("C_REMARK")]
        public string C_Remark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = Guid.NewGuid().ToString();
            this.C_CreateDate = DateTime.Now;
            this.C_CreateBy = userInfo.userId;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            var userInfo = LoginUserInfo.Get();
            this.ID = keyValue;
            this.C_UpdateDate = DateTime.Now;
            this.C_UpdateBy = userInfo.userId;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

