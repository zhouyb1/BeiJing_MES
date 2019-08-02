using Ayma.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-06 14:55
    /// 描 述：工序管理
    /// </summary>
    public partial class Mes_ProceEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("ID")]
        public string ID { get; set; }    
   
        /// <summary>
        /// 工序号
        /// </summary>
        [Column("P_PRONO")]
        public string P_ProNo { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        [Column("P_PRONAME")]
        public string P_ProName { get; set; }
        /// <summary>
        /// 车间
        /// </summary>
        [Column("P_WORKSHOP")]
        public string P_WorkShop { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("P_REMARK")]
        public string P_Remark { get; set; }  
        /// <summary>
        /// 是否本车间最后一道工序
        /// </summary>
        [Column("P_KIND")]
        public ErpEnums.YesOrNoEnum? P_Kind { get; set; }
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

