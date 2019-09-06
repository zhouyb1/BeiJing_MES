using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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
                t.C_CreateBy,
                t.C_CreateDate
                ");
                strSql.Append("  FROM Mes_CollarHead t ");
                strSql.Append("  WHERE t.P_Status in (1,2) ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
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
                if (!queryParam["P_Status"].IsEmpty())
                {
                    dp.Add("P_Status", "%" + queryParam["P_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_Status Like @P_Status ");
                }
                if (!queryParam["C_StockToCode"].IsEmpty())
                {
                    dp.Add("C_StockToCode", "%" + queryParam["C_StockToCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_StockToCode Like @C_StockToCode ");
                }
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.C_CreateDate >= @startTime AND t.C_CreateDate <= @endTime ) ");
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
        /// 获取库存物料
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryEntity> GetMaterList(Pagination pagination, string queryJson, string keyword, string C_Teamcode)
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

                strSql.Append(@"SELECT  S.ID ,
                                        S.I_StockCode ,
                                        S.I_StockName ,
                                        S.I_SupplyCode ,
                                        S.I_SupplyName ,
                                        S.I_Kind ,
                                        S.I_GoodsCode ,
                                        S.I_GoodsName ,
                                        S.I_Unit ,
                                        S.I_Qty ,                              
                                        S.I_Batch ,
                                        G.G_Price I_Price
	                                    ,G.G_TeamCode
										,c.T_Name
                                FROM    dbo.Mes_Inventory S
                                        LEFT JOIN dbo.Mes_Goods G ON S.I_GoodsCode = G.G_Code LEFT JOIN  Mes_Team c on c.T_Code=G.G_TeamCode where 1 = 1 ");

                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!keyword .IsEmpty())
                {
                    dp.Add("keyword", "%"+keyword+"%", DbType.String);
                    strSql.Append(" AND m.P_GoodsCode+m.P_GoodsName like @keyword ");
                }
                if (!C_Teamcode.IsEmpty())
                {
                    dp.Add("C_Teamcode", C_Teamcode , DbType.String);
                    strSql.Append(" AND G.G_TeamCode=@C_Teamcode ");
                }
                if (!queryParam.IsEmpty())
                {
                    dp.Add("stockCode",queryParam["stockCode"].ToString(), DbType.String);
                    strSql.Append(" AND S.I_StockCode =@stockCode ");
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
                    entity.Create();
                    db.Insert(entity);
                    foreach (var item in mes_CollarDetailEntityList)
                    {
                        item.Create();
                        item.C_CollarNo = entity.C_CollarNo;
                        item.C_OrderNo = entity.P_OrderNo;
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

        #endregion

    }
}
