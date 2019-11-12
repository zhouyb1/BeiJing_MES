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
    /// 日 期：2019-11-12 09:16
    /// 描 述：库存快照
    /// </summary>
    public partial class StockHistoryService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryLSEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"
              SELECT SUM(t.I_Qty) AS I_Qty,
       t.I_StockCode,
       t.I_GoodsName,
       t.I_StockName,
       t.I_GoodsCode,
       t.I_Unit,
       (
           SELECT G_Price FROM Mes_Goods m WHERE G_Code = t.I_GoodsCode
       ) AS Price,
       (
           SELECT G_Price FROM Mes_Goods m WHERE G_Code = t.I_GoodsCode
       ) * SUM(t.I_Qty) AS AllMoney,
       (
           SELECT G_Super FROM Mes_Goods a WHERE a.G_Code = t.I_GoodsCode
       ) AS G_Super,
       (
           SELECT G_Lower FROM Mes_Goods a WHERE a.G_Code = t.I_GoodsCode
       ) AS G_Lower,
       CASE
           WHEN SUM(t.I_Qty) >=
           (
               SELECT G_Lower FROM Mes_Goods a WHERE a.G_Code = t.I_GoodsCode
           )
                AND SUM(t.I_Qty) <=
                (
                    SELECT G_Super FROM Mes_Goods a WHERE a.G_Code = t.I_GoodsCode
                ) THEN
               '正常'
           WHEN SUM(t.I_Qty) <
           (
               SELECT G_Lower FROM Mes_Goods a WHERE a.G_Code = t.I_GoodsCode
           ) THEN
               '库存不足'
           WHEN SUM(t.I_Qty) >
           (
               SELECT G_Super FROM Mes_Goods a WHERE a.G_Code = t.I_GoodsCode
           ) THEN
               '高于上限预警'
           ELSE
               '无'
       END AS G_State
FROM Mes_InventoryLS t {0}
GROUP BY t.I_StockCode,
         t.I_GoodsName,
         t.I_StockName,
         t.I_GoodsCode,
         t.I_Unit
HAVING SUM(t.I_Qty) != 0
                ");

                var queryParam = queryJson.ToJObject();


                var cmd = new StringBuilder();
                cmd.Append("WHERE 1=1");

                // 虚拟参数
                var dp = new DynamicParameters(new { });

                if (!queryParam["I_Date"].IsEmpty())
                {
                    dp.Add("I_Date", queryParam["I_Date"].ToString(), DbType.Date);
                    cmd.Append(" AND t.I_Date = @I_Date ");
                }
                else
                {
                    cmd.Append(" AND t.I_Date ='1900-01-01' ");
                }

                if (!queryParam["I_StockCode"].IsEmpty())
                {
                    dp.Add("I_StockCode", "%" + queryParam["I_StockCode"].ToString() + "%", DbType.String);
                    cmd.Append(" AND t.I_StockCode Like @I_StockCode ");
                }

                if (!queryParam["I_StockName"].IsEmpty())
                {
                    dp.Add("I_StockName", "%" + queryParam["I_StockName"].ToString() + "%", DbType.String);
                    cmd.Append(" AND t.I_StockName Like @I_StockName ");
                }

                if (!queryParam["I_GoodsCode"].IsEmpty())
                {
                    dp.Add("I_GoodsCode", "%" + queryParam["I_GoodsCode"].ToString() + "%", DbType.String);
                    cmd.Append(" AND t.I_GoodsCode Like @I_GoodsCode ");
                }

                if (!queryParam["I_GoodsName"].IsEmpty())
                {
                    dp.Add("I_GoodsName", "%" + queryParam["I_GoodsName"].ToString() + "%", DbType.String);
                    cmd.Append(" AND t.I_GoodsName Like @I_GoodsName ");
                }

                if (!queryParam["I_Batch"].IsEmpty())
                {
                    dp.Add("I_Batch", "%" + queryParam["I_Batch"].ToString() + "%", DbType.String);
                    cmd.Append(" AND t.I_Batch Like @I_Batch ");
                }

                return this.BaseRepository().FindList<Mes_InventoryLSEntity>(string.Format(strSql.ToString(),cmd.ToString()), dp, pagination);
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
        public IEnumerable<Mes_InventoryLSEntity> GetInventoryList(Pagination pagination, string queryJson,string I_Date, string I_GoodsName, string I_StockName, string I_Unit, string I_Batch)
        {
            try
            {
                var strSql = new StringBuilder();

                strSql.Append(@"SELECT  
                                t.I_Qty,
					            t.I_StockCode,
					            t.I_GoodsName,
					            t.I_StockName,
					            t.I_GoodsCode,
					            t.I_Unit,
					            t.I_Batch,
					            t.I_Remark,
	                            (select G_Price from Mes_Goods m where G_Code=t.I_GoodsCode)* t.I_Qty as OneMoney,
								(select G_Price from Mes_Goods m where G_Code=t.I_GoodsCode) as Price
                                FROM dbo.Mes_InventoryLS t where t.I_Qty!=0");

                var queryParam = queryJson.ToJObject();
                //虚拟参数
                var dp = new DynamicParameters(new { });

                if (!I_Date.IsEmpty())
                {
                    dp.Add("I_Date", I_Date, DbType.Date);
                    strSql.Append(" AND t.I_Date = @I_Date ");
                }
                else
                {
                    strSql.Append(" AND t.I_Date ='1900-01-01' ");
                }

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
                return this.BaseRepository().FindList<Mes_InventoryLSEntity>(strSql.ToString(), dp, pagination);
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
