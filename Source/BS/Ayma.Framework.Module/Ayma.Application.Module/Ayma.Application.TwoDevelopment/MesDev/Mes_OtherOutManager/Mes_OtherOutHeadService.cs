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
    /// 日 期：2019-11-08 13:40
    /// 描 述：其它出库单
    /// </summary>
    public partial class Mes_OtherOutHeadService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        public Mes_OtherOutHeadService()
        {
            fieldSql= @"
                distinct
                t.ID,
                t.O_OtherOutNo,
                t.O_StockCode,
                t.O_StockName,
                t.O_DepartCode,
                t.O_DepartName,
                t.O_Status,
                t.O_CreateBy,
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
        public IEnumerable<Mes_OtherOutHeadEntity> GetList( string queryJson )
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
                strSql.Append(" FROM Mes_OtherOutHead t ");
                return this.BaseRepository().FindList<Mes_OtherOutHeadEntity>(strSql.ToString());
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
        public IEnumerable<Mes_OtherOutHeadEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM Mes_OtherOutHead t left join Mes_OtherOutDetail s on(t.O_OtherOutNo=s.O_OtherOutNo) where t.O_Status in(1,2)");
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
                if (!queryParam["O_StockName"].IsEmpty())
                {
                    dp.Add("O_StockName", "%" + queryParam["O_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_StockName Like @O_StockName ");
                }
                if (!queryParam["O_ProOutNo"].IsEmpty())
                {
                    dp.Add("O_ProOutNo", "%" + queryParam["O_ProOutNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_ProOutNo Like @O_ProOutNo ");
                }
                if (!queryParam["O_Status"].IsEmpty())
                {
                    dp.Add("O_Status", "%" + queryParam["O_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_Status Like @O_Status ");
                }
                return this.BaseRepository().FindList<Mes_OtherOutHeadEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取查询列表分页数据
        /// <param name="pagination">分页参数</param>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_OtherOutHeadEntity> GetPostPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM Mes_OtherOutHead t left join Mes_OtherOutDetail s on(t.O_OtherOutNo=s.O_OtherOutNo) where t.O_Status =3");
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
                if (!queryParam["O_StockName"].IsEmpty())
                {
                    dp.Add("O_StockName", "%" + queryParam["O_StockName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_StockName Like @O_StockName ");
                }
                if (!queryParam["O_ProOutNo"].IsEmpty())
                {
                    dp.Add("O_ProOutNo", "%" + queryParam["O_ProOutNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_ProOutNo Like @O_ProOutNo ");
                }
                if (!queryParam["O_Status"].IsEmpty())
                {
                    dp.Add("O_Status", "%" + queryParam["O_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_Status Like @O_Status ");
                }
                return this.BaseRepository().FindList<Mes_OtherOutHeadEntity>(strSql.ToString(), dp, pagination);
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
        public Mes_OtherOutHeadEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_OtherOutHeadEntity>(keyValue);
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
        /// 获取其它出库单从表数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public IEnumerable<Mes_OtherOutDetailEntity> GetOtherOutDetailEntity(string keyValue,string state)
        {
            try
            {
                var dp = new DynamicParameters(new { });
                var strSql = new StringBuilder();
                if (state.IsEmpty())
                {
                    strSql.Append(@"SELECT
                                   d.ID
                                  ,d.O_OtherOutNo
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
                              FROM  dbo.Mes_OtherOutHead h INNER JOIN dbo.Mes_OtherOutDetail d ON d.O_OtherOutNo =h.O_OtherOutNo where h.O_OtherOutNo =@O_OtherOutNo");
                }
                else
                {
                    DateTime now = DateTime.Now;
                    //获取拼接形式的，精确到毫秒
                    string time = now.ToString("yyyyMM");
                    dp.Add("time", time, DbType.String);
                    strSql.Append(@"SELECT
                                   d.ID
                                  ,d.O_OtherOutNo
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
                              FROM  dbo.Mes_OtherOutHead h INNER JOIN dbo.Mes_OtherOutDetail d ON d.O_OtherOutNo =h.O_OtherOutNo where h.O_OtherOutNo =@O_OtherOutNo");
                }
                dp.Add("@O_OtherOutNo", keyValue, DbType.String);
                var entity = this.BaseRepository().FindList<Mes_OtherOutDetailEntity>(strSql.ToString(), dp);
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
            try
            {
                this.BaseRepository().Delete<Mes_OtherOutHeadEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_OtherOutHeadEntity strEntity, List<Mes_OtherOutDetailEntity> mes_OtherOutDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                string a = null;
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var mes_OtherOutHeadEntityTmp = GetEntity(keyValue);
                    strEntity.Modify(keyValue);
                    db.Update(strEntity);
                    db.Delete<Mes_OtherOutDetailEntity>(t => t.O_OtherOutNo == mes_OtherOutHeadEntityTmp.O_OtherOutNo);
                    foreach (Mes_OtherOutDetailEntity item in mes_OtherOutDetailList)
                    {
                        item.O_Price =a.ToDecimal();
                        item.Create();
                        item.O_OtherOutNo = mes_OtherOutHeadEntityTmp.O_OtherOutNo;
                        db.Insert(item);
                    }
                }
                else
                {
                    var dp = new DynamicParameters(new { });
                    dp.Add("@BillType", "其他出库单");
                    dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                    db.ExecuteByProc("sp_GetDoucno", dp);
                    var billNo = dp.Get<string>("@Doucno"); //存储过程返回单号
                    strEntity.O_OtherOutNo = billNo;
                    strEntity.Create();
                    db.Insert(strEntity);
                    foreach (Mes_OtherOutDetailEntity item in mes_OtherOutDetailList)
                    {
                        item.O_Price = a.ToDecimal(); ;
                        item.Create();
                        item.O_OtherOutNo = strEntity.O_OtherOutNo;
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

        #endregion

    }
}
