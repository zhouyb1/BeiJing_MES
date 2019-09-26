using System.Linq;
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
    /// 日 期：2019-01-08 17:17
    /// 描 述：库存查询
    /// </summary>
    public partial class InventorySeachService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
               sum(t.I_Qty) as I_Qty,
					t.I_StockCode,
					t.I_GoodsName,
					t.I_StockName,
					t.I_GoodsCode,
					t.I_Unit,
                    (select G_Super from Mes_Goods a where a.G_Code=t.I_GoodsCode ) as G_Super,
					(select G_Lower from Mes_Goods a where a.G_Code=t.I_GoodsCode ) as G_Lower,
                     case when sum(t.I_Qty)>(select G_Lower from Mes_Goods a where a.G_Code=t.I_GoodsCode ) and sum(t.I_Qty)<(select G_Super from Mes_Goods a where a.G_Code=t.I_GoodsCode ) then '正常' 
					 when sum(t.I_Qty)<(select G_Lower from Mes_Goods a where a.G_Code=t.I_GoodsCode ) then '低于下限预警' 
					 when  sum(t.I_Qty)>(select G_Super from Mes_Goods a where a.G_Code=t.I_GoodsCode ) then  '高于上限预警' else '无' end as G_State
                ");
                strSql.Append("  FROM Mes_Inventory  t   group by t.I_StockCode,t.I_GoodsName,t.I_StockName,t.I_GoodsCode,t.I_Unit ");
                strSql.Append("  having 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["I_StockCode"].IsEmpty())
                {
                    dp.Add("I_StockCode", "%" + queryParam["I_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_StockCode Like @I_StockCode ");
                }
                if (!queryParam["I_StockName"].IsEmpty())
                {
                    dp.Add("I_StockName", "%" + queryParam["I_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_StockName Like @I_StockName ");
                }
                if (!queryParam["I_GoodsCode"].IsEmpty())
                {
                    dp.Add("I_GoodsCode", "%" + queryParam["I_GoodsCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_GoodsCode Like @I_GoodsCode ");
                }
                if (!queryParam["I_GoodsName"].IsEmpty())
                {
                    dp.Add("I_GoodsName", "%" + queryParam["I_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_GoodsName Like @I_GoodsName ");
                }
                if (!queryParam["I_Batch"].IsEmpty())
                {
                    dp.Add("I_Batch", "%" + queryParam["I_Batch"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.I_Batch Like @I_Batch ");
                }
                return this.BaseRepository().FindList<Mes_InventoryEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取物料领用列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<PickOrUsedModel> GetPickPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                var queryParam = queryJson.ToJObject();
                strSql.AppendFormat(@"
               --领料到周转仓
              select a.C_GoodsCode GoodsCode,SUM(a.C_Qty) as Qty,a.C_Batch Batch,a.C_GoodsName GoodsName from dbo.Mes_CollarDetail as a left join Mes_CollarHead as b on a.C_CollarNo = b.C_CollarNo where  M_UploadDate > @startTime and M_UploadDate < @endTime {0}  group by a.C_GoodsCode,a.C_Batch,a.C_GoodsName  
              UNION ALL
              --调拨到周转仓
              select a.R_GoodsCode,SUM(a.R_Qty) as qty,a.R_Batch,a.R_GoodsName from dbo.Mes_RequistDetail as a left join Mes_RequistHead as b on a.R_RequistNo = b.R_RequistNo where  R_UpdateDate >@startTime and R_UpdateDate < @endTime  {1} group by a.R_GoodsCode,a.R_Batch,a.R_GoodsName
                UNION ALL
              --车间退料到周转仓
              select a.O_GoodsCode,SUM(a.O_Qty) as qty,a.O_Batch,a.O_GoodsName from dbo.Mes_OutWorkShopDetail as a left join Mes_OutWorkShopHead as b on a.O_OutNo = b.O_OutNo where  O_Kind = '2' and O_UploadDate > @startTime and O_UploadDate < @endTime {2}  group by a.O_GoodsCode,a.O_Batch,a.O_GoodsName
                UNION ALL
              --车间入库到周转仓
              select a.I_GoodsCode,SUM(a.I_Qty) as qty,a.I_Batch,a.I_GoodsName from dbo.Mes_InWorkShopDetail as a left join dbo.Mes_InWorkShopHead as b on a.I_InNo = b.I_InNo where  I_UploadDate > @startTime and I_UploadDate < @endTime {3}  group by a.I_GoodsCode,a.I_Batch,a.I_GoodsName
                            ", queryParam["StockCode"].IsEmpty() ? "" : "and b.C_StockToCode = " + queryParam["StockCode"].ToString(), queryParam["StockCode"].IsEmpty() ? "" : "and b.R_StockToCode = " + queryParam["StockCode"].ToString(), queryParam["StockCode"].IsEmpty() ? "" : "and b.O_StockCode = " + queryParam["StockCode"].ToString(), queryParam["StockCode"].IsEmpty() ? "" : "and b.I_StockCode = " + queryParam["StockCode"].ToString());
                
                // 虚拟参数
                var dp = new DynamicParameters(new { });
              
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                }
                return this.BaseRepository().FindList<PickOrUsedModel>(strSql.ToString(), dp, pagination);
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
        /// 获取物料使用列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<PickOrUsedModel> GetUsedPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var strSql = new StringBuilder();
                strSql.AppendFormat(@"
                  --周转仓出库到车间
                  select a.O_GoodsCode GoodsCode,SUM(a.O_Qty) as Qty,a.O_Batch Batch,a.O_GoodsName GoodsName from dbo.Mes_OutWorkShopDetail as a left join Mes_OutWorkShopHead as b on a.O_OutNo = b.O_OutNo where  O_Kind = '1' and O_UploadDate > @startTime and O_UploadDate < @endTime {0}  group by a.O_GoodsCode,a.O_Batch,a.O_GoodsName
                  UNION ALL
                  --周转仓报废
                  select a.S_GoodsCode,SUM(a.S_Qty) as qty,a.S_Batch,a.S_GoodsName from dbo.Mes_ScrapDetail as a left join Mes_ScrapHead as b on a.S_ScrapNo = b.S_ScrapNo where  S_UploadDate > @startTime and S_UploadDate < @endTime {1}  group by a.S_GoodsCode,a.S_Batch,a.S_GoodsName
                            ", queryParam["StockCode"].IsEmpty() ? "" : "and b.O_StockCode =" + queryParam["StockCode"].ToString(), queryParam["StockCode"].IsEmpty() ? "" : "and b.S_StockCode = " + queryParam["StockCode"].ToString());
               
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                }
                return this.BaseRepository().FindList<PickOrUsedModel>(strSql.ToString(), dp, pagination);
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
        /// 获取物料价值查询列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<GoodsPriceModel> GetPricePageList(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var strSql = new StringBuilder();
                strSql.Append(@"
                  SELECT    ord.O_SecGoodsCode ,
                            ord.O_SecGoodsName ,
                            ord.O_SecPrice ,
                            O_Batch
                  FROM      dbo.Mes_OrgResDetail ord
                            LEFT JOIN dbo.Mes_OrgResHead h ON ord.O_OrgResNo = h.O_OrgResNo
                  WHERE     h.O_OrderDate > @startTime
                            AND h.O_OrderDate < @endTime
                            ");

                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["OrderNo"].IsEmpty())
                {
                    dp.Add("OrderNo", queryParam["OrderNo"].ToString(), DbType.String);
                    strSql.Append("AND h.O_OrderNo = @OrderNo");
                }
                if (!queryParam["SecGoodsCode"].IsEmpty())
                {
                    dp.Add("SecGoodsCode",queryParam["SecGoodsCode"].ToString(), DbType.String);
                    strSql.Append("AND O_SecGoodsCode=@SecGoodsCode");
                }
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                }
                return this.BaseRepository().FindList<GoodsPriceModel>(strSql.ToString(), dp, pagination);
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
        /// 获取Mes_Inventory表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_InventoryEntity GetMes_InventoryEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_InventoryEntity>(keyValue);
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
        /// 获取Mes_Inventory表实体数据 根据商品编码和仓库编码以及批次
        /// </summary>
        /// <param name="goodsCode">商品编码</param>
        /// <param name="stockCode">仓库编码</param>
        /// <param name="batch">批次</param>
        /// <returns></returns>
        public Mes_InventoryEntity GetEntityBy(string goodsCode, string stockCode, string batch)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_InventoryEntity>(c=>c.I_GoodsCode==goodsCode&&c.I_StockCode==stockCode&&c.I_Batch==batch);
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
        /// 根据仓库编码和商品编码获取列表
        /// </summary>
        /// <param name="stockCode">仓库编码</param>
        /// <param name="goodsCode">物料编码</param>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryEntity> GetListByStockAndCode(string stockCode, string goodsCode)
        {
            try
            {
                return
                    this.BaseRepository()
                        .FindList<Mes_InventoryEntity>(c => c.I_StockCode == stockCode && c.I_GoodsCode == goodsCode);
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
        ///根据goodsCode、批次获取Entity
        /// </summary>
        /// <param name="goodsCode"></param>
        /// <returns></returns>
        public Mes_InventoryEntity GetListByParams(string goodsCode,string batch)
        {
            try
            {
                return
                    this.BaseRepository()
                        .FindEntity<Mes_InventoryEntity>(c => c.I_GoodsCode == goodsCode && c.I_Batch == batch);
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
        /// 获取库存物料
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryEntity> GetInventoryList(Pagination pagination, string queryJson, string I_GoodsName, string I_StockName, string I_Unit, string I_Batch)
        {
            try
            {
                var strSql = new StringBuilder();
                //                strSql.Append(@"SELECT  m.ID ,
                //                                        m.P_GoodsCode ,
                //                                        m.P_GoodsName ,
                //                                        s.I_Batch P_Batch ,
                //                                        m.P_Unit ,
                //                                        s.I_Qty P_Qty ,
                //                                        m.P_OrderNo ,
                //                                        m.P_OrderDate ,
                //                                        g.G_Price P_Price
                //                                FROM    dbo.Mes_Inventory s
                //                                        LEFT JOIN dbo.Mes_Goods g ON g.G_Code = s.I_GoodsCode
                //                                        RIGHT JOIN dbo.Mes_Mater m ON m.P_GoodsCode = s.I_GoodsCode
                //                                WHERE   1 = 1 ");

                strSql.Append(@"SELECT  
                                t.I_Qty,
					            t.I_StockCode,
					            t.I_GoodsName,
					            t.I_StockName,
					            t.I_GoodsCode,
					            t.I_Unit,
					            t.I_Batch,
					            t.I_Remark
                                FROM dbo.Mes_Inventory t where 1=1");

                var queryParam = queryJson.ToJObject();
                 //虚拟参数
                var dp = new DynamicParameters(new { });
                if (!I_Batch.IsEmpty())
                {
                    dp.Add("I_Batch", "%" + I_Batch + "%", DbType.String);
                    strSql.Append(" AND t.I_Batch like @I_Batch ");
                }
                if (!I_GoodsName.IsEmpty())
                {
                    dp.Add("I_GoodsName", I_GoodsName, DbType.String);
                    strSql.Append(" AND t.I_GoodsName= @I_GoodsName ");
                }
                if (!I_StockName.IsEmpty())
                {
                    dp.Add("I_StockName", I_StockName, DbType.String);
                    strSql.Append(" AND t.I_StockName=@I_StockName ");
                }
                if (!I_Unit.IsEmpty())
                {
                    dp.Add("I_Unit", I_Unit, DbType.String);
                    strSql.Append(" AND t.I_Unit=@I_Unit ");
                }
                return this.BaseRepository().FindList<Mes_InventoryEntity>(strSql.ToString(), dp, pagination);
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
            try
            {
                this.BaseRepository().Delete<Mes_InventoryEntity>(t=>t.ID == keyValue);
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
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, Mes_InventoryEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
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
