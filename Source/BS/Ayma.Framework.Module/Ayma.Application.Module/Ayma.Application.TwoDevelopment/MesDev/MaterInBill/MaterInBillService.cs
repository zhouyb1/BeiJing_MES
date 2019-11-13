﻿using System.Data.SqlClient;
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
                                  ,[G_CreateBy]
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
        public IEnumerable<Mes_GoodsEntity> GetGoodsList(Pagination pagination, string queryJson, string keyword)
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
                                  ,[G_Unit]
                                  ,[G_UnitWeight]
                                  ,[G_Super]
                                  ,[G_Lower]
                                  ,[G_CreateBy]
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
                                  ,(select P_InPrice from Mes_InPrice where P_GoodsCode=t.[G_Code] and P_SupplyCode=t.[G_SupplyCode]) as G_Price
                              FROM [dbo].[Mes_Goods] t ");
                strSql.Append(" where t.G_Kind !=3 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!keyword.IsEmpty())
                {
                    dp.Add("keyword", "%" + keyword + "%", DbType.String);
                    strSql.Append(" AND t.G_Code+t.G_Name like @keyword ");
                }
                if (!queryParam["G_StockCode"].IsEmpty())
                {
                    dp.Add("@G_StockCode", "%" + queryParam["G_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" and t.G_StockCode like @G_StockCode ");
                }
                if (!queryParam["G_SupplyCode"].IsEmpty())
                {
                    dp.Add("@G_SupplyCode", "%" + queryParam["G_SupplyCode"].ToString() + "%", DbType.String);
                    strSql.Append(" and t.G_SupplyCode like @G_SupplyCode ");
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
                t.ID,
                t.M_MaterInNo,
                t.M_StockCode,
                t.M_StockName,
                t.M_OrderNo,
                t.M_OrderDate,
                t.M_Status,
                t.M_CreateBy,
                t.M_CreateDate,
                t.M_UpdateBy,
                t.M_UpdateDate,
                t.M_Remark,
                t.M_DeleteBy,
                t.M_DeleteDate,
                t.M_UploadBy,
                t.M_UploadDate
                ");
                strSql.Append("  FROM Mes_MaterInHead t ");
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
                t.M_CreateBy,
                t.M_CreateDate,
                t.M_UpdateBy,
                t.M_UpdateDate,
                t.M_Remark,
                t.M_DeleteBy,
                t.M_DeleteDate,
                t.M_UploadBy,
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
        public IEnumerable<Mes_MaterInHeadEntity> GetPostPageList(Pagination pagination, string queryJson)
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
                t.M_CreateBy,
                t.M_CreateDate,
                t.M_UpdateBy,
                t.M_UpdateDate,
                t.M_Remark,
                t.M_DeleteBy,
                t.M_DeleteDate,
                t.M_UploadBy,
                t.M_UploadDate
                ");
                strSql.Append("  FROM Mes_MaterInHead t ");
                strSql.Append("  WHERE t.M_Status = 3  ");
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
                    dp.Add("M_Status", queryParam["M_Status"].ToString(), DbType.Int32);
                    strSql.Append(" AND t.M_Status = @M_Status ");
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
                t.ID,
                t.M_MaterInNo,
                t.M_StockCode,
                t.M_StockName,
                t.M_OrderNo,
                t.M_OrderDate,
                t.M_Status,
                t.M_SupplyName,
                t.M_SupplyCode,
                t.M_CreateBy,
                t.M_CreateDate,
                t.M_UpdateBy,
                t.M_UpdateDate,
                t.M_Remark,
                t.M_DeleteBy,
                t.M_DeleteDate,
                t.M_UploadBy,
                t.M_UploadDate
                ");
                strSql.Append("  FROM Mes_MaterInHead t ");
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
        public DataTable GetMaterInSum()
        {
        //    var dp = new DynamicParameters(new { });
        //    dp.Add("@tableName", "Mes_MaterInDetail");
        //    dp.Add("@groupColumn", "M_SupplyName");
        //    dp.Add("@row2column", "M_GoodsName");
        //    dp.Add("@row2columnValue", "M_Qty");
        //    dp.Add("@sql_where", "where M_SupplyName is not null");
            //var dt= this.BaseRepository().ExecuteByProc<DataTable>("sp_GetPivot", dp);
            var sqlConnection = this.BaseRepository().getDbConnection() as SqlConnection;
            if (sqlConnection != null)
            {
                var command = sqlConnection.CreateCommand();
                command.CommandText = "sp_GetPivot";
                command.CommandType=CommandType.StoredProcedure;
                command.Parameters.Add("@tableName", SqlDbType.VarChar).Value = "Mes_MaterInDetail";
                command.Parameters.Add("@groupColumn", SqlDbType.VarChar).Value = "M_SupplyName";
                command.Parameters.Add("@row2column", SqlDbType.VarChar).Value = "M_GoodsName";
                command.Parameters.Add("@row2columnValue", SqlDbType.VarChar).Value = "M_Qty";
                command.Parameters.Add("@sql_where", SqlDbType.VarChar).Value = "where M_SupplyName is not null";
                SqlDataAdapter sda = new SqlDataAdapter(command);

                var ds = new DataSet();
                sda.Fill(ds);
                var dt = ds.Tables[0];
                this.BaseRepository().getDbConnection().Close();
                return dt;

            }
            return new DataTable();
        }

        /// <summary>
        /// 渲染前端表头
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ColumnModel> GetPageTitle()
        {
            string sql = @"SELECT DISTINCT
                                    d.M_GoodsName
                            FROM    dbo.Mes_MaterInHead h
                                    LEFT JOIN dbo.Mes_MaterInDetail d ON d.M_MaterInNo = h.M_MaterInNo
                            WHERE   M_OrderKind = 0";
          var columns=  this.BaseRepository().FindList<Mes_MaterInDetailEntity>(sql);

            List<ColumnModel> cmList = new List<ColumnModel>();
          
            foreach (var col in columns)
            {
                ColumnModel cm1 = new ColumnModel();
                cm1.name = col.M_GoodsName;
                cm1.label = col.M_GoodsName;
                cm1.width = 130;
                cm1.align = "left";
                cm1.sort = false;
                cm1.statistics = false;
                cm1.children = null;
                cmList.Add(cm1);
            }
            return cmList;
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
                        item.M_OrderNo = entity.M_OrderNo;
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
                        item.M_OrderNo = entity.M_OrderNo;
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
