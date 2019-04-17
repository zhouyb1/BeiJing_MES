using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.System
{
    /// <summary>
    /// 描述: 实体层 -- MesRecord表映射类
    /// </summary>
    public partial class MesRecordEntity
    {
        #region 属性
        /// <summary>
        /// 设备ID
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 编码
        /// </summary>
        public string R_Record { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string R_Name { set; get; }
        /// <summary>
        /// 成品物料
        /// </summary>
        public string R_GoodsCode { set; get; }
        
        #endregion
    }
}
