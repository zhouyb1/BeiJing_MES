﻿using System.Linq;
using Ayma.Application.TwoDevelopment.MesDev.ScrapManager;
using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Ayma.Cache.Base;
using Ayma.Cache.Factory;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-18 15:14
    /// 描 述：组装与拆分单据制作
    /// </summary>
    public partial class OrgResMangerService : RepositoryFactory
    {
        private Mes_WorkShopScanIBLL workShopScanIbll =new Mes_WorkShopScanBLL();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_OrgResHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                distinct
                t.ID,
                t.O_Status,
                t.O_OrgResNo,
                dbo.GetProNamekByCode(O_ProCode) O_ProCode,
                t.O_StockCode,
                t.O_StockName,
                t.O_Remark,
                 dbo.GetUserNameById(t.O_CreateBy) O_CreateBy,
                t.O_CreateDate
                ");
                strSql.Append("  FROM Mes_OrgResHead t left join Mes_OrgResDetail s on(t.O_OrgResNo=s.O_OrgResNo)");
                strSql.Append("  WHERE 1=1 AND t.O_Status in (1,2)");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.O_CreateDate >= @startTime AND t.O_CreateDate <= @endTime ) ");
                }
                if (!queryParam["M_GoodsName"].IsEmpty())
                {
                    dp.Add("M_GoodsName", "%" + queryParam["M_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.O_GoodsName Like @M_GoodsName ");
                }
                if (!queryParam["O_SecGoodsName"].IsEmpty())
                {
                    dp.Add("O_SecGoodsName", "%" + queryParam["O_SecGoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.O_SecGoodsName Like @O_SecGoodsName ");
                }
                if (!queryParam["O_OrgResNo"].IsEmpty())
                {
                    dp.Add("O_OrgResNo", "%" + queryParam["O_OrgResNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_OrgResNo Like @O_OrgResNo ");
                }
                if (!queryParam["O_StockCode"].IsEmpty())
                {
                    dp.Add("O_StockCode", "%" + queryParam["O_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_StockCode Like @O_StockCode ");
                }
                if (!queryParam["O_Status"].IsEmpty())
                {
                    dp.Add("O_Status", "%" + queryParam["O_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_Status Like @O_Status ");
                }
                if (!queryParam["O_ProCode"].IsEmpty())
                {
                    dp.Add("O_ProCode", "%" + queryParam["O_ProCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_ProCode Like @O_ProCode ");
                }
                return this.BaseRepository().FindList<Mes_OrgResHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取组装与拆分单据查询页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_OrgResHeadEntity> OrgResManagerList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                distinct
                t.ID,
                t.O_Status,
                t.O_OrgResNo,
                t.O_OrderNo,
                t.O_OrderDate,
                t.O_Record,
                dbo.GetProNamekByCode(O_ProCode) O_ProCode,
                t.O_StockCode,
                t.O_StockName,
                t.O_Remark,
                dbo.GetUserNameById(t.O_CreateBy) O_CreateBy,
                t.O_CreateDate
                ");
                strSql.Append("  FROM Mes_OrgResHead t left join Mes_OrgResDetail s on(t.O_OrgResNo=s.O_OrgResNo)");
                strSql.Append("  WHERE 1=1 AND t.O_Status =3");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.O_CreateDate >= @startTime AND t.O_CreateDate <= @endTime ) ");
                }
                if (!queryParam["M_GoodsName"].IsEmpty())
                {
                    dp.Add("M_GoodsName", "%" + queryParam["M_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.O_GoodsName Like @M_GoodsName ");
                }
                if (!queryParam["O_SecGoodsName"].IsEmpty())
                {
                    dp.Add("O_SecGoodsName", "%" + queryParam["O_SecGoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.O_SecGoodsName Like @O_SecGoodsName ");
                }
                if (!queryParam["O_OrgResNo"].IsEmpty())
                {
                    dp.Add("O_OrgResNo", "%" + queryParam["O_OrgResNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_OrgResNo Like @O_OrgResNo ");
                }
                if (!queryParam["O_StockCode"].IsEmpty())
                {
                    dp.Add("O_StockCode", "%" + queryParam["O_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_StockCode Like @O_StockCode ");
                }
                if (!queryParam["O_Status"].IsEmpty())
                {
                    dp.Add("O_Status", "%" + queryParam["O_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_Status Like @O_Status ");
                }
                if (!queryParam["O_ProCode"].IsEmpty())
                {
                    dp.Add("O_ProCode", "%" + queryParam["O_ProCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_ProCode Like @O_ProCode ");
                }
                return this.BaseRepository().FindList<Mes_OrgResHeadEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取仓库物料
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public DataTable GetGoodsList(string keyword, string queryJson,Pagination obj)
        {
            ICache redisCache = CacheFactory.CaChe();
            var userId = LoginUserInfo.Get().userId;
            var key = userId + "_stock";
            var stock = redisCache.Read<string>(key);
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT          c.C_Code O_GoodsCode ,
                                        c.C_Name O_GoodsName ,
                                        c.C_SecCode O_SecGoodsCode ,
                                        c.C_SecName O_SecGoodsName ,
                                        i.I_Batch O_Batch ,
                                        i.I_Qty O_Qty ,
                                        i.I_Unit O_Unit ,
                                        (SELECT G_Price FROM dbo.Mes_Goods WHERE G_Code = i.I_GoodsCode) O_Price
                                FROM    dbo.Mes_Convert c
                                        INNER JOIN Mes_Inventory i ON i.I_GoodsCode = c.C_Code
                                        INNER JOIN dbo.Mes_Stock s ON s.S_Code = i.I_StockCode
                                        WHERE i.I_Qty > 0 AND s.S_Kind =4  ");
            // 虚拟参数
            var dp = new DynamicParameters(new { });
            var queryParam = queryJson.ToJObject();
            if (!keyword.IsEmpty())
            {
                dp.Add("keyword", "%" + keyword + "%", DbType.String);
                sb.Append(" AND i.I_GoodsCode+i.I_GoodsName like @keyword ");
            }
            if (!stock.IsEmpty())
            {
                dp.Add("stock", "%" + stock + "%", DbType.String);
                sb.Append(" AND i.I_StockCode like @stock ");
            }
            return BaseRepository().FindTable(sb.ToString(), dp,obj);
        }

        /// <summary>
        /// 获取转换后的物料
        /// </summary>
        /// <returns></returns>
        public DataTable GetSecGoodsList(string keyword,Pagination obj)
        {
            var sql = "SELECT DISTINCT c_secname,c_seccode,g_unit FROM dbo.Mes_Convert INNER JOIN dbo.Mes_Goods  ON C_SecCode= G_Code";
            var dp = new DynamicParameters(new { });

            if (!keyword.IsEmpty())
            {
                dp.Add("keyword", "%" + keyword + "%", DbType.String);
                sql += " where c_seccode+c_secname like @keyword ";
            }
            var dt = this.BaseRepository().FindTable(sql,dp,obj);
            return dt;
        }

        /// <summary>
        /// 获取Mes_OrgResHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_OrgResHeadEntity GetMes_OrgResHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_OrgResHeadEntity>(keyValue);
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
        /// 获取Mes_OrgResDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_OrgResDetailEntity> GetMes_OrgResDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_OrgResDetailEntity>(t=>t.O_OrgResNo == keyValue).OrderBy(c=>c.O_Index);
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
                var mes_OrgResHeadEntity = GetMes_OrgResHeadEntity(keyValue); 
                db.Delete<Mes_OrgResHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_OrgResDetailEntity>(t=>t.O_OrgResNo == mes_OrgResHeadEntity.O_OrgResNo);
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
        public void SaveEntity(string keyValue, Mes_OrgResHeadEntity entity,List<Mes_OrgResDetailEntity> mes_OrgResDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_OrgResHeadEntityTmp = GetMes_OrgResHeadEntity(keyValue);
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_OrgResDetailEntity>(t => t.O_OrgResNo == mes_OrgResHeadEntityTmp.O_OrgResNo);
                    foreach (var item in mes_OrgResDetailList)
                    {
                        item.Create();
                        item.O_OrgResNo = mes_OrgResHeadEntityTmp.O_OrgResNo;
                    }
                    db.Insert(mes_OrgResDetailList);
                }
                else
                {

                    var dp = new DynamicParameters(new {});
                    dp.Add("@BillType", "组装与拆分单");
                    dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                    db.ExecuteByProc("sp_GetDoucno", dp);
                    var billNo = dp.Get<string>("@Doucno"); //存储过程返回单号

                    entity.O_OrgResNo = billNo;
                    entity.Create();
                    db.Insert(entity);

                    //var list = mes_OrgResDetailList.GroupBy(c => c.O_GoodsCode).ToList();
                    foreach (var item in mes_OrgResDetailList)
                    {
                        item.Create();
                        item.O_OrgResNo = entity.O_OrgResNo;
                    }
                    //foreach (var item in list)
                    //{
                    //    var dr = db.FindEntity<Mes_WorkShopScanEntity>(c => c.W_GoodsCode == item.Key);
                    //    var num = mes_OrgResDetailList.Where(c => c.O_GoodsCode == item.Key).Sum(c => c.O_Qty);
                    //    dr.W_Qty -= num;
                    //    db.Update(dr);
                    //}
                    db.Insert(mes_OrgResDetailList);
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
