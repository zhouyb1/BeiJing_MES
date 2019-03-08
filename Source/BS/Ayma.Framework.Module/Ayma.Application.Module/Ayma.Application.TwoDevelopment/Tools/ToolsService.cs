using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Ayma.Application.TwoDevelopment.MesDev;

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
        /// 根据仓库编码获取仓库实体信息
        /// </summary>
        /// <param name="code">物料编码</param>
        /// <returns></returns>
        public Mes_StockEntity ByCodeGetStockEntity(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_StockEntity>(x => x.S_Code == code);
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
        /// 获取仓库列表
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
        public IEnumerable<Mes_ProceEntity> GetProceList(string parentId)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_ProceEntity>(t=>t.P_ParentId==parentId);
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
        /// 根据物料编码获取物料实体信息
        /// </summary>
        /// <param name="code">物料编码</param>
        /// <returns></returns>
        public Mes_GoodsEntity ByCodeGetGoodsEntity(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_GoodsEntity>(x=>x.G_Code==code);
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
        public IEnumerable<Mes_ProceEntity> GetProceListBy(string code)
        {
            try
            {
                var entity= this.BaseRepository().FindEntity<Mes_ProceEntity>(x => x.P_RecordCode == code);
                var list = this.BaseRepository().FindList<Mes_ProceEntity>(t => t.P_ParentId == entity.ID || t.ID == entity.ID);
                return list;
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
        /// 根据门编码获取门实体信息
        /// </summary>
        /// <param name="code">门编码</param>
        /// <returns></returns>
        public Mes_DoorEntity ByCodeGetDoorEntity(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_DoorEntity>(x=>x.D_Code==code);
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
                return this.BaseRepository().FindEntity<Mes_SupplyEntity>(x=>x.S_Code==code);
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
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="names">名称</param>
        /// <returns></returns>
        public bool IsName(string tables, string field, string names)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("select * from " + tables + " where "+field+"=@F_Name");
                var dp = new DynamicParameters(new { });
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
        public bool IsOrderNo(string tables,string field, string orderNo)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("select * from " + tables + " where "+field+"=@OrderNo");
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
        /// 编码重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public bool IsCode(string tables,string field, string code)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("select * from " + tables + " where "+field+"=@Code");
                var dp = new DynamicParameters(new { });
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

        #endregion
        
        #region 提交数据
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="tables">表名</param>
        /// <param name="field">字段名</param>
        public void AuditingBill(string keyValue,string tables,string field)
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
        #endregion
    }
}
