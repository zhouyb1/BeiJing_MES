using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Ayma.Application.TwoDevelopment.MesDev
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-08 14:02
    /// 描 述：财务月结
    /// </summary>
    public partial class MonthBalanceService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Mes_MonthBalanceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ID,
                t.M_Months,
                t.M_MonthBalanceTime,
                t.M_MonthBalanceBy,
                t.M_Status,
                t.M_Remark
                ");
                strSql.Append("  FROM Mes_MonthBalance t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["M_Months"].IsEmpty())
                {
                    dp.Add("M_Months", "%" + queryParam["M_Months"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.M_Months Like @M_Months ");
                }
                return this.BaseRepository().FindList<Mes_MonthBalanceEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取Mes_MonthBalance表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public Mes_MonthBalanceEntity GetMes_MonthBalanceEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_MonthBalanceEntity>(keyValue);
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
        /// 月结、反月结
        /// </summary>
        /// <param name="month"></param>
        /// <param name="type"></param>
        /// <param name="msg"></param>
        public void PostOrCancel(string month, int type, out string msg)
        {
            try
            {
                DateTime dt=DateTime.Parse(month);//月结日期
                string lastDate = dt.AddMonths(-1).ToString("yyyy-MM");
                string nextDate= dt.AddMonths(1).ToString("yyyy-MM");
                msg = "";
                var entity = this.BaseRepository().FindEntity<Mes_MonthBalanceEntity>(r => r.M_Months == month);

                if (entity != null)
                {
                    #region 月结
                    if (type == 1)
                    {
                        if (entity.M_Status == 1)
                        {
                            msg = "已月结凭证，不能重复月结！";
                        }
                        else
                        {
                            string sql =
                                 @"SELECT [ID]
      ,[M_Months]
      ,[M_MonthBalanceTime]
      ,[M_MonthBalanceBy]
      ,[M_Status]
      ,[M_Remark]
  FROM [Mes_MonthBalance]
  WHERE LEFT(M_Months,7)='"+ lastDate + "'";

                            var rows=this.BaseRepository().FindList<Mes_MonthBalanceEntity>(sql);
                            if (rows == null || rows.Count() < 1)
                            {
                                msg = "上月月结的凭证不存在！";
                            }
                            else
                            {
                               var entityTemp= rows.First();
                               if (entityTemp.M_Status != 1)
                               {
                                   msg = "上月月结的凭证未月结，请先月结上月月结凭证！";
                               }
                               else
                               {
                                   PostMonthBalance(month, out msg);//月结
                               }
                            }
                        }
                    }
                    #endregion

                    #region 反月结

                    else
                    {
                        if (entity.M_Status != 1)
                        {
                            msg = "未月结凭证，不能反月结！";
                        }
                        else
                        {
                            string sql =
                                @"SELECT [ID]
      ,[M_Months]
      ,[M_MonthBalanceTime]
      ,[M_MonthBalanceBy]
      ,[M_Status]
      ,[M_Remark]
  FROM [Mes_MonthBalance]
  WHERE LEFT(M_Months,7)='" + nextDate + "'";

                            var rows = this.BaseRepository().FindList<Mes_MonthBalanceEntity>(sql);
                            bool success = true;
                            if (rows != null || rows.Count()>= 1)
                            {
                                var entityTemp = rows.First();
                                if (entityTemp.M_Status != 2)
                                {
                                    success = false;
                                    msg = "下月月结的凭证未反月结，请先反月结下月月结凭证！";
                                }
                            }

                            if (success)
                            {
                                CancelMonthBalance(month, out msg);//反月结
                            }
                        }
                    }

                    #endregion
                }
                else
                {
                    msg = "月结的凭证不存在！";
                }


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
        /// 月结
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool PostMonthBalance(string month, out string msg)
        {
            msg = "";

            try
            {
                UserInfo userinfo = LoginUserInfo.Get();
                var dp = new DynamicParameters(new { });
                dp.Add("@BalanceMonth", month);
                dp.Add("@BalanceBy", userinfo.realName);
                dp.Add("@errcode", "", DbType.Int32, ParameterDirection.Output);
                dp.Add("@errtxt", "", DbType.String, ParameterDirection.Output);
                this.BaseRepository().ExecuteByProc("sp_MonthBalance_Post", dp);

                int errcode = dp.Get<int>("@errcode");//返回的错误代码 0：成功
                string errMsg = dp.Get<string>("@errtxt");//存储过程返回的错误消息

                if (errcode != 0)
                {
                    msg = errMsg;
                    return false;
                }
                else
                {
                    return true;
                }
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
        /// 反月结
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public bool CancelMonthBalance(string month, out string msg)
        {
            msg = "";

            try
            {
                UserInfo userinfo = LoginUserInfo.Get();
                var dp = new DynamicParameters(new { });
                dp.Add("@BalanceMonth", month);
                dp.Add("@BalanceBy", userinfo.realName);
                dp.Add("@errcode", "", DbType.Int32, ParameterDirection.Output);
                dp.Add("@errtxt", "", DbType.String, ParameterDirection.Output);
                this.BaseRepository().ExecuteByProc("sp_MonthBalance_Cancel", dp);

                int errcode = dp.Get<int>("@errcode");//返回的错误代码 0：成功
                string errMsg = dp.Get<string>("@errtxt");//存储过程返回的错误消息

                if (errcode != 0)
                {
                    msg = errMsg;
                    return false;
                }
                else
                {
                    return true;
                }
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
        /// 新增月结凭证
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, Mes_MonthBalanceEntity entity,out string msg)
        {
            try
            {
                UserInfo userinfo = LoginUserInfo.Get();

                entity.Create();
                entity.M_Status = 2;
                entity.M_MonthBalanceBy = userinfo.account;
                entity.M_MonthBalanceTime=DateTime.Now;
                msg = "";

                string sql=@"SELECT M_MonthsFROM Mes_MonthBalance WHERE LEFT(M_Months, 7) = LEFT('"+entity.M_Months+"', 7)";
                var entityTemp = this.BaseRepository().FindEntity<Mes_MonthBalanceEntity>(r => r.M_Months.Substring(0, 7) == entity.M_Months.Substring(0,7));

                if (entityTemp == null)
                {
                    this.BaseRepository().Insert(entity);
                }
                else
                {
                    msg = "【" + entity.M_Months + "】该月已存在月结凭证，添加失败";
                }

                
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
        /// 删除月结凭证
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public void DeleteEntity(string keyValue, out string msg)
        {
            try
            {
                msg = "";
                var entityTemp = this.BaseRepository().FindEntity<Mes_MonthBalanceEntity>(r => r.ID == keyValue);

                if (entityTemp != null)
                {
                    if (entityTemp.M_Status == 2)
                    {
                        this.BaseRepository().Delete<Mes_MonthBalanceEntity>(t => t.ID == keyValue);
                    }
                    else
                    {
                        msg = "已月结的凭证无法删除！";
                    }
                }
                else
                {
                    msg = "月结的凭证不存在！";
                }


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
