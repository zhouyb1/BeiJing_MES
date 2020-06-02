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
    /// 日 期：2019-11-07 13:51
    /// 描 述：其它入库单
    /// </summary>
    public partial class OtherWarehouseReceiptService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        public OtherWarehouseReceiptService()
        {
            fieldSql= @"
                distinct
                t.ID,
                t.O_OtherInNo,
                t.O_StockCode,
                t.O_StockName,
                t.O_OrderDate,
                t.O_Status,
                dbo.GetUserNameById(t.O_CreateBy) O_CreateBy,
                t.O_CreateDate,
                dbo.GetUserNameById(t.O_UpdateBy) O_UpdateBy,
                t.O_UpdateDate,
                dbo.GetUserNameById(t.O_DeleteBy) O_DeleteBy,
                t.O_DeleteDate,
                 dbo.GetUserNameById(t.O_UploadBy) O_UploadBy,
                t.O_UploadDate,
                t.O_Remark,
                t.MonthBalance
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_OtherInHeadEntity> GetList( string queryJson )
        {
            try
            {
                //参考写法
                //var queryParam = queryJson.ToJObject();
                // 虚拟参数
                //var dp = new DynamicParameters(new { });
                //dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM Mes_OtherInHead t ");
                return this.BaseRepository().FindList<Mes_OtherInHeadEntity>(strSql.ToString());
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
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_OtherInHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM Mes_OtherInHead t left join  Mes_OtherInDetail s on(t.O_OtherInNo=s.O_OtherInNo) where t.O_Status in (1,2)");
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
                if (!queryParam["O_OtherInNo"].IsEmpty())
                {
                    dp.Add("O_OtherInNo", "%" + queryParam["O_OtherInNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_OtherInNo Like @O_OtherInNo ");
                }
                if (!queryParam["O_StockName"].IsEmpty())
                {
                    dp.Add("O_StockName", "%" + queryParam["O_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_StockCode Like @O_StockName ");
                }
                if (!queryParam["O_Status"].IsEmpty())
                {
                    dp.Add("O_Status", "%" + queryParam["O_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_Status Like @O_Status ");
                }
                return this.BaseRepository().FindList<Mes_OtherInHeadEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取列表分页数据(其它入库单查询)
        /// <param name="pagination">分页参数</param>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_OtherInHeadEntity> GetPostPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM Mes_OtherInHead t left join Mes_OtherInDetail s on(t.O_OtherInNo=s.O_OtherInNo) where t.O_Status = 3");
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
                    strSql.Append(" AND ( t.O_OrderDate >= @OrderDate_S AND t.O_OrderDate  <= @OrderDate_E ) ");
                }
                if (!queryParam["M_GoodsName"].IsEmpty())
                {
                    dp.Add("M_GoodsName", "%" + queryParam["M_GoodsName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND s.O_GoodsName Like @M_GoodsName ");
                }
                if (!queryParam["O_OtherInNo"].IsEmpty())
                {
                    dp.Add("O_OtherInNo", "%" + queryParam["O_OtherInNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_OtherInNo Like @O_OtherInNo ");
                }
                if (!queryParam["O_StockName"].IsEmpty())
                {
                    dp.Add("O_StockName", "%" + queryParam["O_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_StockCode Like @O_StockName ");
                }
                if (!queryParam["O_Status"].IsEmpty())
                {
                    dp.Add("O_Status", "%" + queryParam["O_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_Status Like @O_Status ");
                }
                return this.BaseRepository().FindList<Mes_OtherInHeadEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_OtherInHeadEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_OtherInHeadEntity>(keyValue);
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
            try
            {
                this.BaseRepository().Delete<Mes_OtherInHeadEntity>(t=>t.ID == keyValue);
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
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, Mes_OtherInHeadEntity entity, List<Mes_OtherInDetailEntity> strmes_MaterOtherInDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_MaterInHeadEntityTmp = GetEntity(keyValue);
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<Mes_OtherInDetailEntity>(t => t.O_OtherInNo == mes_MaterInHeadEntityTmp.O_OtherInNo);
                    foreach (Mes_OtherInDetailEntity item in strmes_MaterOtherInDetailList)
                    {
                         item.Create();
                        item.O_OtherInNo = mes_MaterInHeadEntityTmp.O_OtherInNo;
                        db.Insert(item);
                    }
                }
                else
                {
                    var dp = new DynamicParameters(new { });
                        dp.Add("@BillType", "其他入库单");
                        dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                        db.ExecuteByProc("sp_GetDoucno", dp);   
                    var billNo = dp.Get<string>("@Doucno"); //存储过程返回单号
                    entity.O_OtherInNo = billNo;
                    entity.Create();
                    db.Insert(entity);
                    foreach (Mes_OtherInDetailEntity item in strmes_MaterOtherInDetailList)
                    {
                        item.Create();
                        item.O_OtherInNo = entity.O_OtherInNo;
                        //item.M_Kind = entity.M_Kind;
                        db.Insert(item);
                    }
                }
                db.Commit();
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
        /// 获取物料列表数据(非成品:原材料/半成品)
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
                                  ,dbo.GetPrice(G_Code,@time) G_Price
                                  ,[G_Unit2]
                              FROM [dbo].[Mes_Goods] t ");
                strSql.Append(" where 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                DateTime now = DateTime.Now;
                //获取拼接形式的，精确到毫秒
                string time = now.ToString("yyyyMM");
                dp.Add("time", time, DbType.String);
                if (!keyword.IsEmpty())
                {
                    dp.Add("keyword", "%" + keyword + "%", DbType.String);
                    strSql.Append(" AND t.G_Code+t.G_Name like @keyword ");
                }
                if (!queryParam["O_StockCode"].IsEmpty())
                {
                    dp.Add("@O_StockCode", "%" + queryParam["O_StockCode"].ToString() + "%", DbType.String);
                    strSql.Append(" and t.G_StockCode like @O_StockCode ");
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
        /// 获取Mes_OtherInHead表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_OtherInDetailEntity> GetMes_OtherInDetaiEntity(string keyValue,string state)
        {
            try
            {
                var dp = new DynamicParameters(new { });
                var strSql = new StringBuilder();
                if (state.IsEmpty())
                {
                    strSql.Append(@"SELECT
                                   d.ID
                                  ,d.O_OtherInNo
                                  ,d.O_GoodsCode
                                  ,d.O_GoodsName
                                  ,d.O_Unit
                                  ,d.O_Qty
                                  ,d.O_Batch
                                  ,d.O_Remark
                                  ,d.O_Unit2
                                  ,d.O_UnitQty
                                  ,d.O_Qty2
                                  ,dbo.GetPrice(d.O_GoodsCode,CONVERT(VARCHAR(6),h.O_UploadDate,112)) O_Price
                              FROM  dbo.Mes_OtherInHead h INNER JOIN dbo.Mes_OtherInDetail d ON d.O_OtherInNo =h.O_OtherInNo where h.O_OtherInNo =@O_OtherInNo");
                }
                else
                {
                    DateTime now = DateTime.Now;
                    //获取拼接形式的，精确到毫秒
                    string time = now.ToString("yyyyMM");
                    dp.Add("time", time, DbType.String);
                    strSql.Append(@"SELECT
                                   d.ID
                                  ,d.O_OtherInNo
                                  ,d.O_GoodsCode
                                  ,d.O_GoodsName
                                  ,d.O_Unit
                                  ,d.O_Qty
                                  ,d.O_Batch
                                  ,d.O_Remark
                                  ,d.O_Unit2
                                  ,d.O_UnitQty
                                  ,d.O_Qty2
                                  ,dbo.GetPrice(d.O_GoodsCode,@time) O_Price
                              FROM  dbo.Mes_OtherInHead h INNER JOIN dbo.Mes_OtherInDetail d ON d.O_OtherInNo =h.O_OtherInNo where h.O_OtherInNo =@O_OtherInNo");
                }
                dp.Add("@O_OtherInNo", keyValue, DbType.String);
                var entity = this.BaseRepository().FindList<Mes_OtherInDetailEntity>(strSql.ToString(), dp);
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

    }
}
