using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-13 11:57
    /// 描 述：车间入库到线边仓
    /// </summary>
    public partial class InWorkShopManagerService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_InWorkShopHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.I_Status,
                t.I_InNo,
                t.I_StockCode,
                t.I_StockName,
                s.W_Name I_WorkShop ,
                t.I_OrderNo,
                t.I_OrderDate,
                t.I_Remark,
                t.I_CreateBy,
                t.I_CreateDate
                ");
                strSql.Append("  FROM Mes_InWorkShopHead t left join Mes_WorkShop s on(t.I_WorkShop=s.W_Code)");
                strSql.Append("  WHERE t.I_Status in (1,2) ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.I_CreateDate >= @startTime AND t.I_CreateDate <= @endTime ) ");
                }
                if (!queryParam["I_InNo"].IsEmpty())
                {
                    dp.Add("I_InNo", "%" + queryParam["I_InNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_InNo Like @I_InNo ");
                }
                if (!queryParam["I_OrderNo"].IsEmpty())
                {
                    dp.Add("I_OrderNo", "%" + queryParam["I_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_OrderNo Like @I_OrderNo ");
                }
                if (!queryParam["I_StockName"].IsEmpty())
                {
                    dp.Add("I_StockName", "%" + queryParam["I_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_StockName Like @I_StockName ");
                }
                if (!queryParam["I_Status"].IsEmpty())
                {
                    dp.Add("I_Status", "%" + queryParam["I_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_Status Like @I_Status ");
                }
                if (!queryParam["I_WorkShop"].IsEmpty())
                {
                    dp.Add("I_WorkShop", "%" + queryParam["I_WorkShop"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.W_Name Like @I_WorkShop ");
                }
                return this.BaseRepository().FindList<Mes_InWorkShopHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 车间入库到线边仓查询
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_InWorkShopHeadEntity> GetSearchIndex(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.I_Status,
                t.I_InNo,
                t.I_StockCode,
                t.I_StockName,
                s.W_Name I_WorkShop ,
                t.I_OrderNo,
                t.I_OrderDate,
                t.I_Remark,
                t.I_CreateBy,
                t.I_CreateDate
                ");
                strSql.Append("  FROM Mes_InWorkShopHead t left join Mes_WorkShop s on(t.I_WorkShop=s.W_Code)");
                strSql.Append("  WHERE 1=1 and t.I_Status=3");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.I_CreateDate >= @startTime AND t.I_CreateDate <= @endTime ) ");
                }
                if (!queryParam["I_InNo"].IsEmpty())
                {
                    dp.Add("I_InNo", "%" + queryParam["I_InNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_InNo Like @I_InNo ");
                }
                if (!queryParam["I_OrderNo"].IsEmpty())
                {
                    dp.Add("I_OrderNo", "%" + queryParam["I_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_OrderNo Like @I_OrderNo ");
                }
                if (!queryParam["I_StockName"].IsEmpty())
                {
                    dp.Add("I_StockName", "%" + queryParam["I_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_StockName Like @I_StockName ");
                }
                if (!queryParam["I_Status"].IsEmpty())
                {
                    dp.Add("I_Status", "%" + queryParam["I_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_Status Like @I_Status ");
                }
                if (!queryParam["I_WorkShop"].IsEmpty())
                {
                    dp.Add("I_WorkShop", "%" + queryParam["I_WorkShop"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.W_Name Like @I_WorkShop ");
                }
                return this.BaseRepository().FindList<Mes_InWorkShopHeadEntity>(strSql.ToString(), dp, pagination);

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
        /// 获取Mes_InWorkShopHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_InWorkShopHeadEntity GetMes_InWorkShopHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_InWorkShopHeadEntity>(keyValue);
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
        /// 获取Mes_InWorkShopDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_InWorkShopDetailEntity> GetMes_InWorkShopDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_InWorkShopDetailEntity>(t=>t.I_InNo == keyValue);
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
        /// 获取仓库物料列表
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryEntity> GetInventoryMaterList(Pagination paginationobj, string stockCode)
        {
            try
            {
                //return this.BaseRepository().FindList<Mes_InventoryEntity>(c => c.I_StockCode == stockCode, paginationobj);
                var strSql = new StringBuilder();
                strSql.Append(@"select m.*,m.I_Qty as Qty,g.G_Price as I_Price from Mes_Inventory m left join Mes_Goods g on m.I_GoodsCode = g.G_Code where m.I_StockCode =@stockCode");
                var dp = new DynamicParameters(new {});
                dp.Add("@stockCode", stockCode,DbType.String);
                
               return this.BaseRepository().FindList<Mes_InventoryEntity>(strSql.ToString(),dp, paginationobj);
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
        /// 获取物料列表(半成品和成品)
        /// </summary>
        /// <param name="paginationobj">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetGoodsList(Pagination paginationobj, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT  G_Code I_GoodsCode,
                                    G_Name I_GoodsName,
                                    G_Unit I_Unit,
                                    G_Price I_Price
                            FROM    dbo.Mes_Goods
                            WHERE   G_Kind !=1 ");
                var dp = new DynamicParameters(new {});
                var queryParam = queryJson.ToJObject();
                if (!queryParam["keyword"].IsEmpty())
                {
                    dp.Add("keyword", "%" + queryParam["keyword"].ToString() + "%", DbType.String);
                    strSql.Append(" AND (G_Code LIKE @keyword OR G_Name LIKE @keyword) ");
                }
               return this.BaseRepository().FindTable(strSql.ToString(),dp, paginationobj);
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
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                var mes_InWorkShopHeadEntity = GetMes_InWorkShopHeadEntity(keyValue); 
                db.Delete<Mes_InWorkShopHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_InWorkShopDetailEntity>(t=>t.I_InNo == mes_InWorkShopHeadEntity.I_InNo);
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, Mes_InWorkShopHeadEntity entity, List<Mes_InWorkShopDetailEntity> mes_InWorkShopDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_InWorkShopHeadEntityTmp = GetMes_InWorkShopHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_InWorkShopDetailEntity>(t => t.I_InNo == mes_InWorkShopHeadEntityTmp.I_InNo);
                    foreach (var item in mes_InWorkShopDetailList)
                    {
                        item.Create();
                        item.I_InNo = mes_InWorkShopHeadEntityTmp.I_InNo;
                    }
                    db.Insert(mes_InWorkShopDetailList);
                }
                else
                {
                    var dp = new DynamicParameters(new { });
                    dp.Add("@BillType", "强制使用记录单");
                    dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                    db.ExecuteByProc("sp_GetDoucno", dp);
                    var billNo = dp.Get<string>("@Doucno");//存储过程返回单号
                    entity.I_InNo = billNo;
                    entity.Create();
                    db.Insert(entity);
                    foreach (var item in mes_InWorkShopDetailList)
                    {
                        item.Create();
                        item.I_InNo = entity.I_InNo;
                    }
                    db.Insert(mes_InWorkShopDetailList);
                }
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
