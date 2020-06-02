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
                distinct
                t.ID,
                t.O_Status,
                t.O_OutNo,
                t.O_StockCode,
                t.O_StockName,
                dbo.GetWorkShopName(t.O_WorkShop) O_WorkShopName ,
                t.O_OrderNo,
                t.O_OrderDate,
                t.O_Remark,
                t.O_WorkShop,
                dbo.GetUserNameById(t.O_CreateBy) O_CreateBy,
                t.O_CreateDate,
                t.O_Kind
                ");
                strSql.Append("  FROM Mes_OutWorkShopHead t left join Mes_OutWorkShopDetail s on(t.O_OutNo=s.O_OutNo)");
                strSql.Append("  WHERE t.O_Status in (1,2) ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.O_CreateDate >= @startTime AND t.O_CreateDate <= @endTime ) ");
                }
                if (!queryParam["OrderDate_S"].IsEmpty() && !queryParam["OrderDate_E"].IsEmpty())//新增单据时间
                {
                    dp.Add("OrderDate_S", queryParam["OrderDate_S"].ToDate(), DbType.DateTime);
                    dp.Add("OrderDate_E", queryParam["OrderDate_E"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.O_OrderDate >= @OrderDate_S AND t.O_OrderDate <= @OrderDate_E ) ");
                }
                if (!queryParam["O_OutNo"].IsEmpty())
                {
                    dp.Add("O_OutNo", "%" + queryParam["O_OutNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_OutNo Like @O_OutNo ");
                }
                if (!queryParam["M_GoodsName"].IsEmpty())
                {
                    dp.Add("M_GoodsName", "%" + queryParam["M_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.O_GoodsName Like @M_GoodsName ");
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
                if (!queryParam["O_WorkShop"].IsEmpty())
                {
                    dp.Add("O_WorkShop", "%" + queryParam["O_WorkShop"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_WorkShop Like @O_WorkShop ");
                }
                if (!queryParam["O_Kind"].IsEmpty())
                {
                    dp.Add("O_Kind", "%" + queryParam["O_Kind"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_Kind Like @O_Kind ");
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
        /// 日耗库出库车间单查询
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_OutWorkShopHeadEntity> GetPostIndex(Pagination pagination ,string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                distinct
                t.ID,
                t.O_Status,
                t.O_OutNo,
                t.O_StockCode,
                t.O_StockName,
                dbo.GetWorkShopName(t.O_WorkShop) O_WorkShopName ,
                t.O_OrderNo,
                t.O_OrderDate,
                t.O_WorkShop,
                t.O_Remark,
                dbo.GetUserNameById(t.O_CreateBy) O_CreateBy,
                t.O_CreateDate,
                t.O_Kind,
                 dbo.GetUserNameById(t.O_UploadBy) O_UploadBy,
                t.O_UploadDate
                ");
                strSql.Append("  FROM Mes_OutWorkShopHead t left join Mes_OutWorkShopDetail s on(t.O_OutNo=s.O_OutNo)");
                strSql.Append("  WHERE 1=1 and t.O_Status=3");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.O_CreateDate >= @startTime AND t.O_CreateDate <= @endTime ) ");
                }
                if (!queryParam["OrderDate_S"].IsEmpty() && !queryParam["OrderDate_E"].IsEmpty())//新增单据时间
                {
                    dp.Add("OrderDate_S", queryParam["OrderDate_S"].ToDate(), DbType.DateTime);
                    dp.Add("OrderDate_E", queryParam["OrderDate_E"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.O_OrderDate >= @OrderDate_S AND t.O_OrderDate <= @OrderDate_E ) ");
                }

                if (!queryParam["M_GoodsName"].IsEmpty())
                {
                    dp.Add("M_GoodsName", "%" + queryParam["M_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.O_GoodsName Like @M_GoodsName ");
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
                if (!queryParam["O_WorkShop"].IsEmpty())
                {
                    dp.Add("O_WorkShop", "%" + queryParam["O_WorkShop"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_WorkShop Like @O_WorkShop ");
                }
                if (!queryParam["O_Kind"].IsEmpty())
                {
                    dp.Add("O_Kind", "%" + queryParam["O_Kind"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_Kind Like @O_Kind ");
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
                //return this.BaseRepository().FindList<Mes_OutWorkShopDetailEntity>(t=>t.O_OutNo == keyValue);
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT 
                                       d.O_GoodsCode
                                      ,d.O_GoodsName
                                      ,d.O_Unit
                                      ,d.O_Qty
                                      ,d.O_Batch
                                      ,d.O_Remark
                                      ,dbo.GetPrice(d.O_GoodsCode,CONVERT(VARCHAR(6),h.O_UploadDate,112)) O_Price
                                      ,dbo.GetPrice(d.O_GoodsCode,CONVERT(VARCHAR(6),h.O_UploadDate,112))* d.O_Qty O_Amount
                                  FROM dbo.Mes_OutWorkShopHead h 
                                  INNER JOIN  dbo.Mes_OutWorkShopDetail d ON h.O_OutNo =d.O_OutNo WHERE h.O_OutNo=@O_OutNo");
                var dp = new DynamicParameters(new {});
                dp.Add("@O_OutNo",keyValue,DbType.String);
                var entity = this.BaseRepository().FindList<Mes_OutWorkShopDetailEntity>(strSql.ToString(), dp);
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
                strSql.Append(@"select m.*,g.G_Price as I_Price from Mes_Inventory m left join Mes_Goods g on m.I_GoodsCode = g.G_Code where m.I_Qty <> 0 and m.I_StockCode =@stockCode");
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
                    var dp = new DynamicParameters(new { });
                    dp.Add("@BillType", "线边仓出库到车间单");
                    dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                    db.ExecuteByProc("sp_GetDoucno", dp);
                    var billNo = dp.Get<string>("@Doucno");//存储过程返回单号
                    entity.O_OutNo = billNo;
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
