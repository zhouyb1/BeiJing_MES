using Dapper;
using Ayma.DataBase.Repository;
using Ayma.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Ayma.Application.TwoDevelopment.Tools
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2018-10-09 10:32
    /// 描 述：商家
    /// </summary>
    public partial class ToolsService : RepositoryFactory
    {

        #region 获取数据
        /// <summary>
        /// 名称重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="names">名称</param>
        /// <returns></returns>
        public bool IsName(string tables, string names)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("select * from " + tables + " where S_Name=@F_Name");
                var dp = new DynamicParameters(new { });
                dp.Add("F_Name", names, DbType.String);
                int count = this.BaseRepository().FindTable(strSql.ToString(), dp).Rows.Count;
                if (count > 0)
                {
                    return true;
                }
                return false;
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
        /// 单号重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="orderNo">单号</param>
        /// <returns></returns>
        public bool IsOrderNo(string tables, string orderNo)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("select * from " + tables + " where F_OrderNo=@OrderNo");
                var dp = new DynamicParameters(new { });
                dp.Add("OrderNo", orderNo, DbType.String);
                int count = this.BaseRepository().FindTable(strSql.ToString(), dp).Rows.Count;
                if (count > 0)
                {
                    return true;
                }
                return false;
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
        /// 编码重复验证
        /// </summary>
        /// <param name="tables">表名</param>
        /// <param name="code">编码</param>
        /// <returns></returns>
        public bool IsCode(string tables, string code)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("select * from " + tables + " where S_Code=@Code");
                var dp = new DynamicParameters(new { });
                dp.Add("Code", code, DbType.String);
                int count = this.BaseRepository().FindTable(strSql.ToString(), dp).Rows.Count;
                if (count > 0)
                {
                    return true;
                }
                return false;
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
