using System.Net.Sockets;
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
    /// 日 期：2019-03-07 11:05
    /// 描 述：生产订单管理
    /// </summary>
    public partial class ProductOrderManagerService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_ProductOrderHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.P_OrderNo,
                t.P_OrderDate,
                t.P_UseDate,
                t1.P_GoodsCode,
                t1.P_Qty,
                t1.P_Unit,
                t1.P_GoodsName,
                t.P_Status
                ");
                strSql.Append("  FROM Mes_ProductOrderHead t ");
                strSql.Append("  LEFT JOIN Mes_ProductOrderDetail t1 ON t1.P_OrderNo = t.P_OrderNo ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["P_OrderNo"].IsEmpty())
                {
                    dp.Add("P_OrderNo", "%" + queryParam["P_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.P_OrderNo Like @P_OrderNo ");
                }
                if (!queryParam["P_GoodsCode"].IsEmpty())
                {
                    dp.Add("P_GoodsCode", "%" + queryParam["P_GoodsCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t1.P_GoodsCode Like @P_GoodsCode ");
                }
                if (!queryParam["P_GoodsName"].IsEmpty())
                {
                    dp.Add("P_GoodsName", "%" + queryParam["P_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t1.P_GoodsName Like @P_GoodsName ");
                }
                return this.BaseRepository().FindList<Mes_ProductOrderHeadEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取Mes_ProductOrderHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_ProductOrderHeadEntity GetMes_ProductOrderHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_ProductOrderHeadEntity>(keyValue);
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
        /// 获取Mes_ProductOrderDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_ProductOrderDetailEntity> GetMes_ProductOrderDetaillist(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_ProductOrderDetailEntity>(c => c.P_OrderNo == keyValue);
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
                var mes_ProductOrderHeadEntity = GetMes_ProductOrderHeadEntity(keyValue);
                db.Delete<Mes_ProductOrderHeadEntity>(t => t.ID == keyValue);
                db.Delete<Mes_ProductOrderDetailEntity>(t => t.P_OrderNo == mes_ProductOrderHeadEntity.P_OrderNo);
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
        public void SaveEntity(string keyValue, Mes_ProductOrderHeadEntity entity, List<Mes_ProductOrderDetailEntity> mes_ProductOrderDetaillist)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_ProductOrderHeadEntityTmp = GetMes_ProductOrderHeadEntity(keyValue);
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_ProductOrderDetailEntity>(t => t.P_OrderNo == mes_ProductOrderHeadEntityTmp.P_OrderNo);
                    foreach (var item in mes_ProductOrderDetaillist)
                    {
                        item.Create();
                        item.P_OrderNo = mes_ProductOrderHeadEntityTmp.P_OrderNo;
                    }
                    db.Insert(mes_ProductOrderDetaillist);
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    foreach (var item in mes_ProductOrderDetaillist)
                    {
                        item.Create();
                        item.P_OrderNo = entity.P_OrderNo;
                    }
                    db.Insert(mes_ProductOrderDetaillist);
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
        /// 审核单据
        /// </summary>
        /// <param name="keyValue"></param>
        public void AuditingBill(string keyValue)
        {
            var entity = GetMes_ProductOrderHeadEntity(keyValue);
            entity.P_UpdateBy = LoginUserInfo.Get().userId;
            entity.P_UpdateDate = DateTime.Now;
            entity.P_Status = ErpEnums.PStatusEnum.StockOut;
            this.BaseRepository().Update(entity);
        }

        /// <summary>
        /// 递归统计bom
        /// </summary>
        public IEnumerable<Mes_BomRecordEntity> GetBomList(string parentId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"WITH CTE");
            sb.Append(" AS ");
            sb.Append("( ");
            sb.Append(@"
               SELECT   b.ID ,
                        b.B_ParentID ,
                        b.B_GoodsCode ,
                        b.B_GoodsName ,
                        b.B_Unit ,
                        b.B_Qty ,
                        b.B_RecordCode ,
                        b.B_ProNo
               FROM     dbo.Mes_BomRecord  b
               WHERE b.ID=@ID");
            sb.Append(@"
               UNION ALL ");
            sb.Append(@"
               SELECT   b1.ID ,
                        b1.B_ParentID ,
                        b1.B_GoodsCode ,
                        b1.B_GoodsName ,
                        b1.B_Unit ,
                        b1.B_Qty ,
                        b1.B_RecordCode ,
                        b1.B_ProNo
               FROM     Mes_BomRecord b1 ");
            sb.Append(@"
                        INNER JOIN CTE c ON c.ID = b1.B_ParentID ");
            sb.Append(" )");
            sb.Append("SELECT *FROM CTE ");
            // 虚拟参数
            var dp = new DynamicParameters(new { });
            dp.Add("ID", parentId, DbType.String);
            var entity =this.BaseRepository().FindList<Mes_BomRecordEntity>(sb.ToString(), dp);
            return entity;
        }

        /// <summary>
        /// 获取配方列表数据
        /// </summary>
        /// <returns></returns>
//        public IEnumerable<Mes_BomRecordEntity> GetBomRecordTreeList(string queryJson)
//        {
//            try
//            {
//                var strSql = new StringBuilder();
//                strSql.Append("SELECT ");
//                strSql.Append(@"
//                           t.[ID]
//                          ,t.[B_RecordCode]
//                          ,t.[B_ProNo]
//                          ,t.[B_FormulaCode]
//                          ,t.[B_FormulaName]
//                          ,t.[B_GoodsCode]
//                          ,t.[B_GoodsName]
//                          ,t.[B_Unit]
//                          ,t.[B_Qty]
//                          ,t.[B_ParentID]
//                          ,t.[B_CreateBy]
//                          ,t.[B_CreateDate]
//                          ,t.[B_UpdateBy]
//                          ,t.[B_UpdateDate]
//                          ,t.[B_Avail]
//                          ,t.[B_StartTime]
//                          ,t.[B_EndTime]
//                          ,t.[B_Remark]
//                        ");
//                strSql.Append("  FROM [dbo].[Mes_BomRecord] t ");
//                strSql.Append("  WHERE 1=1 ");
//                var queryParam = queryJson.ToJObject();
//                // 虚拟参数
//                var dp = new DynamicParameters(new { });
                
//                if (!queryParam["ID"].IsEmpty())
//                {
//                    dp.Add("ID", "%" + queryParam["ID"].ToString() + "%", DbType.String);
//                    strSql.Append(" AND t.ID Like @ID ");
//                }
//                return this.BaseRepository().FindList<Mes_BomRecordEntity>(strSql.ToString(), dp);
//            }
//            catch (Exception ex)
//            {
//                if (ex is ExceptionEx)
//                {
//                    throw;
//                }
//                else
//                {
//                    throw ExceptionEx.ThrowServiceException(ex);
//                }
//            }
//        }
        #endregion
    }
}
