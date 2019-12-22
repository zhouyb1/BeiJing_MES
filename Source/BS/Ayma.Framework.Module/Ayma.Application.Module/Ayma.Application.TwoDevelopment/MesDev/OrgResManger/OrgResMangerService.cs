using Ayma.Application.TwoDevelopment.MesDev.ScrapManager;
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
                t.ID,
                t.O_Status,
                t.O_OrgResNo,
                dbo.GetProNamekByCode(O_ProCode) O_ProCode,
                t.O_WorkShopCode,
                t.O_WorkShopName,
                t.O_Remark,
                 dbo.GetUserNameById(t.O_CreateBy) O_CreateBy,
                t.O_CreateDate
                ");
                strSql.Append("  FROM Mes_OrgResHead t ");
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
                if (!queryParam["O_OrgResNo"].IsEmpty())
                {
                    dp.Add("O_OrgResNo", "%" + queryParam["O_OrgResNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_OrgResNo Like @O_OrgResNo ");
                }
                if (!queryParam["O_WorkShopName"].IsEmpty())
                {
                    dp.Add("O_WorkShopName", "%" + queryParam["O_WorkShopName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_WorkShopName Like @O_WorkShopName ");
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
                t.ID,
                t.O_Status,
                t.O_OrgResNo,
                t.O_OrderNo,
                t.O_OrderDate,
                t.O_Record,
                t.O_ProCode,
                t.O_WorkShopCode,
                t.O_WorkShopName,
                t.O_Remark,
                dbo.GetUserNameById(t.O_CreateBy) O_CreateBy,
                t.O_CreateDate
                ");
                strSql.Append("  FROM Mes_OrgResHead t ");
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
                if (!queryParam["O_OrgResNo"].IsEmpty())
                {
                    dp.Add("O_OrgResNo", "%" + queryParam["O_OrgResNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_OrgResNo Like @O_OrgResNo ");
                }
                if (!queryParam["O_WorkShopName"].IsEmpty())
                {
                    dp.Add("O_WorkShopName", "%" + queryParam["O_WorkShopName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_WorkShopName Like @O_WorkShopName ");
                }
                if (!queryParam["O_Status"].IsEmpty())
                {
                    dp.Add("O_Status", "%" + queryParam["O_Status"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.O_Status Like @O_Status ");
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
        public DataTable GetGoodsList(Pagination obj, string keyword, string queryJson)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(@"SELECT  s.ID,
                                s.W_GoodsCode ,
                                s.W_GoodsName ,
                                c.C_SecCode ,
                                c.C_SecName,
                                s.W_Unit,
                                s.W_Batch,
                                s.W_Qty,
                                s.W_Price,
                                s.W_WorkShop,
                                s.W_WorkShopName,
                                g.G_Price w_secprice
                        FROM    dbo.Mes_WorkShopScan s
                                INNER JOIN dbo.Mes_Convert c ON c.C_Code = s.W_GoodsCode
                                INNER JOIN mes_goods g ON g.G_Code =c.C_Code
                        WHERE   1=1  ");
            // 虚拟参数
            var dp = new DynamicParameters(new { });
            var queryParam = queryJson.ToJObject();
            if (!keyword.IsEmpty())
            {
                dp.Add("keyword", "%" + keyword + "%", DbType.String);
                sb.Append(" AND s.W_GoodsCode+s.W_GoodsName like @keyword ");
            }
            if (!queryParam["workShop"].IsEmpty())
            {
                dp.Add("workShop", "%" + queryParam["workShop"].ToString() + "%", DbType.String);
                sb.Append(" AND s.W_WorkShop like @workShop ");
            }
            if (!queryParam["proNo"].IsEmpty())
            {
                dp.Add("proNo", "%" + queryParam["proNo"].ToString() + "%", DbType.String);
                sb.Append(" AND c.C_ProNo like @proNo ");
            }

            return BaseRepository().FindTable(sb.ToString(), dp, obj);
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
                return this.BaseRepository().FindList<Mes_OrgResDetailEntity>(t=>t.O_OrgResNo == keyValue);
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
                    db.Delete<Mes_OrgResDetailEntity>(t=>t.O_OrgResNo == mes_OrgResHeadEntityTmp.O_OrgResNo);
                    foreach (var item in mes_OrgResDetailList)
                    {
                        item.Create();
                        item.O_OrgResNo = mes_OrgResHeadEntityTmp.O_OrgResNo;
                    }
                    db.Insert(mes_OrgResDetailList);
                }
                else
                {
                   
                    var dp = new DynamicParameters(new { });
                    dp.Add("@BillType", "组装与拆分单");
                    dp.Add("@Doucno", "", DbType.String, ParameterDirection.Output);
                    db.ExecuteByProc("sp_GetDoucno", dp);
                    var billNo = dp.Get<string>("@Doucno");//存储过程返回单号
                    entity.O_OrgResNo = billNo;
                    entity.Create();
                    db.Insert(entity);
                    foreach (var item in mes_OrgResDetailList)
                    {
                        item.Create();
                        item.O_OrgResNo = entity.O_OrgResNo;
                        //获取车间扫描表实体
                        var workShop = workShopScanIbll.GetMes_WorkShopScanEntity(item.O_GoodsCode);
                        //删除或更细车间扫描表里的物料数据
                        if (item.O_Qty == workShop.W_Qty)
                        {
                            db.Delete(workShop.W_GoodsCode);
                        }
                        else
                        {
                            workShop.W_Qty = workShop.W_Qty + (-1 * item.O_Qty);
                            db.Update(workShop);
                        }
                    }
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
