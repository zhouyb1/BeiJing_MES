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
    /// 日 期：2019-09-25 14:42
    /// 描 述：供应商进货数据汇总
    /// </summary>
    public partial class SupplyGoodsCountRepService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                h.M_SupplyCode ,
                h.M_SupplyName ,
                CONVERT(DECIMAL(16,2),SUM(m.M_Qty)) In_Qty ,
                CONVERT(DECIMAL(16,2),SUM(m.M_Qty * M_Price)) In_Amount ,
                CONVERT(DECIMAL(16,2),SUM(m.M_Qty * M_Price)/SUM(m.M_Qty)) Avg_Price
                ");
                strSql.Append("  FROM Mes_MaterInHead h ");
                strSql.Append("  LEFT JOIN Mes_MaterInDetail m ON m.M_MaterInNo = h.M_MaterInNo ");
                strSql.Append("  WHERE 1=1 AND h.M_Status =3 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( h.M_CreateDate >= @startTime AND h.M_CreateDate <= @endTime ) ");
                }
                if (!queryParam["M_SupplyCode"].IsEmpty())
                {
                    dp.Add("M_SupplyCode", "%" + queryParam["M_SupplyCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND h.M_SupplyCode Like @M_SupplyCode ");
                }

                strSql.Append(" GROUP BY h.M_SupplyCode,h.M_SupplyName");
                return this.BaseRepository().FindTable(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_MaterInHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_MaterInHeadEntity GetMes_MaterInHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_MaterInHeadEntity>(keyValue);
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
        /// 供应商进货数据明细数据
        /// </summary>
        /// <param name="supplyCode"></param>
        /// <returns></returns>
        public DataTable GetSupplyGoodsDetail(string supplyCode, string queryJson)
        {
            string sql = @"SELECT   h.M_MaterInNo ,
                                    m.M_SupplyCode ,
                                    m.M_SupplyName ,
                                    m.M_GoodsName ,
                                    m.M_GoodsCode ,
                                    h.M_StockName ,
                                    h.M_StockCode ,
                                    m.M_Price ,
                                    m.M_Qty ,
                                    m.M_Unit ,
                                    m.M_Tax,
                                    m.M_Qty*m.M_Price Amount,
                                    h.M_CreateDate ,
                                    dbo.GetUserNameById(h.M_CreateBy) M_CreateBy
                            FROM    dbo.Mes_MaterInHead h
                                    LEFT JOIN dbo.Mes_MaterInDetail m ON m.M_MaterInNo = h.M_MaterInNo  WHERE h.M_SupplyCode  =@supplyCode AND h.M_Status =3  ";
            var dp = new DynamicParameters(new { });
            var queryParam = queryJson.ToJObject();
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                sql += " AND ( h.M_CreateDate >= @startTime AND h.M_CreateDate <= @endTime ) ";
                sql += "ORDER BY h.M_CreateDate desc";
            }
            dp.Add("@supplyCode",supplyCode,DbType.String);
            return this.BaseRepository().FindTable(sql, dp);
        }

        #endregion

    }
}
