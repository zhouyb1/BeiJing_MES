using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ayma.Application.TwoDevelopment
{
    /// <summary>
    /// 所有枚举类型
    /// </summary>
    public class ErpEnums
    {
        
        /// <summary>
        /// 单号自动编码规则
        /// </summary>
        public enum OrderNoRuleEnum
        {
            /// <summary>
            /// 仓库调拨单
            /// </summary>
            Requist = 10000,
            /// <summary>
            /// 成品入库单
            /// </summary>
            MaterIn = 10001,         
        }
    }
}
