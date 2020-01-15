﻿using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Ayma.Application.TwoDevelopment.MesDev.ExtensionModel;
using Microsoft.Win32.SafeHandles;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-03-11 19:22
    /// 描 述：领料单
    /// </summary>
    public partial class PickingMaterService : RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 获取领料计划页面
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_CollarHeadTempEntity> GetTempPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.C_CollarNo,
                t.C_CollarNoZS,
                t.C_StockCode,
                t.C_StockName,
                t.C_StockToCode,
                t.C_StockToName,
                t.P_OrderNo,
                t.P_OrderDate,
                t.P_Status,
                t.C_CreateBy,
                t.C_CreateDate,
                t.C_UpdateBy,
                t.C_UpdateDate,
                t.C_Remark,
                t.M_DeleteBy,
                t.M_DeleteDate,
                t.M_UploadBy,
                t.M_UploadDate,
                t.C_TeamCode,
                t.MonthBalance
                ");
                strSql.Append("  FROM Mes_CollarHeadTemp t ");
                strSql.Append("  WHERE 1=1");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.C_CreateDate >= @startTime AND t.C_CreateDate <= @endTime ) ");
                }
                if (!queryParam["C_CollarNo"].IsEmpty())
                {
                    dp.Add("C_CollarNo", "%" + queryParam["C_CollarNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_CollarNo Like @C_CollarNo ");
                }
                if (!queryParam["C_CollarNoZS"].IsEmpty())
                {
                    dp.Add("C_CollarNoZS", "%" + queryParam["C_CollarNoZS"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_CollarNoZS Like @C_CollarNoZS ");
                }
                if (!queryParam["C_CreateBy"].IsEmpty())
                {
                    dp.Add("C_CreateBy", "%" + queryParam["C_CreateBy"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_CreateBy Like @C_CreateBy ");
                }
                if (!queryParam["C_StockToCode"].IsEmpty())
                {
                    dp.Add("C_StockToCode", "%" + queryParam["C_StockToCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_StockToCode Like @C_StockToCode ");
                }
                if (!queryParam["P_Status"].IsEmpty())
                {
                    dp.Add("P_Status", "%" + queryParam["P_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_Status Like @P_Status ");
                }
                return this.BaseRepository().FindList<Mes_CollarHeadTempEntity>(strSql.ToString(), dp, pagination);
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
        public IEnumerable<Mes_CollarHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                distinct
                t.ID,
                t.P_Status,
                t.C_CollarNo,
                t.C_StockCode,
                t.C_StockName,
                t.C_StockToCode,
                t.C_StockToName,
                t.P_OrderNo,
                t.P_OrderDate,
                t.C_Remark,
                dbo.GetUserNameById(t.C_CreateBy) C_CreateBy,
                t.C_CreateDate
                ");
                strSql.Append("  FROM Mes_CollarHead t left join Mes_CollarDetail s on(t.C_CollarNo=s.C_CollarNo)");
                strSql.Append("  WHERE t.P_Status in (1,2) ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.C_CreateDate >= @startTime AND t.C_CreateDate <= @endTime ) ");
                }
                if (!queryParam["M_GoodsName"].IsEmpty())
                {
                    dp.Add("M_GoodsName", "%" + queryParam["M_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.C_GoodsName Like @M_GoodsName ");
                }
                if (!queryParam["C_CollarNo"].IsEmpty())
                {
                    dp.Add("C_CollarNo", "%" + queryParam["C_CollarNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_CollarNo Like @C_CollarNo ");
                }
                if (!queryParam["P_OrderNo"].IsEmpty())
                {
                    dp.Add("P_OrderNo", "%" + queryParam["P_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_OrderNo Like @P_OrderNo ");
                }
                if (!queryParam["C_StockName"].IsEmpty())
                {
                    dp.Add("C_StockName", "%" + queryParam["C_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_StockName Like @C_StockName ");
                }
                if (!queryParam["C_StockToName"].IsEmpty())
                {
                    dp.Add("C_StockToName", "%" + queryParam["C_StockToName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_StockToName Like @C_StockToName ");
                }
                if (!queryParam["C_StockCode"].IsEmpty())
                {
                    dp.Add("C_StockCode", "%" + queryParam["C_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_StockCode Like @C_StockCode ");
                }
                if (!queryParam["C_StockToCode"].IsEmpty())
                {
                    dp.Add("C_StockToCode", "%" + queryParam["C_StockToCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_StockToCode Like @C_StockToCode ");
                }
                if (!queryParam["P_Status"].IsEmpty())
                {
                    dp.Add("P_Status", "%" + queryParam["P_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_Status Like @P_Status ");
                }
                return this.BaseRepository().FindList<Mes_CollarHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_CollarHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_CollarHeadEntity GetMes_CollarHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_CollarHeadEntity>(keyValue);
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
        /// 获取Mes_CollarDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable< Mes_CollarDetailEntity >GetMes_CollarDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_CollarDetailEntity>(t=>t.C_CollarNo == keyValue);
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
        /// 获取Mes_CollarTempHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_CollarHeadTempEntity GetMes_CollarHeadTempEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_CollarHeadTempEntity>(keyValue);
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
        /// 获取Mes_CollarTempDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_CollarDetailTempEntity> GetMes_CollarDetailTempEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_CollarDetailTempEntity>(t => t.C_CollarNo == keyValue);
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
        public IEnumerable<Mes_InventoryEntity> GetMaterList(Pagination pagination, string queryJson, string keyword)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT  S.ID ,
                                        S.I_StockCode ,
                                        S.I_StockName ,
                                        S.I_Kind ,
                                        S.I_GoodsCode ,
                                        S.I_GoodsName ,
                                        S.I_Unit ,
                                        S.I_Qty ,                              
                                        S.I_Batch ,
									    G.G_Price I_Price,
										G.G_UnitQty,
										G.G_Unit2,
										G.G_Unit,
                                        G.G_StockCode as C_StockCode, 
                                        (select S_Name from Mes_Stock where S_Code=G.G_StockCode) as C_StockName
                                   FROM    dbo.Mes_Inventory S   left join Mes_Goods G on (S.I_GoodsCode=G.G_Code) where  S.I_Kind=1 and  S.I_Qty <> 0 ");

                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!keyword .IsEmpty())
                {
                    dp.Add("keyword", "%"+keyword+"%", DbType.String);
                    strSql.Append(" AND  S.I_GoodsCode + S.I_GoodsName like @keyword ");
                }
                if (!queryParam["stockCode"].IsEmpty())
                {
                    dp.Add("stockCode", "%" + queryParam["stockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND  S.I_StockCode like @stockCode ");
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

        /// <summary>
        /// 获取出成率报表数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetProductReportData(string queryJson, out string message)
        {
            bool success = true;
            message = "";

            try
            {
                List<ProductBom> boms = new List<ProductBom>();
                Dictionary<string, List<ProductBom>> products = new Dictionary<string, List<ProductBom>>();
                List<GoodsConvert> converts = new List<GoodsConvert>();
                string goodscode = "";

                #region 参数判断

                string F_StartTime = "";
                string F_EndTime = "";
                string F_GoodsCode = "";

                var queryParam = queryJson.ToJObject();
                if (queryParam["StartTime"].IsEmpty() || queryParam["EndTime"].IsEmpty() || queryParam["GoodsCode"].IsEmpty())
                {
                    success = false;
                    message = "查询参数缺失";
                }
                else
                {
                    F_StartTime = queryParam["StartTime"].ToString();
                    F_EndTime = queryParam["EndTime"].ToString();
                    F_GoodsCode = queryParam["GoodsCode"].ToString();
                }
                #endregion
                
                #region  获取产品配方

                if (success)
                {
                    //加载所有成品
                    string strGetBom = @"--递归获取子节点
                WITH CTE
                AS (SELECT ID,
                B_ParentID,
                B_GoodsCode,
                B_GoodsName,
                B_StockCode,
                B_Qty,
                B_ProceCode,
                B_ProceName,
                0 AS F_Level
                FROM Mes_BomRecord
                WHERE B_ParentID = '0'
                UNION ALL
                SELECT Bom1.ID,
                Bom1.B_ParentID,
                Bom1.B_GoodsCode,
                Bom1.B_GoodsName,
                Bom1.B_StockCode,
                Bom1.B_Qty,
                Bom1.B_ProceCode,
                Bom1.B_ProceName,
                (Bom2.F_Level + 1) F_Level
                    FROM Mes_BomRecord Bom1,CTE Bom2
                    WHERE Bom1.B_ParentID = Bom2.ID
                    )
                SELECT C.ID F_ID,
                    C.B_ParentID F_ParentID,
                    C.B_GoodsCode F_GoodsCode,
                    G.G_Name F_GoodsName,
                    C.B_ProceCode F_ProceCode,
                    C.B_ProceName F_ProceName,
                    G.G_Kind F_Kind,
                    G.G_Unit F_Unit,--基本单位
                G.G_Unit2 F_Unit2,--包装单位
                ISNULL(G.G_UnitQty,1) F_UnitQty,--包装数量(包装规格)
                G.G_Price F_Price,--成本价
                C.B_StockCode F_InStockCode,--领料仓库编码
                S1.S_Name F_InStockName,--领料仓库名称
                G.G_StockCode F_OutStockCode,--出料仓库编码
                S2.S_Name F_OutStockName,--出料仓库名称
				ISNULL(M.C_Min,0) F_ConvertMin,--最低转化率
				ISNULL(M.C_Max,100) F_ConvertMax,--最大转化率
                B_Qty F_PlanQty,--计划数量
                B_Qty F_ProposeQty,--建议数量
                F_Level
                FROM CTE C
                LEFT JOIN Mes_Goods G ON C.B_GoodsCode = G.G_Code
				LEFT JOIN Mes_Convert M ON M.C_SecCode=C.B_GoodsCode
                LEFT JOIN Mes_Stock S1 ON S1.S_Code=C.B_StockCode
                LEFT JOIN Mes_Stock S2 ON S2.S_Code=G.G_StockCode
                ORDER BY C.F_Level";

                    var rows = new RepositoryFactory().BaseRepository().FindList<ProductBom>(strGetBom);
                    if (rows.Count() > 0)
                    {
                        boms = rows.ToList();
                    }
                    else
                    {
                        success = false;
                        message = "未获取到任何配方数据";
                    }
                }


                #endregion

                #region 根据原物料，找出成品
                if (success)
                {
                    var rows=boms.Where(r => r.F_GoodsCode == F_GoodsCode);
                    if (rows==null || rows.Count() < 0)
                    {
                        success = false;
                        message = "未从配方数据中匹配到相应原物料";
                    }
                    else
                    {
                        int maxlevel = 0;
                        foreach (var row in rows)
                        {
                            List<ProductBom> goods=new List<ProductBom>();
                            GetGoods(row.F_ID, boms, goods);
                            var bomroot=goods.Find(r => r.F_Level == 0);
                            if (bomroot == null)
                            {
                                success = false;
                                message = "原物料对应配方不完整";
                                break;
                            }
                            else
                            {
                                var F_Level = goods.Max(r => r.F_Level);
                                if (F_Level > maxlevel)
                                {
                                    maxlevel = F_Level;
                                    goodscode = bomroot.F_GoodsCode;
                                }

                                products[bomroot.F_GoodsCode] = goods;
                            }
                        }
                    }
                }
                #endregion

                #region 提取转换单数据
                if (success)
                {
                    foreach (var product in products)
                    {
                        string strGetQty = @"
--当前物料数量统计
SELECT 
       F_CreateDate,
	   F_GoodsCode,
       SUM(F_Qty) F_Qty,
       1 F_Type
FROM
(
    SELECT DISTINCT
           H.O_OrgResNo F_OrderNo,       --相关单据
           D.O_SecGoodsCode F_GoodsCode, --当前物料编码
		   CONVERT(VARCHAR(7),H.O_CreateDate,120) F_CreateDate,--单据月份
           D.O_SecQty F_Qty              --当前物料数量
    FROM Mes_OrgResHead H
        INNER JOIN Mes_OrgResDetail D
            ON D.O_OrgResNo = H.O_OrgResNo
    WHERE H.O_Status = 3
          AND (H.O_CreateDate >= @startTime AND H.O_CreateDate < @endTime)
          AND D.O_GoodsCode = @F_GoodsCode
          AND D.O_SecGoodsCode = @F_SecGoodsCode
) MyData
GROUP BY F_CreateDate,F_GoodsCode

UNION ALL

--上级与当前关联物料统计
SELECT F_CreateDate,
       F_GoodsCode,
       SUM(F_Qty) F_Qty,
       2 F_Type
FROM
(
    SELECT 
	       D.O_GoodsCode F_GoodsCode, --上一级半成品编码
		   CONVERT(VARCHAR(7),H.O_CreateDate,120) F_CreateDate,--单据月份
           D.O_Qty F_Qty              --上一级半成品数量
    FROM Mes_OrgResHead H
        INNER JOIN Mes_OrgResDetail D
            ON D.O_OrgResNo = H.O_OrgResNo
    WHERE H.O_Status = 3
          AND (H.O_CreateDate >= @startTime AND H.O_CreateDate < @endTime)
          AND D.O_GoodsCode = @F_GoodsCode
          AND D.O_SecGoodsCode = @F_SecGoodsCode
) MyData
GROUP BY F_CreateDate,F_GoodsCode

UNION ALL

----上级物料统计
SELECT F_CreateDate,
       F_GoodsCode,
       SUM(F_Qty) F_Qty,
       3 F_Type
FROM
(
    SELECT 
		   DISTINCT
           H.O_OrgResNo F_OrderNo,       --相关单据
		   D.O_SecGoodsCode F_GoodsCode, --上一级半成品编码
		    CONVERT(VARCHAR(7),H.O_CreateDate,120) F_CreateDate,--单据月份
           D.O_SecQty F_Qty--上一级半成品数量
    FROM Mes_OrgResHead H
        INNER JOIN Mes_OrgResDetail D
            ON D.O_OrgResNo = H.O_OrgResNo
    WHERE H.O_Status = 3
          AND (H.O_CreateDate >= @startTime AND H.O_CreateDate < @endTime)
          AND D.O_SecGoodsCode = @F_GoodsCode
) MyData
GROUP BY F_CreateDate,F_GoodsCode";

                        var F_Level = product.Value.Max(r => r.F_Level);
                        for (int i = 0; i < F_Level; i++)
                        {
                            int j = i + 1;
                            var currentBom = product.Value.Find(r => r.F_Level == i);
                            var lastBom = product.Value.Find(r => r.F_Level == j);

                            //加载参数
                            var dp = new DynamicParameters(new { });
                            dp.Add("startTime", F_StartTime.ToDate(), DbType.DateTime);
                            dp.Add("endTime", F_EndTime.ToDate(), DbType.DateTime);
                            dp.Add("F_SecGoodsCode", currentBom.F_GoodsCode, DbType.String);
                            dp.Add("F_GoodsCode", lastBom.F_GoodsCode, DbType.String);
                            
                            var rows = new RepositoryFactory().BaseRepository().FindList<GoodsOrg>(strGetQty, dp);
                            if (rows.Count() > 0)
                            {
                                var gruops=rows.GroupBy(r => r.F_CreateDate);
                                foreach (var gruop in gruops)
                                {
                                    var gruoprows = gruop.ToList();
                                    if (gruoprows.Count != 3)
                                    {
                                        //断层数据，无法计算
                                        break;
                                    }

                                    var group1 = gruoprows.Find(r => r.F_Type == 1);
                                    var group2 = gruoprows.Find(r => r.F_Type == 2);
                                    var group3 = gruoprows.Find(r => r.F_Type == 3);

                                    if (i == 0)
                                    {
                                        //成品
                                        GoodsConvert currentGc = new GoodsConvert();
                                        currentGc.F_ID = currentBom.F_ID;
                                        currentGc.F_ParentID = currentBom.F_ParentID;

                                        currentGc.F_CreateDate = gruop.Key;

                                        currentGc.F_GoodsCode = currentBom.F_GoodsCode;
                                        currentGc.F_GoodsName = currentBom.F_GoodsName;

                                        currentGc.F_ProceCode = currentBom.F_ProceCode;
                                        currentGc.F_ProceName = currentBom.F_ProceName;

                                        currentGc.F_Level = currentBom.F_Level;
                                        currentGc.F_Kind = currentBom.F_Kind;
                                        currentGc.F_Unit = currentBom.F_Unit;

                                        currentGc.F_Qty = group1.F_Qty;

                                        currentGc.F_ConvertMin = currentBom.F_ConvertMin;
                                        currentGc.F_ConvertMax = currentBom.F_ConvertMax;
                                        currentGc.F_ConvertRange = currentBom.F_ConvertMin.Value + "-" + currentBom.F_ConvertMax.Value;
                                        currentGc.F_Convert = (group1.F_Qty / group2.F_Qty) * 100;
                                        if (currentGc.F_Convert > currentGc.F_ConvertMax)
                                            currentGc.F_ConvertTag = 1;
                                        else
                                        {
                                            if (currentGc.F_Convert < currentGc.F_ConvertMin)
                                                currentGc.F_ConvertTag = -1;
                                            else
                                            {
                                                currentGc.F_ConvertTag = 0;
                                            }
                                        }



                                        GoodsConvert lastGc = new GoodsConvert();
                                        lastGc.F_ID = lastBom.F_ID;
                                        lastGc.F_ParentID = lastBom.F_ParentID;

                                        lastGc.F_CreateDate = gruop.Key;

                                        lastGc.F_GoodsCode = lastBom.F_GoodsCode;
                                        lastGc.F_GoodsName = lastBom.F_GoodsName;

                                        lastGc.F_ProceCode = lastBom.F_ProceCode;
                                        lastGc.F_ProceName = lastBom.F_ProceName;

                                        lastGc.F_Level = lastBom.F_Level;
                                        lastGc.F_Kind = lastBom.F_Kind;
                                        lastGc.F_Unit = lastBom.F_Unit;

                                        lastGc.F_Qty = group2.F_Qty;
                                        lastGc.F_SumQty = group3.F_Qty;
                                        
                                        lastGc.F_ConvertMin = lastBom.F_ConvertMin;
                                        lastGc.F_ConvertMax = lastBom.F_ConvertMax;
                                        lastGc.F_ConvertRange = lastBom.F_ConvertMin.Value + "-" + lastBom.F_ConvertMax.Value;
                                        lastGc.F_Convert =0;
                                        lastGc.F_ConvertTag = 0;

                                        converts.Add(currentGc);
                                        converts.Add(lastGc);
                                    }
                                    else
                                    {
                                        //半成品
                                        var currentGc = converts.Find(r => r.F_ID == currentBom.F_ID && r.F_CreateDate == gruop.Key);

                                        GoodsConvert lastGc = new GoodsConvert();
                                        lastGc.F_ID = lastBom.F_ID;
                                        lastGc.F_ParentID = lastBom.F_ParentID;

                                        lastGc.F_CreateDate = gruop.Key;

                                        lastGc.F_GoodsCode = lastBom.F_GoodsCode;
                                        lastGc.F_GoodsName = lastBom.F_GoodsName;

                                        lastGc.F_ProceCode = lastBom.F_ProceCode;
                                        lastGc.F_ProceName = lastBom.F_ProceName;

                                        lastGc.F_Level = lastBom.F_Level;
                                        lastGc.F_Kind = lastBom.F_Kind;
                                        lastGc.F_Unit = lastBom.F_Unit;

                                        lastGc.F_Qty =(currentGc.F_Qty/currentGc.F_SumQty)*group2.F_Qty;
                                        lastGc.F_SumQty = group3.F_Qty;

                                        lastGc.F_ConvertMin = lastBom.F_ConvertMin;
                                        lastGc.F_ConvertMax = lastBom.F_ConvertMax;
                                        lastGc.F_ConvertRange = lastBom.F_ConvertMin.Value + "-" + lastBom.F_ConvertMax.Value;
                                        lastGc.F_Convert = 0;
                                        lastGc.F_ConvertTag = 0;


                                        currentGc.F_Convert = (currentGc.F_Qty / lastGc.F_Qty) * 100;
                                        if (currentGc.F_Convert > currentGc.F_ConvertMax)
                                            currentGc.F_ConvertTag = 1;
                                        else
                                        {
                                            if (currentGc.F_Convert < currentGc.F_ConvertMin)
                                                currentGc.F_ConvertTag = -1;
                                            else
                                            {
                                                currentGc.F_ConvertTag = 0;
                                            }
                                        }
                                        
                                        converts.Add(lastGc);
                                    }
                                }
                            }
                            else
                            {
                                //断层数据，无法计算
                                break;
                            }
                        }
                    }
                }
                #endregion

                #region 组装数据

                if (success)
                {
                    DataTable dt = new DataTable("GoodsConvert");
                    DataColumn dc=new DataColumn();
                    dc.ColumnName = "F_CreateDate";
                    dc.DataType = typeof(string);
                    dt.Columns.Add(dc);

                    var maxboms = products[goodscode];
                    var maxlevel = maxboms.Max(r => r.F_Level);

                    var starbom = maxboms.Find(r => r.F_Level == maxlevel);
                    dc = new DataColumn();
                    dc.ColumnName = "F_GoodsCode_" + starbom.F_ProceCode;
                    dc.DataType = typeof(string);
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.ColumnName = "F_GoodsName_" + starbom.F_ProceCode;
                    dc.DataType = typeof(string);
                    dt.Columns.Add(dc);

                    dc = new DataColumn();
                    dc.ColumnName = "F_GoodsQty_" + starbom.F_ProceCode;
                    dc.DataType = typeof(decimal);
                    dt.Columns.Add(dc);

                    for (int i = maxlevel-1; i <=0; i--)
                    {
                        var bom = maxboms.Find(r => r.F_Level==i);
                        dc = new DataColumn();
                        dc.ColumnName = "F_GoodsCode_"+bom.F_ProceCode;
                        dc.DataType = typeof(string);
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.ColumnName = "F_GoodsName_" + bom.F_ProceCode;
                        dc.DataType = typeof(string);
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.ColumnName = "F_GoodsQty_" + bom.F_ProceCode;
                        dc.DataType = typeof(decimal);
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.ColumnName = "F_ConvertRange_" + bom.F_ProceCode;
                        dc.DataType = typeof(string);
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.ColumnName = "F_Convert_" + bom.F_ProceCode;
                        dc.DataType = typeof(decimal);
                        dt.Columns.Add(dc);

                        dc = new DataColumn();
                        dc.ColumnName = "F_ConvertTag_" + bom.F_ProceCode;
                        dc.DataType = typeof(int);
                        dt.Columns.Add(dc);
                    }


                    var groups=converts.GroupBy(r => r.F_CreateDate);
                    foreach (var group in groups)
                    {
                        var rows = group.Where(r=>r.F_Level==0);
                        foreach (var row in rows)
                        {
                            DataRow dr = dt.NewRow();
                            SetDataRow(row.F_ParentID, group.ToList(), dr);
                            dt.Rows.Add(dt);
                        }
                    }

                    return dt;
                }

               
                #endregion
                return null;
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
        /// 获取出成率报表表体
        /// </summary>
        /// <returns></returns>
        public List<ColumnModel> GetProductReportTitle(string queryJson, out string message)
        {
            bool success = true;
            message = "";

            try
            {
                List<ColumnModel> columns=new List<ColumnModel>();
                List<ProductBom> boms = new List<ProductBom>();
                Dictionary<string, List<ProductBom>> products = new Dictionary<string, List<ProductBom>>();
                string goodscode = "";

                #region 参数判断

                string F_StartTime = "";
                string F_EndTime = "";
                string F_GoodsCode = "";

                var queryParam = queryJson.ToJObject();
                if (queryParam["GoodsCode"].IsEmpty())
                {
                    success = false;
                    message = "查询参数缺失";
                }
                else
                {
                    F_GoodsCode = queryParam["GoodsCode"].ToString();
                }
                #endregion

                #region  获取产品配方

                if (success)
                {
                    //加载所有成品
                    string strGetBom = @"--递归获取子节点
                WITH CTE
                AS (SELECT ID,
                B_ParentID,
                B_GoodsCode,
                B_GoodsName,
                B_StockCode,
                B_Qty,
                B_ProceCode,
                B_ProceName,
                0 AS F_Level
                FROM Mes_BomRecord
                WHERE B_ParentID = '0'
                UNION ALL
                SELECT Bom1.ID,
                Bom1.B_ParentID,
                Bom1.B_GoodsCode,
                Bom1.B_GoodsName,
                Bom1.B_StockCode,
                Bom1.B_Qty,
                Bom1.B_ProceCode,
                Bom1.B_ProceName,
                (Bom2.F_Level + 1) F_Level
                    FROM Mes_BomRecord Bom1,CTE Bom2
                    WHERE Bom1.B_ParentID = Bom2.ID
                    )
                SELECT C.ID F_ID,
                    C.B_ParentID F_ParentID,
                    C.B_GoodsCode F_GoodsCode,
                    G.G_Name F_GoodsName,
                    C.B_ProceCode F_ProceCode,
                    C.B_ProceName F_ProceName,
                    G.G_Kind F_Kind,
                    G.G_Unit F_Unit,--基本单位
                G.G_Unit2 F_Unit2,--包装单位
                ISNULL(G.G_UnitQty,1) F_UnitQty,--包装数量(包装规格)
                G.G_Price F_Price,--成本价
                C.B_StockCode F_InStockCode,--领料仓库编码
                S1.S_Name F_InStockName,--领料仓库名称
                G.G_StockCode F_OutStockCode,--出料仓库编码
                S2.S_Name F_OutStockName,--出料仓库名称
				ISNULL(M.C_Min,0) F_ConvertMin,--最低转化率
				ISNULL(M.C_Max,100) F_ConvertMax,--最大转化率
                B_Qty F_PlanQty,--计划数量
                B_Qty F_ProposeQty,--建议数量
                F_Level
                FROM CTE C
                LEFT JOIN Mes_Goods G ON C.B_GoodsCode = G.G_Code
				LEFT JOIN Mes_Convert M ON M.C_SecCode=C.B_GoodsCode
                LEFT JOIN Mes_Stock S1 ON S1.S_Code=C.B_StockCode
                LEFT JOIN Mes_Stock S2 ON S2.S_Code=G.G_StockCode
                ORDER BY C.F_Level";

                    var rows = new RepositoryFactory().BaseRepository().FindList<ProductBom>(strGetBom);
                    if (rows.Count() > 0)
                    {
                        boms = rows.ToList();
                    }
                    else
                    {
                        success = false;
                        message = "未获取到任何配方数据";
                    }
                }


                #endregion

                #region 根据原物料，找出成品
                if (success)
                {
                    var rows = boms.Where(r => r.F_GoodsCode == F_GoodsCode);
                    if (rows == null || rows.Count() < 0)
                    {
                        success = false;
                        message = "未从配方数据中匹配到相应原物料";
                    }
                    else
                    {
                        int maxlevel = 0;
                        foreach (var row in rows)
                        {
                            List<ProductBom> goods = new List<ProductBom>();
                            GetGoods(row.F_ID, boms, goods);
                            var bomroot = goods.Find(r => r.F_Level == 0);
                            if (bomroot == null)
                            {
                                success = false;
                                message = "原物料对应配方不完整";
                                break;
                            }
                            else
                            {
                                var F_Level = goods.Max(r => r.F_Level);
                                if (F_Level > maxlevel)
                                {
                                    maxlevel = F_Level;
                                    goodscode = bomroot.F_GoodsCode;
                                }

                                products[bomroot.F_GoodsCode] = goods;
                            }
                        }
                    }
                }
                #endregion

                #region 组装数据

                if (success)
                {
                    var maxboms = products[goodscode];
                    var maxlevel = maxboms.Max(r => r.F_Level);
                  

                    if (true)
                    {
                        ColumnModel cm = new ColumnModel();
                        cm.name = "F_CreateDate";
                        cm.label = "日期";
                        cm.width = 100;
                        cm.align = "left";
                        cm.sort = false;
                        cm.statistics = false;
                        cm.children = null;
                        columns.Add(cm);
                    }

                    if (true)
                    {
                        var starbom = maxboms.Find(r => r.F_Level == maxlevel);

                        ColumnModel cm1 = new ColumnModel();
                        cm1.name = "F_GoodsCode_" + starbom.F_ProceCode;
                        cm1.label = "原物料编码";
                        cm1.width = 100;
                        cm1.align = "left";
                        cm1.sort = false;
                        cm1.statistics = false;
                        cm1.children = null;

                        ColumnModel cm2= new ColumnModel();
                        cm2.name = "F_GoodsName_" + starbom.F_ProceCode;
                        cm2.label = "原物料名称";
                        cm2.width = 100;
                        cm2.align = "left";
                        cm2.sort = false;
                        cm2.statistics = false;
                        cm2.children = null;

                        ColumnModel cm3 = new ColumnModel();
                        cm3.name = "F_GoodsQty_" + starbom.F_ProceCode;
                        cm3.label = "数量(KG)";
                        cm3.width = 100;
                        cm3.align = "left";
                        cm3.sort = false;
                        cm3.statistics = false;
                        cm3.children = null;

                        ColumnModel cm = new ColumnModel();
                        cm.name = "F_ProceCode_" + starbom.F_ProceCode;
                        cm.label = starbom.F_ProceName;
                        cm.width = 300;
                        cm.align = "center";
                        cm.sort = false;
                        cm.statistics = false;
                        cm.children = new List<ColumnModel>();
                        cm.children.Add(cm1);
                        cm.children.Add(cm2);
                        cm.children.Add(cm3);
                        columns.Add(cm);
                    }

                    if (true)
                    {
                        for (int i = maxlevel - 1; i >= 0; i--)
                        {
                            var bom = maxboms.Find(r => r.F_Level == i);

                            ColumnModel cm1 = new ColumnModel();
                            cm1.name = "F_GoodsCode_" + bom.F_ProceCode;
                            cm1.label = "物料编码";
                            cm1.width = 100;
                            cm1.align = "left";
                            cm1.sort = false;
                            cm1.statistics = false;
                            cm1.children = null;

                            ColumnModel cm2 = new ColumnModel();
                            cm2.name = "F_GoodsName_" + bom.F_ProceCode;
                            cm2.label = "物料名称";
                            cm2.width = 100;
                            cm2.align = "left";
                            cm2.sort = false;
                            cm2.statistics = false;
                            cm2.children = null;

                            ColumnModel cm3 = new ColumnModel();
                            cm3.name = "F_GoodsQty_" + bom.F_ProceCode;
                            cm3.label = "数量(KG)";
                            cm3.width = 100;
                            cm3.align = "left";
                            cm3.sort = false;
                            cm3.statistics = false;
                            cm3.children = null;

                            ColumnModel cm4 = new ColumnModel();
                            cm4.name = "F_ConvertRange_" + bom.F_ProceCode;
                            cm4.label = "转化率标准";
                            cm4.width = 100;
                            cm4.align = "left";
                            cm4.sort = false;
                            cm4.statistics = false;
                            cm4.children = null;

                            ColumnModel cm5 = new ColumnModel();
                            cm5.name = "F_Convert_" + bom.F_ProceCode;
                            cm5.label = "转化率";
                            cm5.width = 100;
                            cm5.align = "left";
                            cm5.sort = false;
                            cm5.statistics = false;
                            cm5.children = null;

                            ColumnModel cm6 = new ColumnModel();
                            cm6.name = "F_ConvertTag_" + bom.F_ProceCode;
                            cm6.label = "转化率标记";
                            cm6.width = 100;
                            cm6.align = "left";
                            cm6.sort = false;
                            cm6.statistics = false;
                            cm6.hidden = true;
                            cm6.children = null;

                            ColumnModel cm = new ColumnModel();
                            cm.name = "F_ProceCode_" + bom.F_ProceCode;
                            cm.label = bom.F_ProceName;
                            cm.width = 500;
                            cm.align = "center";
                            cm.sort = false;
                            cm.statistics = false;
                            cm.children = new List<ColumnModel>();
                            cm.children.Add(cm1);
                            cm.children.Add(cm2);
                            cm.children.Add(cm3);
                            cm.children.Add(cm4);
                            cm.children.Add(cm5);
                            cm.children.Add(cm6);
                            columns.Add(cm);
                        }
                    }

                    
                }


                #endregion

                return columns;
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

        private List<ProductBom> GetGoods(string childrenid, List<ProductBom> boms, List<ProductBom> goods)
        {
           var row= boms.Find(r => r.F_ID == childrenid);
           if (row == null)
               return goods;
           else
           {
               if (row.F_Level == 0)
               {
                   goods.Add(row);
                   return goods;
               }
               else
               {
                   goods.Add(row);
                   return GetGoods(row.F_ParentID, boms, goods);
               }
           }
        }

        private DataRow SetDataRow(string parentid, List<GoodsConvert> converts, DataRow dr)
        {
            var row = converts.Find(r => r.F_ParentID == parentid);
            if (row == null)
                return dr;
            else
            {
                dr["F_GoodsCode_" + row.F_ProceCode] = row.F_GoodsCode;
                dr["F_GoodsName_" + row.F_ProceCode] = row.F_GoodsName;
                dr["F_GoodsQty_" + row.F_ProceCode] = row.F_Qty;
                dr["F_ConvertRange_" + row.F_ProceCode] = row.F_ConvertRange;
                dr["F_Convert_" + row.F_ProceCode] = row.F_Convert;
                dr["F_ConvertTag_" + row.F_ProceCode] = row.F_ConvertTag;

                return SetDataRow(row.F_ID, converts, dr);
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
                var mes_CollarHeadEntity = GetMes_CollarHeadEntity(keyValue); 
                db.Delete<Mes_CollarHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_CollarDetailEntity>(t=>t.C_CollarNo == mes_CollarHeadEntity.C_CollarNo);
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
        public void SaveEntity(string keyValue, Mes_CollarHeadEntity entity,List<Mes_CollarDetailEntity> mes_CollarDetailEntityList)
        {

            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_CollarHeadEntityTmp = GetMes_CollarHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_CollarDetailEntity>(t=>t.C_CollarNo == mes_CollarHeadEntityTmp.C_CollarNo);
                    foreach (var item in mes_CollarDetailEntityList)
                    {
                        item.Create();
                        item.C_CollarNo = mes_CollarHeadEntityTmp.C_CollarNo;
                    }

                    db.Insert(mes_CollarDetailEntityList);
                }
                else
                {
                    var dp = new DynamicParameters(new { });
                    dp.Add("@BillType", "领料单");
                    dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                    db.ExecuteByProc("sp_GetDoucno", dp);
                    var billNo = dp.Get<string>("@Doucno");//存储过程返回单号
                    entity.C_CollarNo = billNo;
                    entity.Create();
                    db.Insert(entity);
                    foreach (var item in mes_CollarDetailEntityList)
                    {
                        item.Create();
                        item.C_CollarNo = entity.C_CollarNo;
                        item.C_OrderNo = null;
                    }
                    //mes_CollarDetailEntity.Create();
                    //mes_CollarDetailEntity.C_CollarNo = entity.C_CollarNo;
                    db.Insert(mes_CollarDetailEntityList);
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
        /// <summary>
        /// 新增实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(List<Mes_CollarHeadEntity> headList, List<Mes_CollarDetailEntity> mes_CollarDetailEntityList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                foreach (var entity in headList)
                {
                    entity.Create();
                }
                db.Insert(headList);
                foreach (var item in mes_CollarDetailEntityList)
                {
                    item.Create();
                }
                db.Insert(mes_CollarDetailEntityList);
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
        /// 自动生成领料单
        /// </summary>
        /// <param name="date"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AutoCreateOrder(string date, out string message)
        {
            bool success = true;
            message = "";
            var db = this.BaseRepository().BeginTrans();
            try
            {
                List< Product > products=new List<Product>();
                Dictionary<string, List<ProductBom>> boms=new Dictionary<string, List<ProductBom>>();
                List<GoodsInventory> inventorys=new List<GoodsInventory>();
                List<GoodsOut> goodsouts=new List<GoodsOut>();

                List<Mes_CollarHeadTempEntity> tempheads = new List<Mes_CollarHeadTempEntity>();
                List<Mes_CollarDetailTempEntity> tempdetails = new List<Mes_CollarDetailTempEntity>();

                List<Mes_CollarHeadEntity> heads=new List<Mes_CollarHeadEntity>();
                List<Mes_CollarDetailEntity> details=new List<Mes_CollarDetailEntity>();


                #region 获取生产订单
                if (success)
                {
                    string strGetProduct = @"--获取生产计划单
SELECT G.G_Code F_GoodsCode,SUM(D.P_Qty) F_Qty FROM Mes_ProductOrderHead H
LEFT JOIN Mes_ProductOrderDetail D ON D.P_OrderNo = H.P_OrderNo
LEFT JOIN Mes_Goods G ON G.G_Erpcode=D.P_GoodsCode AND G.G_Kind=3
WHERE CONVERT(VARCHAR(10),H.P_UseDate,120) =@date AND H.P_Status=2
GROUP BY G.G_Code 
HAVING(G.G_Code  IS NOT NULL)";

                    // 虚拟参数
                    var dp = new DynamicParameters(new { });
                    if (!date.IsEmpty())
                    {
                        dp.Add("date", date, DbType.String);
                    }

                    var rows = new RepositoryFactory().BaseRepository().FindList<Product>(strGetProduct, dp);
                    if (rows.Count() > 0)
                    {
                        products = rows.ToList();
                    }
                    else
                    {
                        success = false;
                        message = string.Format("生产日期{0}没有待生产的计划订单",date);
                    }
                }
                #endregion

                #region  获取产品配方

                if (success)
                {
                    string strGetBom = @"--递归获取子节点
WITH CTE
AS (SELECT ID,
           B_ParentID,
           B_GoodsCode,
           B_GoodsName,
           B_StockCode,
           B_Qty,
           0 AS F_Level
    FROM Mes_BomRecord
    WHERE B_GoodsCode = @F_GoodsCode
    UNION ALL
    SELECT Bom1.ID,
           Bom1.B_ParentID,
           Bom1.B_GoodsCode,
           Bom1.B_GoodsName,
           Bom1.B_StockCode,
           Bom1.B_Qty,
           (Bom2.F_Level + 1) F_Level
    FROM Mes_BomRecord Bom1,
         CTE Bom2
    WHERE Bom1.B_ParentID = Bom2.ID)
SELECT C.ID F_ID,
       C.B_ParentID F_ParentID,
       C.B_GoodsCode F_GoodsCode,
       G.G_Name F_GoodsName,
       G.G_Kind F_Kind,
	   G.G_Unit F_Unit,--基本单位
	   G.G_Unit2 F_Unit2,--包装单位
	   ISNULL(G.G_UnitQty,1) F_UnitQty,--包装数量(包装规格)
	   G.G_Price F_Price,--成本价
       C.B_StockCode F_InStockCode,--领料仓库
	   S1.S_Name F_InStockName,
       G.G_StockCode F_OutStockCode,--出料仓库
	   S2.S_Name F_OutStockName,
       B_Qty F_PlanQty,--计划数量
       B_Qty F_ProposeQty,--建议数量
       F_Level
FROM CTE C
    LEFT JOIN Mes_Goods G ON C.B_GoodsCode = G.G_Code
	LEFT JOIN Mes_Stock S1 ON S1.S_Code=C.B_StockCode
	LEFT JOIN Mes_Stock S2 ON S2.S_Code=G.G_StockCode
ORDER BY C.F_Level";

                    foreach (var product in products)
                    {
                        var dp = new DynamicParameters(new { });
                        dp.Add("F_GoodsCode", product.F_GoodsCode, DbType.String);

                        var rows = new RepositoryFactory().BaseRepository().FindList<ProductBom>(strGetBom, dp);
                        if (rows.Count() > 0)
                        {
                            boms.Add(product.F_GoodsCode, rows.ToList());
                        }
                        else
                        {
                            success = false;
                            message = string.Format("产品编码{0}没有相应的配方", product.F_GoodsCode);
                            break;
                        }
                    }
                }

                #endregion

                #region 获取车间库存

                if (success)
                {
                    string strInventory = @"--加载车间库存
                    SELECT N.I_GoodsCode F_GoodsCode, SUM(N.I_Qty) F_Qty FROM Mes_Inventory N
                        LEFT JOIN Mes_Goods G ON G.G_Code = N.I_GoodsCode
                    WHERE G.G_Kind = 2
                    GROUP BY I_GoodsCode";

                    var rows = new RepositoryFactory().BaseRepository().FindList<GoodsInventory>(strInventory);
                    if (rows.Count() > 0)
                    {
                        inventorys = rows.ToList();
                    }
                }

                #endregion

                #region 加工数据

                if (success)
                {
                    foreach (var product in products)
                    {
                        decimal? productQty = product.F_Qty;//生产数量
                        var productBom = boms[product.F_GoodsCode];//配合
                        var parent = productBom.Find(r => r.F_Level == 0);//根

                        SumQty(parent.F_ID, productBom, productQty.Value);//计算所需数量
                        CalculateQty(parent.F_ID, productBom, inventorys, goodsouts);//推算每个环节
                    }
                    var groups = goodsouts.Where(r => r.F_Kind == 1).GroupBy(r => new { r.F_InStockCode,r.F_InStockName});//按原料领料仓库分组
                    if (groups.Count() < 1)
                    {
                        success = false;
                        message = string.Format("没有需要生成领料计划单数据");
                    }
                    else
                    {

                        string billNoTempTemplate = "";
                        string billNoTemplate = "";
                        int index = 1;

                        if (true)
                        {
                            var dp = new DynamicParameters(new { });
                            dp.Add("@BillType", "计划单");
                            dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                            new RepositoryFactory().BaseRepository().ExecuteByProc("sp_GetDoucno", dp);
                            billNoTempTemplate = dp.Get<string>("@Doucno");
                           
                        }
                        if (true)
                        {
                            var dp = new DynamicParameters(new { });
                            dp.Add("@BillType", "领料单");
                            dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                            new RepositoryFactory().BaseRepository().ExecuteByProc("sp_GetDoucno", dp);
                            billNoTemplate = dp.Get<string>("@Doucno");
                        }
                        if (string.IsNullOrEmpty(billNoTempTemplate) || string.IsNullOrEmpty(billNoTemplate))
                        {
                            success = false;
                            message = string.Format("领料计划单单号失败");
                        }
                        else
                        {


                            billNoTempTemplate = billNoTempTemplate.Substring(0, 12);
                            billNoTemplate = billNoTemplate.Substring(0, 12);

                            foreach (var group in groups)
                            {
                                //生成单号
                                string billTempNo = "";
                                string billNo = "";

                                billTempNo = billNoTempTemplate+index.ToString().PadLeft(4, '0');
                                billNo = billNoTemplate + index.ToString().PadLeft(4, '0');
                                index++;

                                //生成表头
                                Mes_CollarHeadEntity head = new Mes_CollarHeadEntity();
                                head.Create();
                                head.C_CollarNo = billNo;
                                head.C_Remark = "[根据生产计划单生成]";
                                head.C_StockToCode = group.Key.F_InStockCode;
                                head.C_StockToName = group.Key.F_InStockName;
                                head.P_Status = ErpEnums.RequistStatusEnum.NoAudit;
                                heads.Add(head);


                                Mes_CollarHeadTempEntity headtemp = new Mes_CollarHeadTempEntity();
                                headtemp.Create();
                                headtemp.C_CollarNo = billTempNo;
                                headtemp.C_CollarNoZS = billNo;
                                headtemp.C_StockToCode = group.Key.F_InStockCode;
                                headtemp.C_StockToName = group.Key.F_InStockName;
                                headtemp.P_Status = ErpEnums.RequistStatusEnum.NoAudit;
                                tempheads.Add(headtemp);

                                //生成表体
                                var rows = group.GroupBy(r => new { r.F_GoodsCode, r.F_GoodsName, r.F_Unit, r.F_Unit2, r.F_UnitQty, r.F_Price, r.F_OutStockCode, r.F_OutStockName });
                                foreach (var row in rows)
                                {
                                    Mes_CollarDetailEntity detail = new Mes_CollarDetailEntity();
                                    detail.Create();
                                    detail.C_CollarNo = billNo;
                                    detail.C_GoodsCode = row.Key.F_GoodsCode;
                                    detail.C_GoodsName = row.Key.F_GoodsName;
                                    detail.C_StockName = row.Key.F_OutStockName;
                                    detail.C_StockCode = row.Key.F_OutStockCode;
                                    detail.C_StockName = row.Key.F_OutStockName;
                                    detail.C_Unit = row.Key.F_Unit;
                                    detail.C_Unit2 = row.Key.F_Unit2;
                                    detail.C_UnitQty = row.Key.F_UnitQty;
                                    detail.C_Price = row.Key.F_Price;
                                    detail.C_PlanQty = row.Sum(r => r.F_PlanQty);
                                    detail.C_SuggestQty = row.Sum(r => r.F_ProposeQty);

                                    details.Add(detail);

                                    Mes_CollarDetailTempEntity detailtemp = new Mes_CollarDetailTempEntity();
                                    detailtemp.Create();
                                    detailtemp.C_CollarNo = billTempNo;
                                    detailtemp.C_GoodsCode = row.Key.F_GoodsCode;
                                    detailtemp.C_GoodsName = row.Key.F_GoodsName;
                                    detailtemp.C_StockCode = row.Key.F_OutStockCode;
                                    detailtemp.C_StockName = row.Key.F_OutStockName;
                                    detailtemp.C_Unit = row.Key.F_Unit;
                                    detailtemp.C_Unit2 = row.Key.F_Unit2;
                                    detailtemp.C_UnitQty = row.Key.F_UnitQty;
                                    detailtemp.C_Price = row.Key.F_Price;
                                    detailtemp.C_PlanQty = detail.C_PlanQty;
                                    detailtemp.C_SuggestQty = detail.C_SuggestQty;

                                    tempdetails.Add(detailtemp);
                                }
                            }
                        }
                    }


                    
                }

               

                #endregion

                #region 保存数据

                if (success)
                {
                    string strUpdate = @"UPDATE dbo.Mes_ProductOrderHead SET P_Status=3 WHERE P_Status=2 AND CONVERT(VARCHAR(10),P_UseDate,120) =@date";

                    // 虚拟参数
                    var dp = new DynamicParameters(new { });
                    if (!date.IsEmpty())
                    {
                        dp.Add("date", date, DbType.String);
                    }

                    db.ExecuteBySql(strUpdate,dp);

                    db.Insert(tempheads);
                    db.Insert(tempdetails);
                    db.Insert(heads);
                    db.Insert(details);
                }

                #endregion

                if (success)
                {
                    db.Commit();
                }
                else
                {
                    db.Rollback();
                }
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

    
            return success;
        }


        /// <summary>
        ///计算某个产品每个环节所需总数量(其中包含g=>kg转换)
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="boms"></param>
        private void SumQty(string parentid, List<ProductBom> boms,decimal quantity)
        {
            var rows = boms.Where(r => r.F_ParentID == parentid); //子级
            if (rows == null || rows.Count() < 1)
            {
                return;
            }
            else
            {
                foreach (var row in rows)
                {
                    row.F_ProposeQty = (row.F_ProposeQty * quantity)/1000;
                    row.F_PlanQty = (row.F_PlanQty * quantity)/1000;

                    SumQty(row.F_ID,boms,quantity);
                }
            }
        }

        /// <summary>
        /// 循环计算每个环节所需数据
        /// </summary>
        /// <param name="parentid"></param>
        /// <param name="boms"></param>
        /// <param name="inventorys"></param>
        /// <param name="goodsouts"></param>
        /// <returns></returns>
        private List<GoodsOut> CalculateQty(string parentid ,List<ProductBom> boms, List<GoodsInventory> inventorys, List<GoodsOut> goodsouts)
        {

            var rows=boms.Where(r => r.F_ParentID == parentid);//子级

            if (rows == null || rows.Count() < 1) 
            {
                return goodsouts;
            }
            else
            {
                foreach (var row in rows)
                {

                
                    GoodsOut gs = new GoodsOut();
                    gs.F_GoodsCode = row.F_GoodsCode;//物料编码
                    gs.F_GoodsName = row.F_GoodsName;//物料名称
                    gs.F_Kind = row.F_Kind;//物料类型
                    gs.F_Unit = row.F_Unit;//基本单位
                    gs.F_Unit2 = row.F_Unit2;//包装单位
                    gs.F_UnitQty = row.F_UnitQty;//包装数量
                    gs.F_Price = row.F_Price;//成本价
                    gs.F_InStockCode = row.F_InStockCode;//车间入库仓库编码
                    gs.F_InStockName = row.F_InStockName;//车间入库仓库名称
                    gs.F_OutStockCode = row.F_OutStockCode;//原料出库仓库编码
                    gs.F_OutStockName = row.F_OutStockName;//原料出库仓库名称

                    gs.F_PlanQty = row.F_PlanQty;//计划数量

                    if (row.F_ProposeQty > 0)
                    {
                        var inventory = inventorys.Find(r => r.F_GoodsCode == row.F_GoodsCode);//查找车间库存
                        if (inventory != null && inventory.F_Qty > 0)
                        {
                            //存在车间库存情况
                            gs.F_InventoryQty = inventory.F_Qty;//库存数量
                            var childrens = boms.Where(r => r.F_ParentID == row.F_ID);//查找子级

                            if (inventory.F_Qty > row.F_ProposeQty)
                            {
                                //车间库存充足
                                inventory.F_Qty -= row.F_ProposeQty;

                                row.F_ProposeQty = 0;
                                foreach (var children in childrens)
                                {
                                    children.F_ProposeQty = 0;
                                }
                            }
                            else
                            {
                                //车间库存不足
                                inventory.F_Qty = 0;
                                gs.F_ProposeQty = row.F_ProposeQty - -inventory.F_Qty;

                                var surplus = gs.F_ProposeQty / gs.F_PlanQty;
                                foreach (var children in childrens)
                                {
                                    children.F_ProposeQty = children.F_PlanQty * surplus;
                                }
                            }
                            goodsouts.Add(gs);
                        }
                        else
                        {
                            gs.F_InventoryQty = 0;//库存数量
                            gs.F_ProposeQty = row.F_ProposeQty;//建议数量
                        }
                    }
                    else
                    {
                        gs.F_InventoryQty = 0;//库存数量
                        gs.F_ProposeQty =0;//建议数量

                    }
                    goodsouts.Add(gs);

                    CalculateQty(row.F_ID,boms,inventorys,goodsouts);
                }

                return goodsouts;
            }

        }

        #endregion
    }
}
