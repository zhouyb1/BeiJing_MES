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
    /// 日 期：2019-03-13 11:57
    /// 描 述：出库单制作
    /// </summary>
    public partial class OutWorkShopManagerService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_OutWorkShopHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.O_Status,
                t.O_OutNo,
                t.O_StockCode,
                t.O_StockName,
                dbo.GetWorkShopName(t.O_WorkShop) O_WorkShop ,
                t.O_OrderNo,
                t.O_OrderDate,
                t.O_Remark,
                t.O_CreateBy,
                t.O_CreateDate
                ");
                strSql.Append("  FROM Mes_OutWorkShopHead t ");
                strSql.Append("  WHERE t.O_Status in (1,2) ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.O_OrderDate >= @startTime AND t.O_OrderDate <= @endTime ) ");
                }
                if (!queryParam["O_OutNo"].IsEmpty())
                {
                    dp.Add("O_OutNo", "%" + queryParam["O_OutNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_OutNo Like @O_OutNo ");
                }
                if (!queryParam["O_OrderNo"].IsEmpty())
                {
                    dp.Add("O_OrderNo", "%" + queryParam["O_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_OrderNo Like @O_OrderNo ");
                }
                if (!queryParam["O_StockName"].IsEmpty())
                {
                    dp.Add("O_StockName", "%" + queryParam["O_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_StockName Like @O_StockName ");
                }
                if (!queryParam["O_Status"].IsEmpty())
                {
                    dp.Add("O_Status", "%" + queryParam["O_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_Status Like @O_Status ");
                }
                return this.BaseRepository().FindList<Mes_OutWorkShopHeadEntity>(strSql.ToString(),dp, pagination);
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
        /// 领料单查询
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_OutWorkShopHeadEntity> GetPostIndex(Pagination pagination ,string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.O_Status,
                t.O_OutNo,
                t.O_StockCode,
                t.O_StockName,
                dbo.GetWorkShopName(t.O_WorkShop) O_WorkShop ,
                t.O_OrderNo,
                t.O_OrderDate,
                t.O_Remark,
                t.O_CreateBy,
                t.O_CreateDate
                ");
                strSql.Append("  FROM Mes_OutWorkShopHead t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.O_OrderDate >= @startTime AND t.O_OrderDate <= @endTime ) ");
                }
                if (!queryParam["O_OutNo"].IsEmpty())
                {
                    dp.Add("O_OutNo", "%" + queryParam["O_OutNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_OutNo Like @O_OutNo ");
                }
                if (!queryParam["O_OrderNo"].IsEmpty())
                {
                    dp.Add("O_OrderNo", "%" + queryParam["O_OrderNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_OrderNo Like @O_OrderNo ");
                }
                if (!queryParam["O_StockName"].IsEmpty())
                {
                    dp.Add("O_StockName", "%" + queryParam["O_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_StockName Like @O_StockName ");
                }
                if (!queryParam["O_Status"].IsEmpty())
                {
                    dp.Add("O_Status", "%" + queryParam["O_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_Status Like @O_Status ");
                }
                return this.BaseRepository().FindList<Mes_OutWorkShopHeadEntity>(strSql.ToString(), dp, pagination);

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
        /// 获取Mes_OutWorkShopHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_OutWorkShopHeadEntity GetMes_OutWorkShopHeadEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_OutWorkShopHeadEntity>(keyValue);
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
        /// 获取Mes_OutWorkShopDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_OutWorkShopDetailEntity> GetMes_OutWorkShopDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<Mes_OutWorkShopDetailEntity>(t=>t.O_OutNo == keyValue);
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
        /// 获取仓库物料列表
        /// </summary>
        /// <param name="stockCode"></param>
        /// <returns></returns>
        public IEnumerable<Mes_InventoryEntity> GetInventoryMaterList(Pagination paginationobj, string stockCode, string keyword)
        {
            try
            {
                //return this.BaseRepository().FindList<Mes_InventoryEntity>(c => c.I_StockCode == stockCode, paginationobj);
                var strSql = new StringBuilder();
                strSql.Append(@"select m.*,g.G_Price as I_Price from Mes_Inventory m left join Mes_Goods g on m.I_GoodsCode = g.G_Code where m.I_StockCode =@stockCode");
                var dp = new DynamicParameters(new {});
                dp.Add("@stockCode", stockCode,DbType.String);
                if (!keyword.IsEmpty())
                {
                    dp.Add("keyword", "%" + keyword + "%", DbType.String);
                    strSql.Append(" AND m.I_GoodsCode+m.I_GoodsName like @keyword ");
                }
               return this.BaseRepository().FindList<Mes_InventoryEntity>(strSql.ToString(),dp, paginationobj);
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
                var mes_OutWorkShopHeadEntity = GetMes_OutWorkShopHeadEntity(keyValue); 
                db.Delete<Mes_OutWorkShopHeadEntity>(t=>t.ID == keyValue);
                db.Delete<Mes_OutWorkShopDetailEntity>(t=>t.O_OutNo == mes_OutWorkShopHeadEntity.O_OutNo);
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
        public void SaveEntity(string keyValue, Mes_OutWorkShopHeadEntity entity, List<Mes_OutWorkShopDetailEntity> mes_OutWorkShopDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_OutWorkShopHeadEntityTmp = GetMes_OutWorkShopHeadEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_OutWorkShopDetailEntity>(t=>t.O_OutNo == mes_OutWorkShopHeadEntityTmp.O_OutNo);
                    foreach (var item in mes_OutWorkShopDetailList)
                    {
                        item.Create();
                        item.O_OutNo = mes_OutWorkShopHeadEntityTmp.O_OutNo;
                    }
                    db.Insert(mes_OutWorkShopDetailList);
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    foreach (var item in mes_OutWorkShopDetailList)
                    {
                        item.Create();
                        item.O_OutNo = entity.O_OutNo;
                    }
                    db.Insert(mes_OutWorkShopDetailList);
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
