using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-01-08 17:51
    /// 描 述：操作记录查询
    /// </summary>
    public partial class T_DocumentsMakeLogEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        [Column("F_STOCKCODE")]
        public string F_StockCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        [Column("F_STOCKNAME")]
        public string F_StockName { get; set; }
        /// <summary>
        /// 单据类型
        /// </summary>
        [Column("F_BILLTYPE")]
        public string F_BillType { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary>
        [Column("F_OPERATIONTYPE")]
        public string F_OperationType { get; set; }
        /// <summary>
        /// 操作单号
        /// </summary>
        [Column("F_ORDERNO")]
        public string F_OrderNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("F_REMARK")]
        public string F_Remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_CreateUserName = userInfo.realName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_Id = keyValue;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

