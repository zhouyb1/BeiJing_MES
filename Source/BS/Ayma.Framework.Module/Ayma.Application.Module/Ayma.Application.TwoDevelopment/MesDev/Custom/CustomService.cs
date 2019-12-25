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
    /// 日 期：2019-11-06 16:30
    /// 描 述：客户表
    /// </summary>
    public partial class CustomService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        public CustomService()
        {
            fieldSql=@"
                t.ID,
                t.C_Code,
                t.C_Name,
                t.C_CreateBy,
                t.C_CreateDate
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Mes_CustomerEntity> GetList( string queryJson )
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
                strSql.Append(" FROM Mes_Customer t ");
                return this.BaseRepository().FindList<Mes_CustomerEntity>(strSql.ToString());
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
        public IEnumerable<Mes_CustomerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT 
                t.ID,
                t.C_Code,
                t.C_Name,
                dbo.GetUserNameById(t.C_CreateBy) C_CreateBy,
                t.C_CreateDate,
                t.S_Person,
                t.S_Telephone,
                t.S_Corp,
                t.S_Address,
                t.S_TaxCode,
                t.S_Effect1,
                t.S_Effect2,
                t.S_Effect3,
                t.S_Effect4,
                t.S_Effect5
                ");
               strSql.Append(" FROM Mes_Customer t where 1=1");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["C_Name"].IsEmpty())
                {
                    dp.Add("C_Name", "%" + queryParam["C_Name"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_Name Like @C_Name ");
                }
                if (!queryParam["S_Person"].IsEmpty())
                {
                    dp.Add("S_Person", "%" + queryParam["S_Person"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Person Like @S_Person ");
                }
                if (!queryParam["S_Telephone"].IsEmpty())
                {
                    dp.Add("S_Telephone", "%" + queryParam["S_Telephone"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Telephone Like @S_Telephone ");
                }
                if (!queryParam["S_Corp"].IsEmpty())
                {
                    dp.Add("S_Corp", "%" + queryParam["S_Corp"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Corp Like @S_Corp ");
                }
                if (!queryParam["S_Address"].IsEmpty())
                {
                    dp.Add("S_Address", "%" + queryParam["S_Address"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.S_Address Like @S_Address ");
                }
                if (!queryParam["C_Code"].IsEmpty())
                {
                    dp.Add("C_Code", "%" + queryParam["C_Code"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.C_Code Like @C_Code ");
                }
                return this.BaseRepository().FindList<Mes_CustomerEntity>(strSql.ToString(), dp, pagination);
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
        public Mes_CustomerEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<Mes_CustomerEntity>(keyValue);
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
                this.BaseRepository().Delete<Mes_CustomerEntity>(t=>t.ID == keyValue);
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
        public void SaveEntity(string keyValue, Mes_CustomerEntity entity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                        var dp = new DynamicParameters(new { });
                        dp.Add("@codeType", "客户编码");
                        dp.Add("@code", "", DbType.String, ParameterDirection.Output);
                        dp.Add("@goodsSecNo", "");
                        dp.Add("@stockType", "");
                        db.ExecuteByProc("sp_GetCode", dp);
                        var C_Code = dp.Get<string>("@code"); //存储过程返回编号
                        entity.C_Code = C_Code;
                        entity.Create();
                        db.Insert(entity);  
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
