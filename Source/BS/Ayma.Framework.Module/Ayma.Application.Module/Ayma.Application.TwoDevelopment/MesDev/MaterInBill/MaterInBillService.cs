using System.Data.SqlClient;
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
    /// 日 期：2019-01-08 14:58
    /// 描 述：入库单制作
    /// </summary>
    public partial class MaterInBillService : RepositoryFactory
    {
        #region 获取数据
       
        /// <summary>
        /// 获取成品列表数据(现用)
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <param name="keyword">编码/名称搜索</param>
        /// <returns></returns>
        public IEnumerable<Mes_GoodsEntity> GetProductGoodsList(Pagination pagination, string queryJson, string keyword)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@" SELECT [ID]
                                  ,[G_Code]
                                  ,[G_Name]
                                  ,[G_SupplyCode]
                                  ,[G_SupplyName]
                                  ,[G_Kind]
                                  ,[G_Period]
                                  ,[G_Price]
                                  ,[G_Unit]
                                  ,[G_UnitWeight]
                                  ,[G_Super]
                                  ,[G_Lower]
                                  ,dbo.GetUserNameById([G_CreateBy])G_CreateBy
                                  ,[G_CreateDate]
                                  ,[G_UpdateBy]
                                  ,[G_UpdateDate]
                                  ,[G_Remark]
                                  ,[G_Erpcode]
                                  ,[G_TKind]
                                  ,[G_UnitQty]
                                  ,[G_Self]
                                  ,[G_Online]
                                  ,[G_Prepareday]
                                  ,[G_Otax]
                                  ,[G_Itax]
                              FROM [dbo].[Mes_Goods] t ");
                strSql.Append(" where t.G_Kind=3 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!keyword.IsEmpty())
                {
                    dp.Add("keyword", "%" + keyword + "%", DbType.String);
                    strSql.Append(" AND t.G_Code+t.G_Name like @keyword ");
                }
                return this.BaseRepository().FindList<Mes_GoodsEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取非成品成品列表数据(非成品:原材料/半成品)
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <param name="keyword">编码/名称搜索</param>
        /// <returns></returns>
        public DataTable GetGoodsList(Pagination pagination, string queryJson, string keyword)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@" select 
                                t.ID,
                                t.P_SupplyCode,
                                t.P_SupplyName,
                                t.P_GoodsCode,
                                t.P_GoodsName,
                                t.P_InPrice,
                                t.P_Itax,
                                t.P_StartDate,  
                                t.P_EndDate,
								t.P_TaxPrice,
                                m.G_Unit,
                                m.G_Kind,
                                m.G_Period,
								m.G_UnitQty,
								m.G_Unit2,
                                m.G_StockCode as G_StockCode,
                              (select S_Name from Mes_Stock where S_Code=m.G_StockCode) as G_StockName
                                from Mes_InPrice t left join Mes_Goods m on(t.P_GoodsCode=m.G_Code) ");
                strSql.Append(" where G_Kind=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
            
                if (!keyword.IsEmpty())
                {
                    dp.Add("keyword", "%" + keyword + "%", DbType.String);
                    strSql.Append(" AND m.G_Code+m.G_Name like @keyword ");
                }
                if (!queryParam["G_SupplyCode"].IsEmpty())
                {
                    dp.Add("G_SupplyCode", "%" + queryParam["G_SupplyCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_SupplyCode Like @G_SupplyCode ");
                }
                return this.BaseRepository().FindTable(strSql.ToString(), dp, pagination);
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
        /// 获取成品入库已提交的成品入库
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_MaterInHeadEntity> GetPostProductPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                distinct
                t.ID,
                t.M_MaterInNo,
                t.M_StockCode,
                t.M_StockName,
                t.M_OrderNo,
                t.M_OrderDate,
                t.M_Status,
                dbo.GetUserNameById(t.M_CreateBy) M_CreateBy,
                t.M_CreateDate,
                dbo.GetUserNameById(t.M_UpdateBy) M_UpdateBy,
                t.M_UpdateDate,
                t.M_Remark,
                t.M_DeleteBy,
                t.M_DeleteDate,
                dbo.GetUserNameById(t.M_UploadBy) M_UploadBy,
                t.M_UploadDate
                ");
                strSql.Append("  FROM Mes_MaterInHead t left join Mes_MaterInDetail s on(t.M_MaterInNo=s.M_MaterInNo)");
                strSql.Append("  WHERE t.M_Status = 3 and t.M_OrderKind=1  ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.M_CreateDate >= @startTime AND t.M_CreateDate <= @endTime ) ");
                }
                if (!queryParam["M_GoodsName"].IsEmpty())
                {
                    dp.Add("M_GoodsName", "%" + queryParam["M_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.M_GoodsName Like @M_GoodsName ");
                }
                if (!queryParam["M_MaterInNo"].IsEmpty())
                {
                    dp.Add("M_MaterInNo", "%" + queryParam["M_MaterInNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_MaterInNo Like @M_MaterInNo ");
                }
                if (!queryParam["M_OrderNo"].IsEmpty())
                {
                    dp.Add("M_OrderNo", "%" + queryParam["M_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_OrderNo Like @M_OrderNo ");
                }
                if (!queryParam["M_StockCode"].IsEmpty())
                {
                    dp.Add("M_StockCode", "%" + queryParam["M_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_StockCode Like @M_StockCode ");
                }
                if (!queryParam["M_StockName"].IsEmpty())
                {
                    dp.Add("M_StockName", "%" + queryParam["M_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_StockName Like @M_StockName ");
                }
                return this.BaseRepository().FindList<Mes_MaterInHeadEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取成品入库列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_MaterInHeadEntity> GetProductPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.M_MaterInNo,
                t.M_StockCode,
                t.M_StockName,
                t.M_OrderNo,
                t.M_OrderDate,
                t.M_Status,
                dbo.GetUserNameById(t.M_CreateBy) M_CreateBy,
                t.M_CreateDate,
                dbo.GetUserNameById(t.M_UpdateBy) M_CreateBy,
                t.M_UpdateDate,
                t.M_Remark,
                t.M_DeleteBy,
                t.M_DeleteDate,
                dbo.GetUserNameById(t.M_UploadBy) M_UploadBy,
                t.M_UploadDate
                ");
                strSql.Append("  FROM Mes_MaterInHead t ");
                strSql.Append("  WHERE t.M_Status in (1,2) and t.M_OrderKind=1  ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.M_CreateDate >= @startTime AND t.M_CreateDate <= @endTime ) ");
                }
                if (!queryParam["M_MaterInNo"].IsEmpty())
                {
                    dp.Add("M_MaterInNo", "%" + queryParam["M_MaterInNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_MaterInNo Like @M_MaterInNo ");
                }
                if (!queryParam["M_OrderNo"].IsEmpty())
                {
                    dp.Add("M_OrderNo", "%" + queryParam["M_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_OrderNo Like @M_OrderNo ");
                }
                if (!queryParam["M_StockCode"].IsEmpty())
                {
                    dp.Add("M_StockCode", "%" + queryParam["M_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_StockCode Like @M_StockCode ");
                }
                if (!queryParam["M_StockName"].IsEmpty())
                {
                    dp.Add("M_StockName", "%" + queryParam["M_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_StockName Like @M_StockName ");
                }
                if (!queryParam["M_Status"].IsEmpty())
                {
                    dp.Add("M_Status", "%" + queryParam["M_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_Status like @M_Status ");
                }
                return this.BaseRepository().FindList<Mes_MaterInHeadEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取已提交单据列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_MaterInHeadEntity> GetPostPageList(Pagination pagination, string queryJson, string M_MaterInNo)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                distinct
                t.ID,
                t.M_MaterInNo,
                t.M_StockCode,
                t.M_StockName,
                t.M_OrderNo,
                t.M_SupplyName,
                t.M_OrderDate,
                t.M_Status,
                dbo.GetUserNameById(t.M_CreateBy) M_CreateBy,
                t.M_CreateDate,
                dbo.GetUserNameById(t.M_UpdateBy) M_UpdateBy,
                t.M_UpdateDate,
                t.M_Remark,
                t.M_DeleteBy,
                t.M_DeleteDate,
                dbo.GetUserNameById(t.M_UploadBy) M_UploadBy,
                t.M_UploadDate
                ");
                strSql.Append("  FROM Mes_MaterInHead t left join Mes_MaterInDetail s on(t.M_MaterInNo=s.M_MaterInNo)");
                strSql.Append("  WHERE t.M_Status = 3  ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty() && string.IsNullOrWhiteSpace(M_MaterInNo))
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.M_CreateDate >= @startTime AND t.M_CreateDate <= @endTime ) ");
                }
                if (!string.IsNullOrWhiteSpace(M_MaterInNo) && queryParam["M_MaterInNo"].IsEmpty() && queryParam["S_Name"].IsEmpty())
                {
                    dp.Add("M_MaterInNo", "%" + M_MaterInNo + "%", DbType.String);
                    strSql.Append(" AND t.M_MaterInNo Like @M_MaterInNo ");
                }
                if (!queryParam["M_MaterInNo"].IsEmpty())
                {
                    dp.Add("M_MaterInNo", "%" + queryParam["M_MaterInNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_MaterInNo Like @M_MaterInNo ");
                }
                if (!queryParam["M_GoodsName"].IsEmpty())
                {
                    dp.Add("M_GoodsName", "%" + queryParam["M_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.M_GoodsName Like @M_GoodsName ");
                }
                  if (!queryParam["S_Name"].IsEmpty())
                {
                    dp.Add("S_Name", "%" + queryParam["S_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_SupplyName Like @S_Name ");
                }
                return this.BaseRepository().FindList<Mes_MaterInHeadEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_MaterInHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                distinct
                t.ID,
                t.M_MaterInNo,
                t.M_StockCode,
                t.M_StockName,
                t.M_OrderNo,
                t.M_OrderDate,
                t.M_Status,
                t.M_SupplyName,
                t.M_SupplyCode,
                dbo.GetUserNameById(t.M_CreateBy) M_CreateBy,
                t.M_CreateDate,
                dbo.GetUserNameById(t.M_UpdateBy) M_UpdateBy,
                t.M_UpdateDate,
                t.M_Remark,
                t.M_DeleteBy,
                t.M_DeleteDate,
                dbo.GetUserNameById(t.M_UploadBy) M_UploadBy,
                t.M_UploadDate
                ");
                strSql.Append("  FROM Mes_MaterInHead t left join Mes_MaterInDetail s on(t.M_MaterInNo=s.M_MaterInNo)");
                strSql.Append("  WHERE t.M_Status in (1,2) AND t.M_OrderKind=0 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.M_CreateDate >= @startTime AND t.M_CreateDate <= @endTime ) ");
                }
                if (!queryParam["S_Name"].IsEmpty())
                {
                    dp.Add("S_Name", "%" + queryParam["S_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_SupplyName Like @S_Name ");
                }
                if (!queryParam["M_GoodsName"].IsEmpty())
                {
                    dp.Add("M_GoodsName", "%" + queryParam["M_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.M_GoodsName Like @M_GoodsName ");
                }
                if (!queryParam["M_MaterInNo"].IsEmpty())
                {
                    dp.Add("M_MaterInNo", "%" + queryParam["M_MaterInNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_MaterInNo Like @M_MaterInNo ");
                }
                if (!queryParam["M_OrderNo"].IsEmpty())
                {
                    dp.Add("M_OrderNo", "%" + queryParam["M_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_OrderNo Like @M_OrderNo ");
                }
                if (!queryParam["M_StockCode"].IsEmpty())
                {
                    dp.Add("M_StockCode", "%" + queryParam["M_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_StockCode Like @M_StockCode ");
                }
                if (!queryParam["M_StockName"].IsEmpty())
                {
                    dp.Add("M_StockName", "%" + queryParam["M_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_StockName Like @M_StockName ");
                } 
                if (!queryParam["M_Status"].IsEmpty())
                {
                    dp.Add("M_Status", queryParam["M_Status"].ToString(),DbType.Int32);
                    strSql.Append(" AND t.M_Status = @M_Status ");
                }
                return this.BaseRepository().FindList<Mes_MaterInHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_MaterInDetail表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_MaterInDetailEntity> GetMes_MaterInDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_MaterInDetailEntity>(t=>t.M_MaterInNo == keyValue );
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
        /// 获取Mes_MaterInDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_MaterInDetailEntity GetMes_MaterInDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_MaterInDetailEntity>(t=>t.M_MaterInNo == keyValue);
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
        /// 获取原物料入库列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetMaterInSum(string queryJson)
        {
            try
            {
                var sqlHead = @"SELECT G_Name FROM dbo.Mes_GoodKind ";


                var sqlData = @"SELECT  *,0 allamount,0 tax,0 taxAmount
                                        
                                FROM    ( SELECT    h.M_SupplyName,
                                                    h.M_SupplyCode,
                                                    k.G_Name,
                                                    (m.M_Price*M_Qty) amount
                                          FROM      dbo.Mes_MaterInHead h
                                                    INNER JOIN Mes_MaterInDetail m ON m.M_MaterInNo=h.M_MaterInNo
                                                    INNER JOIN dbo.Mes_Goods g ON g.G_Code = m.M_GoodsCode
                                                    INNER JOIN dbo.Mes_GoodKind k ON k.G_Code = g.G_TKind
                                          WHERE     M_Status = 3  {0} ) tempDt pivot (sum(amount) for G_Name in  ({1})) as pt";

                var sqlTax = @"SELECT  *
                                        
                                FROM    ( SELECT    h.M_SupplyName,
                                                    k.G_Name,
                                                    (m.M_Price*M_Qty*M_GoodsItax) tax
                                          FROM      dbo.Mes_MaterInHead h
                                                    INNER JOIN Mes_MaterInDetail m ON m.M_MaterInNo=h.M_MaterInNo
                                                    INNER JOIN dbo.Mes_Goods g ON g.G_Code = m.M_GoodsCode
                                                    INNER JOIN dbo.Mes_GoodKind k ON k.G_Code = g.G_TKind
                                          WHERE     M_Status = 3  {0} ) tempDt pivot (sum(tax) for G_Name in  ({1})) as pt";


                var sqlTaxAmount = @"SELECT  *
                                        
                                FROM    ( SELECT    h.M_SupplyName,
                                                    k.G_Name,
                                                    (m.M_Price*M_Qty*(1+M_GoodsItax)) taxAmount
                                          FROM      dbo.Mes_MaterInHead h
                                                    INNER JOIN Mes_MaterInDetail m ON m.M_MaterInNo=h.M_MaterInNo
                                                    INNER JOIN dbo.Mes_Goods g ON g.G_Code = m.M_GoodsCode
                                                    INNER JOIN dbo.Mes_GoodKind k ON k.G_Code = g.G_TKind
                                          WHERE     M_Status = 3  {0} ) tempDt pivot (sum(taxAmount) for G_Name in  ({1})) as pt";


                
                var queryParam = queryJson.ToJObject();
                var dp = new DynamicParameters(new {});
                var sqlParm = new StringBuilder();

                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    sqlParm.Append(" AND ( h.M_CreateDate >= @startTime AND h.M_CreateDate <= @endTime ) ");
                }
                var rowHeads = this.BaseRepository().FindList<Mes_GoodKindEntity>(sqlHead).ToList();

                if (!rowHeads.Any())
                {
                    return new DataTable();
                }
                var dtColumns = new StringBuilder();
                List<string> goodsList = new List<string>();
                foreach (var row in rowHeads)
                {
                    goodsList.Add(string.Format("[{0}]", row.G_Name));
                }
                var dt= this.BaseRepository() .FindTable( string.Format(sqlData,sqlParm.ToString(), string.Join(",", goodsList)), dp);
                var dtTax = this.BaseRepository().FindTable(string.Format(sqlTax, sqlParm.ToString(), string.Join(",", goodsList)), dp);
                var dtTaxAlmount = this.BaseRepository().FindTable(string.Format(sqlTaxAmount, sqlParm.ToString(), string.Join(",", goodsList)), dp);
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    var list = dt.Rows[i].ItemArray.ToList();
                    list.RemoveAt(1);
                    var array = list.ToArray();
                    dt.Rows[i]["allamount"] = array.Sum(c => c.ToDecimal(6));
                }

                for (var i = 0; i < dtTax.Rows.Count; i++)
                {
                    var arr = dtTax.Rows[i].ItemArray;
                    dt.Rows[i]["tax"] = arr.Sum(c => c.ToDecimal(6));
                }

                 for (var i = 0; i < dtTaxAlmount.Rows.Count; i++)
                {
                    var arr = dtTaxAlmount.Rows[i].ItemArray;
                    dt.Rows[i]["taxamount"] = arr.Sum(c => c.ToDecimal(6));
                }
                return dt;
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
        /// 渲染前端表头
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ColumnModel> GetPageTitle(string queryJson)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(@"SELECT G_Name FROM dbo.Mes_GoodKind ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                
                var columnsHead = this.BaseRepository().FindList<Mes_GoodKindEntity>(sql.ToString());
                List<ColumnModel> cmList = new List<ColumnModel>();

                ColumnModel cm4 = new ColumnModel();
                cm4.name = "m_supplycode";
                cm4.label = "供应商编码";
                cm4.width = 80;
                cm4.align = "center";
                cm4.sort = false;
                cm4.statistics = false;
                cm4.children = null;
                cmList.Add(cm4);
                ColumnModel cm = new ColumnModel();
                cm.name = "m_supplyname";
                cm.label = "供应商名称";
                cm.width = 130;
                cm.align = "left";
                cm.sort = false;
                cm.statistics = false;
                cm.children = null;
                cmList.Add(cm);
                
                foreach (var col in columnsHead)
                {
                    ColumnModel cm_head = new ColumnModel();
                    cm_head.name = col.G_Name;
                    cm_head.label = col.G_Name;
                    cm_head.width = 90;
                    cm_head.align = "center";
                    cm_head.sort = false;
                    cm_head.statistics = false;
                    cm_head.children = null;
                    cmList.Add(cm_head);
                }

                ColumnModel cm1 = new ColumnModel();
                cm1.name = "allamount";
                cm1.label = "不含税金额(元)";
                cm1.width = 100;
                cm1.align = "center";
                cm1.sort = false;
                cm1.statistics = false;
                cm1.children = null;
                cmList.Add(cm1);
                ColumnModel cm2 = new ColumnModel();
                cm2.name = "tax";
                cm2.label = "合计税额(元)";
                cm2.width = 100;
                cm2.align = "center";
                cm2.sort = false;
                cm2.statistics = false;
                cm2.children = null;
                cmList.Add(cm2);
                 ColumnModel cm3 = new ColumnModel();
                cm3.name = "taxamount";
                cm3.label = "价税合计(元)";
                cm3.width = 100;
                cm3.align = "center";
                cm3.sort = false;
                cm3.statistics = false;
                cm3.children = null;
                cmList.Add(cm3);

                return cmList;
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
        /// 获取物料的合计税额，价税合计
        /// </summary>
        /// <returns></returns>
        public DataTable GetTaxAndAmount()
        {
            string sql = @"SELECT   
                    k.G_Name,
                    h.M_SupplyName,
                    SUM(d.M_Qty)qty,
                    SUM(d.M_Price*M_Qty) amount
                    SUM(d.M_Price*M_Qty*G_Itax) tax,
                    SUM(d.M_Price*M_Qty*(1+G_Itax))taxAmount
          FROM      dbo.Mes_MaterInHead h
                    LEFT JOIN Mes_MaterInDetail d ON d.M_MaterInNo= h.M_MaterInNo
                    INNER JOIN dbo.Mes_Goods g ON g.G_Code = d.M_GoodsCode
                    INNER JOIN dbo.Mes_GoodKind k ON k.G_Code = g.G_TKind
                    WHERE h.M_Status=3 GROUP BY k.G_Name,h.M_SupplyName ";
            var dt = this.BaseRepository().FindTable(sql);
            return dt;
        }

        /// <summary>
        /// 供应商存货明细
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetSupplyGoodsList(string queryJson)
        {
            var sql = @"SELECT  h.M_MaterInNo,
                                h.M_SupplyCode,
                                h.M_SupplyName ,
                                d.M_GoodsCode ,
                                d.M_GoodsName ,
                                d.M_Unit ,
                                d.M_Tax,
                                MAX(d.M_Price) M_Price,
                                SUM(d.M_Price * M_Qty) row_amount, 
                                SUM(M_Qty) row_qty,
                                d.M_Unit2,
                                d.M_UnitQty
                        FROM    dbo.Mes_MaterInHead h
                                LEFT JOIN dbo.Mes_MaterInDetail d ON d.M_MaterInNo = h.M_MaterInNo
                        WHERE   h.M_Status = 3
                                AND d.M_Kind = 1 {0}
                        GROUP BY
                                h.M_SupplyName,
                                h.M_SupplyCode,
                                h.M_MaterInNo, 
                                M_GoodsCode ,
                                M_GoodsName ,
                                M_Unit,
                                d.M_Tax,
                                d.M_Unit2,
                                d.M_UnitQty 
                                        ";

            var queryParam = queryJson.ToJObject();
            var dp = new DynamicParameters(new { });
            var sqlParam = new StringBuilder();
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                sqlParam.Append(" AND ( h.M_CreateDate >= @startTime AND h.M_CreateDate <= @endTime ) ");
            }
            var dt = this.BaseRepository().FindTable(string.Format(sql, sqlParam.ToString()),dp);
            return dt;
        }

        #endregion



        #region 提交数据
        /// <summary>
        /// 审核单据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void AuditingBill(string keyValue)
        {
            try
            {
                var entity = GetMes_MaterInHeadEntity(keyValue);
                entity.M_Status = ErpEnums.MaterInStatusEnum.Audit;
                this.BaseRepository().Update(entity);
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
        /// 撤销单据
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <param name="errMsg">错误信息</param>
        public int CancelBill(string orderNo, out string errMsg)
        {
            UserInfo userinfo = LoginUserInfo.Get();
            var dp = new DynamicParameters(new { });
            dp.Add("@OrderNo", orderNo);
            dp.Add("@UserName", userinfo.realName);
            dp.Add("@errcode", "", DbType.Int32, ParameterDirection.Output);
            dp.Add("@errtxt", "", DbType.String, ParameterDirection.Output);
            this.BaseRepository().ExecuteByProc("sp_MaterIn_Cancel", dp);
            errMsg = dp.Get<string>("@errtxt");//存储过程返回的错误消息
            return dp.Get<int>("@errcode");//返回的错误代码 0：成功
        }

        /// <summary>
        /// 提交单据
        /// </summary>
        /// <param name="orderNo">单号</param>
        /// <param name="errMsg">错误信息</param>
        public int PostBill(string orderNo,out string errMsg)
        {
            UserInfo userinfo = LoginUserInfo.Get();
            var dp=new DynamicParameters(new{});
            dp.Add("@OrderNo",orderNo);
            dp.Add("@UserName",userinfo.realName);
            dp.Add("@errcode", "", DbType.Int32, ParameterDirection.Output);
            dp.Add("@errtxt", "", DbType.String, ParameterDirection.Output);
            this.BaseRepository().ExecuteByProc("sp_MaterIn_Post", dp);
            errMsg = dp.Get<string>("@errtxt");//存储过程返回的错误消息
            return dp.Get<int>("@errcode");//返回的错误代码 0：成功
        }

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
                var mes_MaterInHeadEntity = GetMes_MaterInHeadEntity(keyValue); 
                db.Delete<Mes_MaterInHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_MaterInDetailEntity>(t=>t.M_MaterInNo == mes_MaterInHeadEntity.M_MaterInNo);
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
        public void SaveEntity(string keyValue, Mes_MaterInHeadEntity entity,List<Mes_MaterInDetailEntity> mes_MaterInDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_MaterInHeadEntityTmp = GetMes_MaterInHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_MaterInDetailEntity>(t=>t.M_MaterInNo == mes_MaterInHeadEntityTmp.M_MaterInNo);
                    foreach (Mes_MaterInDetailEntity item in mes_MaterInDetailList)
                    {
                        item.Create();
                        item.M_MaterInNo = mes_MaterInHeadEntityTmp.M_MaterInNo;
                        item.M_OrderNo = null;
                        db.Insert(item);
                    }
                }
                else
                {
                    var dp = new DynamicParameters(new { });
                    if (entity.M_OrderKind == ErpEnums.OrderKindEnum.NoProduct)
                    {
                        dp.Add("@BillType", "入库单");
                        dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                        db.ExecuteByProc("sp_GetDoucno", dp);
                    }
                    else
                    {
                        dp.Add("@BillType", "成品入库单");
                        dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                        db.ExecuteByProc("sp_GetDoucno", dp);
                    }
                    var billNo = dp.Get<string>("@Doucno"); //存储过程返回单号
                    entity.M_MaterInNo = billNo;
                    entity.Create();
                    db.Insert(entity);
                    foreach (Mes_MaterInDetailEntity item in mes_MaterInDetailList)
                    {
                        item.Create();
                        item.M_MaterInNo = entity.M_MaterInNo;
                        item.M_OrderNo = null;
                        //item.M_Kind = entity.M_Kind;
                        db.Insert(item);
                    }
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
