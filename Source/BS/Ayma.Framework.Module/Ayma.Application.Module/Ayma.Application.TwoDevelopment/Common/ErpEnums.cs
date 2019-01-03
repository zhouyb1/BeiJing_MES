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
        /// 单据类型
        /// </summary> 
        public enum BillTypeEnum
        {
            /// <summary>
            /// 商品
            /// </summary>
            Goods = 1,
            /// <summary>
            /// 备品
            /// </summary>
            Spare = 2,
            /// <summary>
            /// 全部
            /// </summary>
            All = 3
        }

        /// <summary>
        /// 车站类型
        /// </summary>
        public enum StationTypeEnum
        {
            /// <summary>
            /// 高铁
            /// </summary>
            HighSpeedRail = 0,
            /// <summary>
            /// 普速
            /// </summary>
            UniversalSpeed = 1,
            /// <summary>
            /// 混合
            /// </summary>
            Blend = 2
        }

        /// <summary>
        /// 归属类型
        /// </summary>
        public enum AscriptionTypeEnum
        {
            /// <summary>
            /// 超市
            /// </summary>
            Supermarket = 1,
            /// <summary>
            /// 普车
            /// </summary>
            UniversalCar = 2,
            /// <summary>
            /// 高铁
            /// </summary>
            HighSpeedRail = 3
        }

        /// <summary>
        /// 商品编码类型
        /// </summary>
        public enum GoodsCodeTypeEnum
        {
            /// <summary>
            /// 普通码21
            /// </summary>
            NormalCode = 0,
            /// <summary>
            /// 称重码22
            /// </summary>
            WeighingCode = 2,
            /// <summary>
            /// 金额码23
            /// </summary>
            MoneyCode = 3,
            /// <summary>
            /// 组合商品26
            /// </summary>
            CompositeCommodities = 4,
            /// <summary>
            /// 自用原料21
            /// </summary>
            SelfuseMaterials = 5,
            /// <summary>
            /// 基本码21
            /// </summary>
            BasicCode = 8,
            /// <summary>
            /// 代收银21
            /// </summary>
            Receipts = 9

        }

        /// <summary>
        /// 商品状态
        /// </summary>
        public enum GoodsStatusEnum
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 冻结
            /// </summary>
            Freeze = 1,
            /// <summary>
            /// 暂时冻结
            /// </summary>
            TemporaryFreeze = 2,
            /// <summary>
            /// 停止销售
            /// </summary>
            StopSelling = 3,
            /// <summary>
            /// 清理中
            /// </summary>
            Cleaning = 4,
            /// <summary>
            ///已清理
            /// </summary>
            Cleaned = 5
        }
        /// <summary>
        /// 商品设置状态
        /// </summary>
        public enum GoodsSetStatusEnum
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 停止销售
            /// </summary>
            StopSelling = 3
        }
        /// <summary>
        /// 商品类型
        /// </summary>
        public enum GoodsKindEnum
        {
            /// <summary>
            /// 普通
            /// </summary>
            Ordinary = 0,
            /// <summary>
            /// 特有(自产/OEM)
            /// </summary>
            Specific = 1,
            /// <summary>
            /// 服务
            /// </summary>
            Service = 2
        }

        /// <summary>
        /// 商品价格档
        /// </summary>
        public enum GoodsLevelEnum
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// 高档
            /// </summary>
            TopGrade = 1,
            /// <summary>
            /// 中档
            /// </summary>
            MediumGrade = 2,
            /// <summary>
            /// 停止销售
            /// </summary>
            LowGrade = 3
        }
        /// <summary>
        /// 价格因子
        /// </summary>
        public enum PriceRateEnum
        {
            /// <summary>
            /// 普通商品
            /// </summary>
            GeneralGoods = 1,
            /// <summary>
            /// 称重商品斤
            /// </summary>
            WeighHeavyJin = 500,
            /// <summary>
            /// 称重商品公斤
            /// </summary>
            WeighHeavyKg = 1000
        }
        /// <summary>
        /// 商品标志
        /// </summary>
        public enum TimesFlagEnum
        {
            /// <summary>
            /// 普通商品
            /// </summary>
            GeneralGoods = 0,
            /// <summary>
            /// 一次性商品
            /// </summary>
            OneTimeGoods = 1,
            /// <summary>
            /// 年节性商品
            /// </summary>
            AnnualGoods = 2,
            /// <summary>
            /// 促销赠品
            /// </summary>
            PromotionGiveaways = 3,
            /// <summary>
            /// 季节性商品
            /// </summary>
            SeasonalGoods = 4
        }
        /// <summary>
        /// 经营类型
        /// </summary>
        public enum RunTypeEnum
        {
            /// <summary>
            /// 自营
            /// </summary>
            SelfEmployed = 0,
            /// <summary>
            /// 联营
            /// </summary>
            JointOperation = 1,
            /// <summary>
            /// 租赁
            /// </summary>
            Lease = 2,
            /// <summary>
            /// 代售
            /// </summary>
            Substitute = 3,
            /// <summary>
            /// 代理
            /// </summary>
            Agency = 4
        }

        /// <summary>
        /// 联营基本码类型
        /// </summary>
        public enum UnionCodeTypeEnum
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 特价
            /// </summary>
            SpecialOffer = 1,
            /// <summary>
            /// 特殊
            /// </summary>
            Special = 2
        }
        /// <summary>
        /// 商品库存处理模式
        /// </summary>
        public enum StockModeEnum
        {
            /// <summary>
            /// 记库存
            /// </summary>
            Inventory = 0,
            /// <summary>
            /// 不记库存
            /// </summary>
            NotTakingStock = 1,
            /// <summary>
            /// 只记数量库存
            /// </summary>
            OnlyCountInventory = 2
        }
        /// <summary>
        /// 所属类型
        /// </summary>
        public enum BelongTypeEnum
        {
            /// <summary>
            /// 车站商业
            /// </summary>
            StationBusiness = 1,
            /// <summary>
            /// 小红帽
            /// </summary>
            LittleRedCap = 2,
        }
        /// <summary>
        /// 入会方式
        /// </summary>
        public enum JoinTypeEnum
        {
            /// <summary>
            /// 扫码入会
            /// </summary>
            Scavenging = 1,
            /// <summary>
            /// 店员添加
            /// </summary>
            ShopAssistant = 2,
        }
        /// <summary>
        /// 账户状态
        /// </summary>
        public enum UserStatusEnum
        {
            /// <summary>
            /// 正常
            /// </summary>
            Normal = 0,
            /// <summary>
            /// 待验证
            /// </summary>
            ToBeVerified = 1,
            /// <summary>
            /// 待审核
            /// </summary>
            ToBeAudited = 2,
            /// <summary>
            /// 锁定
            /// </summary>
            Locking = 3,
        }

        /// <summary>
        /// 单号自动编码规则
        /// </summary>
        public enum OrderNoRuleEnum
        {
            /// <summary>
            /// 商品入库单
            /// </summary>
            GoodsIn = 10000,
            /// <summary>
            /// 商品出库单
            /// </summary>
            GoodsOut = 10001,
            /// <summary>
            /// 商品价格驱动单
            /// </summary>
            GoodsPriceDrive = 10002,
            /// <summary>
            /// 会员组价格驱动单
            /// </summary>
            UserGroupPriceDrive = 10003,
            /// <summary>
            /// 仓库报废单
            /// </summary>
            ScrapStock = 10004,
            /// <summary>
            /// 商品退回供应商单
            /// </summary>
            GoodsReturnToSupplier = 10005,
            /// <summary>
            /// 商品调拨单
            /// </summary>
            GoodsTransfer = 10006,
            /// <summary>
            /// 商品退回仓库单
            /// </summary>
            GoodsToReturnWarehouse = 10007,
            /// <summary>
            /// 门店报废单
            /// </summary>
            ShopScrapStock = 10008,
            /// <summary>
            /// 商品主编码
            /// </summary>
            GoodsBaseCode = 20000,
            /// <summary>
            /// 自营门店自动编码
            /// </summary>
            ShopAutoCode = 20003, 
            /// <summary>
            /// 联营门店自动编码
            /// </summary>
            ShopAutoCodePool = 20004,
            /// <summary>
            /// 出租门店自动编码
            /// </summary>
            ShopAutoCodeLease = 20005,
            /// <summary>
            /// 库存数量初始化
            /// </summary>
            GoodsStockQtyInit = 10009,
            /// <summary>
            /// 盘点
            /// </summary>
            CheckWarehouse = 300001,
            /// <summary>
            /// 供应商结账(入库)
            /// </summary>
            SupplierCheckoutIn = 300002,
            /// <summary>
            /// 供应商结算(销售)
            /// </summary>
            SupplierCheckoutOut = 300003,
            /// <summary>
            /// 门店盘点
            /// </summary>
            ShopCheckWarehouse=300004,
            /// <summary>
            /// 商品入库单_联营
            /// </summary>
            GoodsIn_Pool = 90000,
            /// <summary>
            /// 商品出库单_联营
            /// </summary>
            GoodsOut_Pool = 90001,
            /// <summary>
            /// 商家报废单_联营
            /// </summary>
            ScrapStock_Pool = 90004,
            /// <summary>
            /// 商品退回供应商单_联营
            /// </summary>
            GoodsReturnToSupplier_Pool = 90005,
            /// <summary>
            /// 商品调拨单_联营
            /// </summary>
            GoodsTransfer_Pool = 90006,
            /// <summary>
            /// 商品报废单_联营
            /// </summary>
            GoodsScrapStock_Pool = 90008,
            /// <summary>
            /// 退回仓库单_联营
            /// </summary>
            GoodsToReturnWarehouse_Pool = 90007,
            /// <summary>
            /// 商家盘点_联营
            /// </summary>
            BusinessCheckWarehouse_Pool=90009,
            /// <summary>
            /// 盘盈盘亏单_联营
            /// </summary>
            CheckWarehouse_POOL=90010,
            /// <summary>
            /// 会员组调价_联营
            /// </summary>
            UserGroupPriceDrive_POOL=90011,
            /// <summary>
            /// 供应商编码
            /// </summary>
            SupplierCode = 200021,
            /// <summary>
            /// 供应商编码_联营
            /// </summary>
            SupplierCode_Pool = 200022,
            /// <summary>
            /// 商家
            /// </summary>
            Business=90015,
            /// <summary>
            /// 商家编码
            /// </summary>
            BusinessCode=90016,
            /// <summary>
            /// 要货单
            /// </summary>
            RequireGoods=90017
        }
        /// <summary>
        /// 机器类型
        /// </summary>
        public enum MachineTypeEnum
        {
            /// <summary>
            /// POS收银机
            /// </summary>
            CashRegister = 1,
            /// <summary>
            /// PDA
            /// </summary>
            PDA = 2
        }
        /// <summary>
        /// 商品出库单据类型
        /// </summary> 
        public enum GoodsInOutBillTypeEnum
        {
            /// <summary>
            /// 门店
            /// </summary>
            Shop = 1,
            /// <summary>
            /// 商家
            /// </summary>
            Shopping = 2,
        }
        /// <summary>
        /// 商家类别
        /// </summary>
        public enum BusinessCategoryEnum
        {
            /// <summary>
            /// 中餐
            /// </summary>
            ChineseFood = 0,
            /// <summary>
            /// 西餐
            /// </summary>
            WesternStyleFood = 1
        }
        /// <summary>
        /// 单据状态
        /// </summary>
        public enum OrderStatusEnum
        {
            /// <summary>
            /// 审核
            /// </summary>
            Audit = 0,
            /// <summary>
            /// 通过
            /// </summary>
            Pass = 1,
            /// <summary>
            /// 不通过
            /// </summary>
            NoPass = -1,
        }
        /// <summary>
        /// 单据类型
        /// </summary>
        public enum SheetTypeEnum
        {
            /// <summary>
            /// 默认价格
            /// </summary>
            DefaultPrice = 0,
            /// <summary>
            /// 普通调价
            /// </summary>
            PriceAdjustment = 1,
            /// <summary>
            /// 促销调价
            /// </summary>
            SalesPromotion = 2,
        }
        /// <summary>
        /// 促销类型
        /// </summary>
        public enum PromTypeEnum
        {
            /// <summary>
            /// 非促销
            /// </summary>
            NonPromotion = -1,
            /// <summary>
            /// 折让
            /// </summary>
            Concession = 0,
            /// <summary>
            /// 满减
            /// </summary>
            FullSubtraction = 1,
        }
        /// <summary>
        /// 门店调价权
        /// </summary>
        public enum PriceRightEnum
        {
            /// <summary>
            /// 不允许
            /// </summary>
            NotAllow = 0,
            /// <summary>
            /// 允许
            /// </summary>
            Allow = 1
        }

        public enum ShopIsBusiness
        {
            //门店
            Shop = 1,
            //商家
            Business,
            //应急
            Emergency
        }
        /// <summary>
        /// 商品审核状态
        /// </summary>
        public enum ReviewMarkEnum
        {
            /// <summary>
            /// 待审核
            /// </summary>
            Audit = 0,
            /// <summary>
            /// 通过
            /// </summary>
            Pass = 1,
            /// <summary>
            /// 不通过
            /// </summary>
            NoPass = -1
        }
        /// <summary>
        /// 商品调价是否提交状态
        /// </summary>
        public enum PostMarkEnum
        {
            /// <summary>
            /// 无提交临时单据
            /// </summary>
            NoPost = 0,
            /// <summary>
            /// 已经提交
            /// </summary>
            IsPost = 1
        }
        //自营还是联营
        public enum SysType
        {
            /// <summary>
            /// 自营
            /// </summary>
            SelfSupport = 1,
            /// <summary>
            /// 联营
            /// </summary>
            Pool = 2
        }
        /// <summary>
        /// 支付方式
        /// </summary>
        public enum PaymentTypeEnum
        {
            /// <summary>
            /// 微信
            /// </summary>
            WeChat = 1,
            /// <summary>
            /// 支付宝
            /// </summary>
            Alipay = 2,
            /// <summary>
            /// 现金
            /// </summary>
            Cash = 3,
            /// <summary>
            /// 建行龙支付
            /// </summary>
            CcbPayment = 4,
            /// <summary>
            /// 银联
            /// </summary>
            UnionPay = 5,
             /// <summary>
            /// 云闪付
            /// </summary>
            CloudPay = 6,
            /// <summary>
            /// 小程序
            /// </summary>
            SmallProgram = 7
        }
        /// <summary>
        /// 费率类型
        /// </summary>
        public enum RateTypeEnum
        {
            /// <summary>
            /// 线上
            /// </summary>
            OnLine = 1,
            /// <summary>
            /// 线下
            /// </summary>
            UnderLine = 2
        }
        public enum TypeEnum
        {
            /// <summary>
            /// 出库单
            /// </summary>
            GoodsOut=1,
            /// <summary>
            /// 要货单
            /// </summary>
            RequireGoods=2
        }
    }
}
