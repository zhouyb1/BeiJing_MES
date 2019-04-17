using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 描述: 实体层 -- MesWorkShop表映射类
    /// </summary>
    public partial class MesWorkShopEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        public string ID { set; get; }
        /// <summary>
        /// 车间编码
        /// </summary>
        public string W_Code { set; get; }
        /// <summary>
        /// 车间名称
        /// </summary>
        public string W_Name { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string W_Remark { set; get; }
       
        /// <summary>
        /// 添加人
        /// </summary>
        public string CreateUserName { set; get; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime CreateDate { set; get; }
        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyUserName { set; get; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyDate { set; get; }

        #endregion
    }
}
