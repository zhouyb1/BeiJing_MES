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
    /// 日 期：2019-03-13 10:06
    /// 描 述：领料单查询
    /// </summary>
    public partial class PickingMaterQueryService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_CollarHeadEntity> GetPageList(Pagination pagination, string queryJson, string C_CollarNo)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                distinct
                t.ID,
                t.C_StockCode,
                t.C_StockName,
                t.C_StockToCode,
                t.C_StockToName,
                t.P_Status,
                t.C_CollarNo,
                t.P_OrderNo,
                t.C_Remark,
                dbo.GetUserNameById(t.C_CreateBy) C_CreateBy,
                t.M_UploadBy,
                t.M_UploadDate,
                t.C_CreateDate,
                t.P_OrderDate
                ");
                strSql.Append("  FROM Mes_CollarHead t left join Mes_CollarDetail s on(t.C_CollarNo=s.C_CollarNo)");
                strSql.Append("  WHERE 1=1 and t.P_Status=3");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!string.IsNullOrWhiteSpace(C_CollarNo) && queryParam["C_StockToCode"].IsEmpty())
                {
                    dp.Add("C_CollarNo", "%" + C_CollarNo + "%", DbType.String);
                    strSql.Append(" AND t.C_CollarNo Like @C_CollarNo ");
                }
                if (!queryParam["M_GoodsName"].IsEmpty())
                {
                    dp.Add("M_GoodsName", "%" + queryParam["M_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.C_GoodsName Like @M_GoodsName ");
                }
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty() && string.IsNullOrWhiteSpace(C_CollarNo))
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.P_OrderDate >= @startTime AND t.P_OrderDate <= @endTime ) ");
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
                return this.BaseRepository().FindList<Mes_CollarHeadEntity>(strSql.ToString(), dp, pagination);
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
        public IEnumerable<Mes_CollarDetailEntity> GetMes_CollarDetailEntityList(string keyValue)
        {
            try
            {
               // return this.BaseRepository().FindList<Mes_CollarDetailEntity>(t=>t.C_CollarNo == keyValue);
                //获取加权平均价
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT d.C_StockCode
                                      ,d.C_StockName
                                      ,d.C_Unit2
                                      ,d.C_UnitQty
                                      ,d.C_Qty2
                                      ,d.C_SupplyCode
                                      ,d.C_SupplyName
                                      ,d.C_GoodsCode
                                      ,d.C_GoodsName
                                      ,d.C_Unit
                                      ,d.C_Qty
                                      ,d.C_Batch
                                      ,d.C_Remark
                                      ,d.C_Price
                                      ,d.C_PlanQty
                                      ,d.C_SuggestQty
                                      ,dbo.GetPrice(C_GoodsCode,CONVERT(VARCHAR(6),h.M_UploadDate,112)) C_Price
                                      ,dbo.GetPrice(C_GoodsCode,CONVERT(VARCHAR(6),h.M_UploadDate,112))*d.C_Qty C_Amount
                                  FROM dbo.Mes_CollarHead h INNER JOIN dbo.Mes_CollarDetail d ON h.C_CollarNo=d.C_CollarNo  where h.C_CollarNo =@C_CollarNo");

                var dp = new DynamicParameters(new {});
                dp.Add("@C_CollarNo",keyValue,DbType.String);
                var entity = this.BaseRepository().FindList<Mes_CollarDetailEntity>(strSql.ToString(), dp);
                return entity;

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
        public void SaveEntity(string keyValue, Mes_CollarHeadEntity entity,Mes_CollarDetailEntity mes_CollarDetailEntity)
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
                    mes_CollarDetailEntity.Create();
                    mes_CollarDetailEntity.C_CollarNo = mes_CollarHeadEntityTmp.C_CollarNo;
                    db.Insert(mes_CollarDetailEntity);
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    mes_CollarDetailEntity.Create();
                    mes_CollarDetailEntity.C_CollarNo = entity.C_CollarNo;
                    db.Insert(mes_CollarDetailEntity);
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
