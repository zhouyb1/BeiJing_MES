using Ayma.Application.TwoDevelopment.MesDev.GoodsInfo;
using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Ayma.Application.TwoDevelopment.MesDev;
using Ayma.Application.Organization;

namespace Ayma.Application.TwoDevelopment.Tools
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2018-10-09 10:32
    /// 描 述：商家
    /// </summary>
    public partial class ToolsService : RepositoryFactory
    {

        #region 获取数据
        /// <summary>
        /// 根据部门编码获取部门实体信息
        /// </summary>
        /// <param name="code">物料编码</param>
        /// <returns></returns>
        public DepartmentEntity ByCodeGetDepartmentEntity(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<DepartmentEntity>(x => x.F_EnCode == code || x.F_FullName == code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 根据仓库编码获取仓库实体信息
        /// </summary>
        /// <param name="code">物料编码</param>
        /// <returns></returns>
        public Mes_StockEntity ByCodeGetStockEntity(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_StockEntity>(x => x.S_Code == code || x.S_Name == code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 根据班组编码获取班组实体信息
        /// </summary>
        /// <param name="code">班组编码</param>
        /// <returns></returns>
        public Mes_TeamEntity ByCodeGetTeamEntity(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_TeamEntity>(x =>x.T_Code == code || x.T_Name == code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_CustomerEntity> GetCustomerList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_CustomerEntity>();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetDepartmentList()
        {
            try
            {
                return this.BaseRepository().FindList<DepartmentEntity>();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取原物料仓库列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_StockEntity> GetOriginalStockList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_StockEntity>(c => c.S_Kind==1);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取原物料仓库和半成品仓库列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_StockEntity> GetOtherStockList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_StockEntity>(c => c.S_Kind == 1||c.S_Kind==2);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获所仓库列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_StockEntity> GetStockList()
        {
            try
            {

                return this.BaseRepository().FindList<Mes_StockEntity>();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 根据参数获取仓库列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_StockEntity> GetStockListByParam(string strWhere)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"select * from Mes_Stock ");
                if (!strWhere.IsEmpty())
                {
                    sb.Append(" where " + strWhere);
                }
                return this.BaseRepository().FindList<Mes_StockEntity>(sb.ToString());
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取非成品仓库列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_StockEntity> GetNoProjStockList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_StockEntity>(c => c.S_Kind != 3);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取线边仓仓库列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_StockEntity> GetLineStockList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_StockEntity>(c => c.S_Kind == 4);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取成品仓库列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_StockEntity> GetProjStockList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_StockEntity>(c => c.S_Kind == 3);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取车间列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_WorkShopEntity> GetWorkShopList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_WorkShopEntity>();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取生产订单号列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_ProductOrderHeadEntity> GetProductOrderList(string orderNo)
        {
            try
            {
                DateTime startTime = Convert.ToDateTime(DateTime.Now.AddDays(-3000).ToString("yyyy-MM-dd"));
                return this.BaseRepository()
                          .FindList<Mes_ProductOrderHeadEntity>(t => (t.P_Status == ErpEnums.PStatusEnum.StockOut && t.P_OrderDate >= startTime) || t.P_OrderNo == orderNo)
                          .OrderByDescending(t => t.P_OrderNo);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 根据订单时间起止获取生产订单号列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_ProductOrderHeadEntity> GetProductOrderListBy(DateTime orderStartDate,
            DateTime orderEndDate)
        {
            try
            {
                return this.BaseRepository()
                          .FindList<Mes_ProductOrderHeadEntity>(t => t.P_OrderDate >= orderStartDate && t.P_OrderDate <= orderEndDate)
                          .OrderByDescending(t => t.P_OrderNo);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 根据车间编码获取车间实体信息
        /// </summary>
        /// <param name="code">车间编码</param>
        /// <returns></returns>
        public Mes_WorkShopEntity ByCodeGetWorkShopEntity(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_WorkShopEntity>(t => t.W_Name == code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 根据车间名称获取车间实体信息
        /// </summary>
        /// <param name="name">车间名称</param>
        /// <returns></returns>
        public Mes_WorkShopEntity ByNameGetWorkShopEntity(string name)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_WorkShopEntity>(t => t.W_Name == name);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取工艺列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_RecordEntity> GetRecordList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_RecordEntity>().OrderBy(t => t.R_Record);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取工序号
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_OrgResHeadEntity> ByCodeGetProceEntity(string Code)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_OrgResHeadEntity>().Where(t => t.O_Record==Code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取配方列表
        /// </summary>
        /// <returns></returns>
        public List<Mes_BomRecordEntity> GetBomRecordList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_BomRecordEntity>().ToList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取工序树形列表
        /// </summary>
        /// <returns></returns>
        public List<Mes_ProceEntity> GetProceTreeList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_ProceEntity>().ToList();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取工序列表
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<Mes_ProceEntity> GetProceList(string parentId)
        //{
        //    try
        //    {
        //        return this.BaseRepository().FindList<Mes_ProceEntity>(t => t.P_ParentId == parentId);
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is ExceptionEx)
        //        {
        //            throw;
        //        }
        //        else
        //        {
        //            throw ExceptionEx.ThrowServiceException(ex);
        //        }
        //    }
        //}

        /// <summary>
        /// 根据物料编码获取物料实体信息
        /// </summary>
        /// <param name="code">物料编码</param>
        /// <returns></returns>
        public Mes_GoodsEntity ByCodeGetGoodsEntity(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_GoodsEntity>(x => x.G_Code == code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 根据物料名称获取物料实体信息
        /// <param name="name">物料名称</param>
        /// </summary>
        /// <returns></returns>
        public Mes_GoodsEntity ByNameGetGoodsEntity(string name)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_GoodsEntity>(x => x.G_Name == name);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 根据工序代码获取工序表实体
        /// <param name="code">工艺代码</param>
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<Mes_ProceEntity> GetProceListBy(string code)
        //{
        //    try
        //    {
        //        var entity = this.BaseRepository().FindEntity<Mes_ProceEntity>(x => x.P_RecordCode == code);
        //        var list = this.BaseRepository().FindList<Mes_ProceEntity>(t => t.P_ParentId == entity.ID || t.ID == entity.ID);
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        if (ex is ExceptionEx)
        //        {
        //            throw;
        //        }
        //        else
        //        {
        //            throw ExceptionEx.ThrowServiceException(ex);
        //        }
        //    }
        //}
        /// <summary>
        /// 获取工序列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_ProceEntity> GetProceList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_ProceEntity>().OrderBy(x => x.P_ProNo);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 根据(工序号或工序名称)获取工序实体
        /// </summary>
        /// <param name="code">工序号或工序名称</param>
        /// <returns></returns>
        public Mes_ProceEntity ByGetProceEntity( string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ProceEntity>(x =>x.P_ProNo == code || x.P_ProName == code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取物料列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_GoodsEntity> GetGoodsList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_GoodsEntity>();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 根据供应商获取物料列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_GoodsEntity> GetGoodsListBySupplyName(string G_SupplyName)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_GoodsEntity>(x => x.G_SupplyName == G_SupplyName);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取成品物料列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_GoodsEntity> GetProjGoodsList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_GoodsEntity>(c => c.G_Kind == ErpEnums.GkindEnum.FinishedProduct);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取原物料物料列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_GoodsEntity> GetMaterialGoodsList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_GoodsEntity>(c => c.G_Kind == ErpEnums.GkindEnum.Material);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取不合格原因列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_ResonEntity> GetReasonList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_ResonEntity>();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取商品二级分类列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_GoodKindEntity> GetGoodsKind()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_GoodKindEntity>();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 根据编码获取商品二级分类实体
        /// </summary>
        /// <returns></returns>
        public Mes_GoodKindEntity GetGoodsKindEntityBy(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_GoodKindEntity>(t => t.G_Code == code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 根据门编码获取门实体信息
        /// </summary>
        /// <param name="code">门编码</param>
        /// <returns></returns>
        public Mes_DoorEntity ByCodeGetDoorEntity(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_DoorEntity>(x => x.D_Code == code||x.D_Name==code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_DoorEntity> GetDoorList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_DoorEntity>();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 根据供应商编码获取供应商实体信息
        /// </summary>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public Mes_SupplyEntity ByCodeGetSupplyEntity(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_SupplyEntity>(x => x.S_Code == code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        } 
        /// <summary>
        /// 根据供应商名称获取供应商实体信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns></returns>
        public Mes_SupplyEntity ByNameGetSupplyEntity(string name)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_SupplyEntity>(x => x.S_Name == name);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 根据名字获取用户实体信息
        /// </summary>
        /// <param name="name">名字</param>
        /// <returns></returns>
        public UserEntity ByNameGetUserEntity(string name)
        {
            try
            {
                return this.BaseRepository().FindEntity<UserEntity>(x => x.F_RealName == name);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取供应商列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_SupplyEntity> GetSupplyList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_SupplyEntity>();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取资质有效期的供应商列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_SupplyEntity> GetEffectSupplyList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_SupplyEntity>(c=>c.S_EffectTime>=DateTime.Now);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetUserList()
        {
            try
            {
                return this.BaseRepository().FindList<UserEntity>();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取用户列表(超级管理员除外)
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserEntity> GetUserNoSysList()
        {
            try
            {
                return this.BaseRepository().FindList<UserEntity>(c => c.F_Account != "System");
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取班次列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_ClassEntity> GetClassList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_ClassEntity>();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="names">名称</param>
        /// <param name="keyValue">主键Id</param>
        /// <returns></returns>
        public bool IsName(string tables, string field, string names, string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("select * from " + tables + " where " + field + "=@F_Name ");
                var dp = new DynamicParameters(new { });
                if (!string.IsNullOrEmpty(keyValue))
                {
                    strSql.Append(" AND ID!=@keyValue");
                    dp.Add("keyValue", keyValue, DbType.String);
                }
                dp.Add("F_Name", names, DbType.String);
                int count = this.BaseRepository().FindTable(strSql.ToString(), dp).Rows.Count;
                if (count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 单号重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="orderNo">单号</param>
        /// <param name="field">字段名</param>
        /// <returns></returns>
        public bool IsOrderNo(string tables, string field, string orderNo)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("select * from " + tables + " where " + field + "=@OrderNo");
                var dp = new DynamicParameters(new { });
                dp.Add("OrderNo", orderNo, DbType.String);
                int count = this.BaseRepository().FindTable(strSql.ToString(), dp).Rows.Count;
                if (count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 排班重复验证
        /// </summary>
        /// <param name="A_F_EnCode">用户编码</param>
        /// <param name="A_ClassCode">班次</param>
        /// <param name="A_Date">日期</param>
        /// <returns></returns>
        public bool IsExistRecord(string keyValue, string A_F_EnCode, string A_ClassCode, DateTime A_Date)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT * FROM dbo.Mes_Arrange  WHERE A_F_EnCode=@A_F_EnCode AND A_ClassCode=@A_ClassCode AND A_Date BETWEEN @StartDate AND @EndDate");
                var dp = new DynamicParameters(new { });
                if (!string.IsNullOrEmpty(keyValue))
                {
                    strSql.Append(" AND ID !=@keyValue");
                    dp.Add("keyValue", keyValue, DbType.String);
                }
                dp.Add("A_F_EnCode", A_F_EnCode, DbType.String);
                dp.Add("A_ClassCode", A_ClassCode, DbType.String);
                dp.Add("StartDate", A_Date.ToString("yyyy-MM-dd") + " 00:00:00", DbType.String);
                dp.Add("EndDate", A_Date.ToString("yyyy-MM-dd") + " 23:59:59", DbType.String);
                int count = this.BaseRepository().FindTable(strSql.ToString(), dp).Rows.Count;
                if (count > 0)
                {
                    return true;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 编码重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="code">编码</param>
        /// <param name="keyValue">主键Id</param>
        /// <returns></returns>
        public bool IsCode(string tables, string field, string code, string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("select * from " + tables + " where " + field + "=@Code ");
                var dp = new DynamicParameters(new { });
                if (!string.IsNullOrEmpty(keyValue))
                {
                    strSql.Append(" AND ID !=@keyValue");
                    dp.Add("keyValue", keyValue, DbType.String);
                }

                dp.Add("Code", code, DbType.String);

                int count = this.BaseRepository().FindTable(strSql.ToString(), dp).Rows.Count;
                if (count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取配方列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_BomRecordEntity> GetBomList(string goodsCode)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_BomRecordEntity>(c => c.B_GoodsCode == goodsCode);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取原物料code
        /// </summary>
        /// <returns></returns>
        public Mes_GoodsEntity GetCode(string goodsCode)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_GoodsEntity>(c => c.G_Erpcode == goodsCode);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }


        /// <summary>
        /// 获取Mes_Convert表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_GoodsEntity GetMes_ConvertEntity(string goodsCode)
        {
            try
            {
                var entity = this.BaseRepository().FindEntity<MesDev.Mes_ConvertEntity>(c => c.C_SecCode == goodsCode|| c.C_Code==goodsCode);

                return this.BaseRepository().FindEntity<Mes_GoodsEntity>(c => c.G_Code == entity.C_Code);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取订单里的商品
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public IEnumerable<Mes_ProductOrderDetailEntity> GetOrderGoodsList(string orderNo)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_ProductOrderDetailEntity>(c => c.P_OrderNo == orderNo);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取订单商品实体
        /// </summary>
        /// <param name="goodsCode"></param>
        /// <returns></returns>
        public Mes_ProductOrderDetailEntity GetOrderGoodsEntity(string goodsCode)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ProductOrderDetailEntity>(c => c.P_GoodsCode == goodsCode);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取班组列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_TeamEntity> GetTeamList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_TeamEntity>();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetRoleList()
        {
            try
            {
                return this.BaseRepository().FindList<RoleEntity>();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取条码
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_ScanCodeEntity> GetBarCodeList()
        {
            try
            {
                return this.BaseRepository().FindList<Mes_ScanCodeEntity>("select S_Code from Mes_ScanCode ");
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 编码重复验证和供应商编码重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="code">编码</param>
        /// <param name="keyValue">主键Id</param>
        /// <returns></returns>
        public bool IsCodeAndSupplyCode(string tables, string field, string code, string field2, string code2, string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("select * from " + tables + " where " + field + "=@Code and "+field2+"=@Code2");
                var dp = new DynamicParameters(new { });
                if (!string.IsNullOrEmpty(keyValue))
                {
                    strSql.Append(" AND ID !=@keyValue");
                    dp.Add("keyValue", keyValue, DbType.String);
                }

                dp.Add("Code", code, DbType.String);
                dp.Add("Code2", code2, DbType.String);
                int count = this.BaseRepository().FindTable(strSql.ToString(), dp).Rows.Count;
                if (count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取成品批次列表
        /// </summary>
        /// <param name="goodsCode"></param>
        /// <returns></returns>
        public DataTable GetProductBatchList(string goodsCode,string stockCode)
        {
            var sql = "select I_Batch from Mes_Inventory where I_StockCode =@I_StockCode and I_GoodsCode =@I_GoodsCode ";
            var dp = new DynamicParameters(new {});
            dp.Add("@I_StockCode",stockCode,DbType.String);
            dp.Add("@I_GoodsCode", goodsCode, DbType.String);
            return this.BaseRepository().FindTable(sql, dp);
        }

        #endregion

        #region 提交数据
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        public void AuditingBill(string keyValue, string tables, string field)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("update " + tables + " set " + field + " =2 where ID='" + keyValue + "' ");
                this.BaseRepository().ExecuteBySql(strSql.ToString());
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 提交单据,撤销单据
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <param name="proc">存储过程</param>
        /// <param name="errMsg">错误信息</param>
        public int PostOrCancelOrDeleteBill(string orderNo, string proc, out string errMsg)
        {
            try
            {
                if (string.IsNullOrEmpty(proc))
                {
                    var sql = "update Mes_OrgResHead set O_Status = 3 where O_OrgResNo ='"+orderNo+"'";
                    errMsg = "";
                    var status = this.BaseRepository().ExecuteBySql(sql, null) == 1 ? 0 : 1;
                    return status;
                }
                UserInfo userinfo = LoginUserInfo.Get();
                var dp = new DynamicParameters(new {});
                dp.Add("@OrderNo", orderNo);
                dp.Add("@UserName", userinfo.realName);
                dp.Add("@errcode", "", DbType.Int32, ParameterDirection.Output);
                dp.Add("@errtxt", "", DbType.String, ParameterDirection.Output);
                this.BaseRepository().ExecuteByProc(proc, dp);
                errMsg = dp.Get<string>("@errtxt"); //存储过程返回的错误消息
                return dp.Get<int>("@errcode"); //返回的错误代码 0：成功
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 提交单据,撤销单据,删除单据(入库单)
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <param name="proc">存储过程</param>
        /// <param name="errMsg">错误信息</param>
        public int PostOrCancelOrDeleteMaterInBill(string orderNo, string proc, out string errMsg)
        {
            try
            {
                UserInfo userinfo = LoginUserInfo.Get();
                var dp = new DynamicParameters(new { });
                dp.Add("@OrderNo", orderNo);
                dp.Add("@UserName", userinfo.realName);
                dp.Add("@errcode", "", DbType.Int32, ParameterDirection.Output);
                dp.Add("@errtxt", "", DbType.String, ParameterDirection.Output);
                this.BaseRepository().ExecuteByProc(proc, dp);
                errMsg = dp.Get<string>("@errtxt");//存储过程返回的错误消息
                return dp.Get<int>("@errcode");//返回的错误代码 0：成功
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取成品列表
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public DataTable GetProductList(string stockCode)
        {
            var sql = "select distinct I_GoodsCode,I_GoodsName from Mes_Inventory where I_StockCode =@stockCode";
            var dp = new DynamicParameters(new { });
            dp.Add("@stockCode",stockCode,DbType.String);
            return this.BaseRepository().FindTable(sql, dp);
        }
        #endregion
    }
}
