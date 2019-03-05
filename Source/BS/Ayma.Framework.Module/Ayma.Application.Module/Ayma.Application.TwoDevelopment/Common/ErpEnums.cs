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
            /// <summary>
            /// 成品出库单
            /// </summary>
            ProOut = 10002,         
        }
        /// <summary>
        /// 生成订单状态
        /// </summary>
        public enum PStatusEnum
        {
            /// <summary>
            /// 生产中
            /// </summary>
            Productint = 0,
            /// <summary>
            /// 入库
            /// </summary>
            StockIn = 1,   
            /// <summary>
            /// 出库
            /// </summary>
            StockOut= 2,         
        } 
        /// <summary>
        /// 成品入库单状态
        /// </summary>
        public enum MaterInStatusEnum
        {
            /// <summary>
            /// 单据生成
            /// </summary>
            NoAudit = 1,
            /// <summary>
            /// 审核通过
            /// </summary>
            Audit = 2,   
            /// <summary>
            /// 单据完成
            /// </summary>
            AuditFinish= 3,
            /// <summary>
            /// 单据删除
            /// </summary>
            IsDelete= -1,         
        }
        /// <summary>
        /// 成品出库单状态
        /// </summary>
        public enum ProOutStatusEnum
        {
            /// <summary>
            /// 单据生成
            /// </summary>
            NoAudit = 1,
            /// <summary>
            /// 审核通过
            /// </summary>
            Audit = 2,   
            /// <summary>
            /// 单据完成
            /// </summary>
            AuditFinish= 3,
            /// <summary>
            /// 单据删除
            /// </summary>
            IsDelete= -1,         
        }
        /// <summary>
        /// 调拨单状态
        /// </summary>
        public enum RequistStatusEnum
        {
            /// <summary>
            /// 单据生成
            /// </summary>
            NoAudit = 1,
            /// <summary>
            /// 审核通过
            /// </summary>
            Audit = 2,   
            /// <summary>
            /// 单据完成
            /// </summary>
            AuditFinish= 3,
            /// <summary>
            /// 单据删除
            /// </summary>
            IsDelete= -1,         
        }
    }
}
