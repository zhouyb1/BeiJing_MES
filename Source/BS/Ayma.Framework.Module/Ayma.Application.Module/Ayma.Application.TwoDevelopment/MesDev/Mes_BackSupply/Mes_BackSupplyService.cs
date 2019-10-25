﻿using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 16:30
    /// 描 述：退供应商单制作
    /// </summary>
    public partial class Mes_BackSupplyService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_BackSupplyHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.B_BackSupplyNo,
                t.B_StockCode,
                t.B_StockName,
                t.B_OrderDate,
                t.B_Status,
                t.B_CreateBy,
                t.B_CreateDate,
                t.B_UpdateBy,
                t.B_UpdateDate,
                t.B_DeleteBy,
                t.B_DeleteDate,
                t.B_UploadBy,
                t.B_UploadDate,
                t.B_Remark
                ");
                strSql.Append("  FROM Mes_BackSupplyHead t ");
                strSql.Append("  WHERE 1=1 And t.B_Status in (1,2)");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.B_CreateDate >= @startTime AND t.B_CreateDate <= @endTime ) ");
                }
                if (!queryParam["B_OrderDate"].IsEmpty())
                {
                    dp.Add("B_OrderDate", "%" + queryParam["B_OrderDate"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_OrderDate Like @B_OrderDate ");
                }
                if (!queryParam["B_BackSupplyNo"].IsEmpty())
                {
                    dp.Add("B_BackSupplyNo", "%" + queryParam["B_BackSupplyNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_BackSupplyNo Like @B_BackSupplyNo ");
                }
                if (!queryParam["B_StockCode"].IsEmpty())
                {
                    dp.Add("B_StockCode", "%" + queryParam["B_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_StockCode Like @B_StockCode ");
                }
                if (!queryParam["B_StockName"].IsEmpty())
                {
                    dp.Add("B_StockName", "%" + queryParam["B_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_StockName Like @B_StockName ");
                }
                if (!queryParam["B_Status"].IsEmpty())
                {
                    dp.Add("B_Status", "%" + queryParam["B_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.B_Status Like @B_Status ");
                }
                return this.BaseRepository().FindList<Mes_BackSupplyHeadEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取退供应商物料数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetBackGoodsList(Pagination pagination, string queryJson, string keyword, string stockCode)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.I_GoodsCode,
                t.I_GoodsName,
                t.I_Unit,
                t.I_Qty,
                t.I_Batch
                ");
                strSql.Append("  FROM Mes_Inventory t LEFT JOIN Mes_Goods b ON t.I_GoodsCode=b.G_Code");
                strSql.Append("  WHERE b.G_Kind=1 and t.I_Qty <> 0 And t.I_StockCode=@I_StockCode ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters();
                dp.Add("I_StockCode", stockCode);
                if (!keyword.IsEmpty())
                {
                    dp.Add("keyword", "%" + keyword + "%", DbType.String);
                    strSql.Append(" AND I_GoodsCode+I_GoodsName like @keyword ");
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
        /// 获取Mes_BackSupplyDetail表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_BackSupplyDetailEntity> GetMes_BackSupplyDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_BackSupplyDetailEntity>(t => t.B_BackSupplyNo == keyValue);
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
        /// 获取Mes_BackSupplyHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_BackSupplyHeadEntity GetMes_BackSupplyHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_BackSupplyHeadEntity>(keyValue);
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
        /// 根据入库单Id获取退供应商表头数据
        /// </summary>
        /// <param name="materInKeyValue">入库单Id</param>
        /// <returns></returns>
        public DataTable GetMes_BackSupplyHeadModel(string materInKeyValue)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"
                                SELECT  M_StockCode AS B_StockCode ,
                        M_StockName AS B_StockName
                FROM    dbo.Mes_MaterInHead
                WHERE   ID = @ID
                ");
                
                // 虚拟参数
                var dp = new DynamicParameters();
                dp.Add("ID", materInKeyValue);
               
                return this.BaseRepository().FindTable(strSql.ToString(), dp);
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
        /// 获取Mes_BackSupplyDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_BackSupplyDetailEntity GetMes_BackSupplyDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_BackSupplyDetailEntity>(t => t.B_BackSupplyNo == keyValue);
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
        /// 根据入库单号 制作退供应商详情
        /// </summary>
        /// <param name="materInNo">入库单号</param>
        /// <returns></returns>
        public DataTable GetMes_BackSupplyList(string materInNo)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT  M_GoodsCode AS B_GoodsCode ,
                                M_GoodsName AS B_GoodsName ,
                                M_Unit AS B_Unit ,
                                M_Qty AS B_Qty ,
                                M_Batch AS B_Batch
                        FROM    dbo.Mes_MaterInDetail
                        WHERE   M_MaterInNo = @M_MaterInNo
                ");

                // 虚拟参数
                var dp = new DynamicParameters();
                dp.Add("M_MaterInNo", materInNo);

                return this.BaseRepository().FindTable(strSql.ToString(), dp);
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
                var mes_BackSupplyHeadEntity = GetMes_BackSupplyHeadEntity(keyValue);
                db.Delete<Mes_BackSupplyHeadEntity>(t => t.ID == keyValue);
                db.Delete<Mes_BackSupplyDetailEntity>(t => t.B_BackSupplyNo == mes_BackSupplyHeadEntity.B_BackSupplyNo);
                //var entity = GetMes_BackSupplyHeadEntity(keyValue); 
                //entity.Delete(keyValue);
                //db.Update(entity);
                //db.Commit();
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
        public void SaveEntity(string keyValue, Mes_BackSupplyHeadEntity entity, List<Mes_BackSupplyDetailEntity> mes_BackSupplyDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_BackSupplyHeadEntityTmp = GetMes_BackSupplyHeadEntity(keyValue);
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_BackSupplyDetailEntity>(t => t.B_BackSupplyNo == mes_BackSupplyHeadEntityTmp.B_BackSupplyNo);
                    foreach (Mes_BackSupplyDetailEntity item in mes_BackSupplyDetailList)
                    {
                        item.Create();
                        item.B_BackSupplyNo = mes_BackSupplyHeadEntityTmp.B_BackSupplyNo;
                        db.Insert(item);
                    }
                }
                else
                {
                    var dp = new DynamicParameters(new { });
                    dp.Add("@BillType", "退供应商单");
                    dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                    db.ExecuteByProc("sp_GetDoucno", dp);
                    var billNo = dp.Get<string>("@Doucno");//存储过程返回单号
                    entity.B_BackSupplyNo = billNo;
                    entity.Create();
                    db.Insert(entity);
                    foreach (Mes_BackSupplyDetailEntity item in mes_BackSupplyDetailList)
                    {
                        item.Create();
                        item.B_BackSupplyNo = entity.B_BackSupplyNo;
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
