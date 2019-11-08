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
            /// 入库单
            /// </summary>
            MaterIn = 10001,
            /// <summary>
            /// 成品入库单
            /// </summary>
            ProjectMaterIn = 10009,
            /// <summary>
            /// 车间入库到线边仓
            /// </summary>
            InX=10011,
            /// <summary>
            /// 出库单
            /// </summary>
            Out = 10002,
            /// <summary>
            /// 成品出库单
            /// </summary>
            ProOut = 10010,
            /// <summary>
            /// 报废单
            /// </summary>
            Scrap=10003,
            /// <summary>
            /// 退供应商单
            /// </summary>
            BackSupply = 10004,
            /// <summary>
            /// 退库单
            /// </summary>
            BackToStock=10005,
            /// <summary>
            /// 组装单
            /// </summary>
            Org=10006,
            /// <summary>
            /// 抽检记录单
            /// </summary>
            Inspect=10007,
             /// <summary>
            /// 强制使用记录单号
            /// </summary>
            CompUser=10008,
            /// <summary>
            /// 领料单
            /// </summary>
            PickMater=10012

        }
        /// <summary>
        /// 是否
        /// </summary>
        public enum YesOrNoEnum
        {
            /// <summary>
            /// 是
            /// </summary>
            Yes = 1,
            /// <summary>
            /// 否
            /// </summary>
            No = 0     
        }  
        /// <summary>
        /// 入库单据类型(成品与非成品)
        /// </summary>
        public enum OrderKindEnum
        {
            /// <summary>
            /// 非成品
            /// </summary>
            NoProduct = 0,
            /// <summary>
            /// 成品
            /// </summary>
            IsProduct = 1     
        } 
       
        
        /// <summary>
        /// 商品类型
        /// </summary>
        public enum GkindEnum
        {
            /// <summary>
            /// 原材料
            /// </summary>
            Material = 1,
            /// <summary>
            /// 半成品
            /// </summary>
            ParProduct = 2,   
            /// <summary>
            /// 成品
            /// </summary>
            FinishedProduct= 3      
        } 
        /// <summary>
        /// 商品类型(二级分类)
        /// </summary>
        public enum GTkindEnum
        {
            /// <summary>
            /// 肉食
            /// </summary>
            Carnivorous = 1,
            /// <summary>
            /// 蔬菜
            /// </summary>
            Vegetables = 2,   
            /// <summary>
            /// 调料
            /// </summary>
            Seasoning = 3,
            /// <summary>
            /// 冷链
            /// </summary>
            ColdChain = 4,
            /// <summary>
            /// 面条
            /// </summary>
            Noodle = 5, 
            /// <summary>
            /// 糕点
            /// </summary>
            Cakes = 6
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
            /// 单据生成
            /// </summary>
            OrderInit = 1,   
            /// <summary>
            /// 审核成功
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
        /// 其它入库单状态
        /// </summary>
        public enum OtherInStatusEnum
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
            AuditFinish = 3,
            /// <summary>
            /// 单据删除
            /// </summary>
            IsDelete = -1,
        }
        /// <summary>
        /// 原材料销售单据状态
        /// </summary>
        public enum SaleOutStatusEnum
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
            AuditFinish = 3,
            /// <summary>
            /// 单据删除
            /// </summary>
            IsDelete = -1,
        }

        /// <summary>
        /// 消耗单单状态
        /// </summary>
        public enum ExpendStatus
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
            AuditFinish = 3,
            /// <summary>
            /// 单据删除
            /// </summary>
            IsDelete = -1,
        }
        /// <summary>
        /// 线边仓入库到车间状态
        /// </summary>
        public enum InStatusEnum
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
            AuditFinish = 3,
            /// <summary>
            /// 单据删除
            /// </summary>
            IsDelete = -1,
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
        /// <summary>
        /// 退供应商单状态
        /// </summary>
        public enum BackSupplyStatusEnum
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
            AuditFinish = 3,
            /// <summary>
            /// 单据删除
            /// </summary>
            IsDelete = -1,
        }
        /// <summary>
        /// 退供应商单状态
        /// </summary>
        public enum CompUserStatusEnum
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
            AuditFinish = 3,
            /// <summary>
            /// 单据删除
            /// </summary>
            IsDelete = -1,
        }

        public enum ScrapStatusEnum
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
            AuditFinish = 3,
            /// <summary>
            /// 单据删除
            /// </summary>
            IsDelete = -1,
        }
    }
}
